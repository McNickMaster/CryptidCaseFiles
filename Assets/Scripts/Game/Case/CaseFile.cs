using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseFile : MonoBehaviour
{
    public static CaseFile instance;
    public Culprit culpritGuess;
    public CauseOfDeath causeOfDeathGuess;
    public Victim victimGuess;
    public Victim victim2Guess;

    public GameObject culpritPicture;
    public GameObject causePicture;
    public GameObject victimPicture;
    public GameObject victim2Picture;

    public Case thisCase;

    GameObject objectParent;


    public Transform photoSpawnTL, photoSpawnBR;


    void Awake()
    {
        instance = this;
        
        
        
    }

    void OnEnable()
    {
        
        ResetCaseFile();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SetCulpritGuess()
    {

    }
    
    public Case GetGuess()
    {
        Case caseGuess = ScriptableObject.CreateInstance<Case>();
        caseGuess.InitGuess(culpritGuess, causeOfDeathGuess, victimGuess);
        return caseGuess;
    }

    public void SetCase(Case newCase)
    {
        thisCase = newCase;
        ResetCaseFile();
    }

    public void ResetCaseFile()
    {
        Destroy(objectParent);
        objectParent = new GameObject("PolaroidParent");
        objectParent.transform.parent = GetComponentInParent<TempNode>().transform;
        thisCase = GameManager.instance.currentCase;
        PopulateCulprits(); 
        PopulateCauses();
        PopulateVictims();

    }

    public void PopulateCulprits()
    {
        //spawn all the culprit/cause of death photos listed in the Case
        

        int culpritAmount = thisCase.culpritList.Count;
        
        for(int i = 0; i < culpritAmount; i++)
        {
            string objName = ConvertPhotoEnumToObj(thisCase.culpritList[i].ToString());
            for(int j = 0; j < GameData.instance.CULPRIT_MUGSHOTS.Length; j++)
            {
                //Debug.Log("looking for: " + objName + " from: " + GameData.instance.CULPRIT_MUGSHOTS[j].name);
                if(GameData.instance.CULPRIT_MUGSHOTS[j].name.Equals(objName))
                {
                    
                    Instantiate(GameData.instance.CULPRIT_MUGSHOTS[j], GetRandomPointWithinSpawn(), Quaternion.identity, objectParent.transform);
                }
            }
        }
        
    }

    public void PopulateCauses()
    {
        int causeAmount = thisCase.causeOfDeathList.Count;
        
        for(int i = 0; i < causeAmount; i++)
        {
            string objName = ConvertPhotoEnumToObj(thisCase.causeOfDeathList[i].ToString());
            for(int j = 0; j < GameData.instance.CAUSE_MUGSHOTS.Length; j++)
            {
                //Debug.Log("looking for: " + objName);
                if(GameData.instance.CAUSE_MUGSHOTS[j].name.Equals(objName))
                {
                    
                    Instantiate(GameData.instance.CAUSE_MUGSHOTS[j], GetRandomPointWithinSpawn(), Quaternion.identity, objectParent.transform);
                }
            }
        }
    }

    public void PopulateVictims()
    {
        int victAmount = thisCase.victimList.Count;
        
        for(int i = 0; i < victAmount; i++)
        {
            string objName = ConvertPhotoEnumToObj(thisCase.victimList[i].ToString());
            for(int j = 0; j < GameData.instance.VICTIM_MUGSHOTS.Length; j++)
            {
                //Debug.Log("looking for: " + objName);
                if(GameData.instance.VICTIM_MUGSHOTS[j].name.Equals(objName))
                {
                    
                    Instantiate(GameData.instance.VICTIM_MUGSHOTS[j], GetRandomPointWithinSpawn(), Quaternion.identity, objectParent.transform);
                }
            }
        }
    }

    string ConvertPhotoEnumToObj(string enumName)
    {
        string objName = "";

        objName = "photo_" + enumName.ToLower();

//        Debug.Log(objName);


        return objName;
    }

    Vector3 GetRandomPointWithinSpawn()
    {
        float randX = Random.Range(photoSpawnTL.position.x, photoSpawnBR.position.x);
        float randZ = Random.Range(photoSpawnTL.position.z, photoSpawnBR.position.z);
        Vector3 spawnPos = new Vector3(randX, photoSpawnTL.position.y, randZ);

        return spawnPos;
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject parentObj = other.transform.parent.gameObject;
        Interactable_Picture interactable = parentObj.GetComponent<Interactable_Picture>();
        if(interactable != null)
        {
            
            if(interactable.culpritPhoto)
            {
                if(culpritPicture != null && culpritPicture != parentObj)
                {
                    culpritPicture.transform.position = GetRandomPointWithinSpawn();
                }

                culpritGuess = interactable.myCulprit;
                culpritPicture = interactable.gameObject;
            }
            
            if(interactable.causeOfDeathPhoto)
            {
                if(causePicture != null && causePicture != parentObj)
                {
                    causePicture.transform.position = GetRandomPointWithinSpawn();
                }


                causeOfDeathGuess = interactable.myCauseOfDeath;
                causePicture = interactable.gameObject;
            }

            if(interactable.victimPhoto)
            {
                if(victimPicture != null && victimPicture != parentObj)
                {
                    if(victim2Picture != null && victim2Picture != parentObj)
                    {
                        
                        //reset
                        victimPicture.transform.position = GetRandomPointWithinSpawn();
                    } else 
                    {
                        
                        victim2Guess = interactable.myVictim;
                        victim2Picture = interactable.gameObject;
                    }
                } else 
                {
                    victimGuess = interactable.myVictim;
                    victimPicture = interactable.gameObject;
                }

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject parentObj = other.transform.parent.gameObject;
        Interactable_Picture interactable = parentObj.GetComponent<Interactable_Picture>();
        if(interactable != null)
        {
            
            if(interactable.culpritPhoto)
            {

                //culpritPicture = null;
            }
            
            if(interactable.causeOfDeathPhoto)
            {

                //causePicture = null;
            }
            
            if(interactable.victimPhoto)
            {

                //causePicture = null;
            }
        }

    }



    void OnDisable()
    {
        
    }


}
