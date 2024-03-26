using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corkboard : MonoBehaviour
{
    public Transform culpritSpawnY, causeSpawnY, victimY, evidenceY;
    public float corkboardWidth = 30;

    private GameObject parentObj;
    private Case thisCase;

    [SerializeField]
    private List<GameObject> spawnedPhotos = new List<GameObject>();

    void OnEnable()
    {
        
        parentObj = new GameObject("PolaroidParent");
        SpawnAll();

        GameEvents.instance.Event_Evidence.AddListener(SpawnAll);
        GameEvents.instance.Event_SolveCase.AddListener(RemoveAll);
    }

    void OnDisable()
    {
        GameEvents.instance.Event_Evidence.RemoveListener(SpawnAll);
        GameEvents.instance.Event_SolveCase.RemoveListener(RemoveAll);
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SpawnAll();
        }
    }

    void SpawnAll()
    {
        Debug.Log("managing the corkboard");
        thisCase = GameManager.instance.currentCase;

        SpawnCulprits();
        SpawnCauses();
        SpawnVictims();
        SpawnEvidence();
    }

    void RemoveAll()
    {
        foreach(GameObject g in spawnedPhotos)
        {
            Destroy(g);
        }
    }

    void SpawnCulprits()
    {
        int culpritAmount = thisCase.culpritList.Count;
        
        Vector3 spawnPos;

        for(int i = 0; i < culpritAmount; i++)
        {
            string objName = ConvertPhotoEnumToObj(thisCase.culpritList[i].ToString());
            for(int j = 0; j < GameData.instance.CULPRIT_MUGSHOTS.Length; j++)
            {
//                Debug.Log("looking for: " + objName + " from: " + GameData.instance.CULPRIT_MUGSHOTS[j].name);
                if(GameData.instance.CULPRIT_MUGSHOTS[j].name.Equals(objName) && !spawnedPhotos.Exists(p => p.name.Contains(objName)))
                {
                    //float x = (i * (corkboardWidth/thisCase.culpritList.Count)) - (corkboardWidth/2);
                    float x = (Random.Range(-5f, corkboardWidth/3));
                    spawnPos = new Vector3(x, culpritSpawnY.position.y, 3.5f);
                    spawnedPhotos.Add(ConvertDeskPhotoToCork(Instantiate(GameData.instance.CULPRIT_MUGSHOTS[j], spawnPos, Quaternion.AngleAxis(90, Vector3.right), parentObj.transform)));
                }
            }
        }
    }

    void SpawnCauses()
    {
        int causeAmount = thisCase.causeOfDeathList.Count;
        
        Vector3 spawnPos;

        for(int i = 0; i < causeAmount; i++)
        {
            string objName = ConvertPhotoEnumToObj(thisCase.causeOfDeathList[i].ToString());
            for(int j = 0; j < GameData.instance.CAUSE_MUGSHOTS.Length; j++)
            {
                //Debug.Log("looking for: " + objName + " from: " + GameData.instance.CAUSE_MUGSHOTS[j].name);
                if(GameData.instance.CAUSE_MUGSHOTS[j].name.Equals(objName) && !spawnedPhotos.Exists(p => p.name.Contains(objName)))
                {
                    //float x = (i * (corkboardWidth/causeAmount)) - (corkboardWidth/2);
                    float x = (Random.Range(-5f, corkboardWidth/3));
                    spawnPos = new Vector3(x, causeSpawnY.position.y, 2);
                    spawnedPhotos.Add(ConvertDeskPhotoToCork(Instantiate(GameData.instance.CAUSE_MUGSHOTS[j], spawnPos, Quaternion.AngleAxis(90, Vector3.right), parentObj.transform)));
                }
            }
        }
    }

    void SpawnEvidence()
    {
        int evidenceAmount = thisCase.evidenceList.Count;
        
        Vector3 spawnPos;

        for(int i = 0; i < evidenceAmount; i++)
        {
            string objName = ConvertPhotoEnumToObj(thisCase.evidenceList[i].ToString());
            for(int j = 0; j < GameData.instance.EVIDENCE_MUGSHOTS.Length; j++)
            {
                //Debug.Log("looking for: " + objName + " from: " + GameData.instance.EVIDENCE_MUGSHOTS[j].name);
                if(GameData.instance.EVIDENCE_MUGSHOTS[j].name.Equals(objName) && !spawnedPhotos.Exists(p => p.name.Contains(objName)))
                {
                    //float x = (i * (corkboardWidth/evidenceAmount)) - (corkboardWidth/2);
                    float x = (Random.Range(-5f, corkboardWidth/3));
                    spawnPos = new Vector3(x, evidenceY.position.y, 2);
                    spawnedPhotos.Add(ConvertDeskPhotoToCork(Instantiate(GameData.instance.EVIDENCE_MUGSHOTS[j], spawnPos, Quaternion.AngleAxis(90, Vector3.right), parentObj.transform)));
                }
            }
        }
    }
    void SpawnVictims()
    {
        int victimAmount = thisCase.victimList.Count;
        
        Vector3 spawnPos;

        for(int i = 0; i < victimAmount; i++)
        {
            string objName = ConvertPhotoEnumToObj(thisCase.victimList[i].ToString());
            for(int j = 0; j < GameData.instance.VICTIM_MUGSHOTS.Length; j++)
            {
                //Debug.Log("looking for: " + objName + " from: " + GameData.instance.VICTIM_MUGSHOTS[j].name);
                if(GameData.instance.VICTIM_MUGSHOTS[j].name.Equals(objName) && !spawnedPhotos.Exists(p => p.name.Contains(objName)))
                {
                    //float x = (i * (corkboardWidth/victimAmount)) - (corkboardWidth/2);
                    float x = (Random.Range(-5f, corkboardWidth/3));
                    spawnPos = new Vector3(x, victimY.position.y, 2);
                    spawnedPhotos.Add(ConvertDeskPhotoToCork(Instantiate(GameData.instance.VICTIM_MUGSHOTS[j], spawnPos, Quaternion.AngleAxis(90, Vector3.right), parentObj.transform)));
                }
            }
        }
    }

    string ConvertPhotoEnumToObj(string enumName)
    {
        string objName = "";

        objName = "photo_" + enumName.ToLower();

//        Debug.Log("Converting " + enumName + " to " + objName);


        return objName;
    }

    GameObject ConvertDeskPhotoToCork(GameObject photo)
    {
        photo.transform.localScale = Vector3.one * 1.75f;
        photo.transform.GetChild(0).localPosition = new Vector3(0,0,-1.3f);
        photo.GetComponent<Interactable_Picture>().normal = new Vector3(0,0,1);

        return photo;
    }

}
