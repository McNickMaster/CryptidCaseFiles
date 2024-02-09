using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueLoader : MonoBehaviour
{
    public static DialogueLoader instance;

    public GameObject textAsset;
    LoadTextFromCSV csvLoader;

    public string fileToLoad = "";
    public string convoIDToLoad = "";

    public List<Branch> myBranches = new List<Branch>();

    public Transform dialogueParent;
    public GameObject slidePrefab, branchPrefab;

    private GameObject currentUIObject;
    public Path currentPath;
    private Branch currentBranch;
    private int pathIndex;

    //json part
    public LoadTextFromJson jsonLoader; 
    public GameObject dialogueBase;
    TextMeshProUGUI title, body;

    private DialogueData dialogueData;
    private GameObject objInstance;

    private int dialogueIndex = 0, numPages = 0;

    void Awake()
    {
        instance = this;


        csvLoader = GetComponent<LoadTextFromCSV>();
        jsonLoader = GetComponent<LoadTextFromJson>();

        //LoadConversation("output1");

        //LoadDialogueBranch("1");

        //Debug.Log("does 211 exist? " + (FindBranch("211") != null));
        //SpawnSlide(new Slide("ME", "HI HYD"));

        //StartConversation();
        //Debug.Log(conversation.myBranches[0].GetFirstSlidesOfPath()[0].Body);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

/*
    public void LoadDialogueBranch(string idOfSlide1)
    {
        foreach(Branch b in branches)
        {
            foreach(Path p in b.myPathOptions)
            {
                if(p.firstSlide.ID == Int32.Parse(idOfSlide1))
                {
                    path = p;
                }
            }
        }
    }
*/

    public void LoadMonologue(string file)
    {
        
        jsonLoader.LoadJson(file);
    }
    public void StartMonologue()
    {
        dialogueIndex = 0;
        PlayerInput.instance.enabled = false;


        objInstance = Instantiate(slidePrefab, Vector3.zero, Quaternion.identity, transform.GetChild(0).transform);
        objInstance.transform.localPosition = Vector3.zero;
        title = objInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        body = objInstance.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

        Button btn = objInstance.GetComponent<Button>();
        btn.onClick.AddListener(NextPage);

//        numPages = dialogueData.numPages;

        title.text = jsonLoader.GetNotePages()[0];
        body.text = jsonLoader.GetNotePages()[1];


    }

    public void NextPage()
    {
        dialogueIndex++;

        if(dialogueIndex >= numPages)
        {
            Destroy(objInstance);
            PlayerInput.instance.enabled = true;

        } else 
        {
            
            jsonLoader.LoadJson(dialogueData.GetDialoguePage(dialogueIndex));

            title.text = jsonLoader.GetNotePages()[0];
            body.text = jsonLoader.GetNotePages()[1];

        }
    }
    public void LoadConversation(string file)
    {
        
        csvLoader.LoadCSV(file);

        myBranches = csvLoader.GetBranches();
    }

    public void StartConversation()
    {
        Path initPath = new Path(new Slide[]{csvLoader.GetFirstSlide()});
        currentPath = initPath;
        initPath.firstSlide.Body = initPath.endSlide.Body + "[back]";
        initPath.DetectPathEnd();

        SpawnSlide(initPath.firstSlide);
        //SpawnBranch(myBranches[0]);
        
    }

    void StartConversation(string id)
    {
        SpawnBranch(myBranches[0]);
    }

    public void EndConversation()
    {
        Destroy(currentUIObject);
        currentUIObject = null;
    }


    public void LoadNextInPath()
    {
        Destroy(currentUIObject);

        pathIndex++;
        if(pathIndex <= currentPath.slides.Length-1)
        {
            SpawnSlide(currentPath.slides[pathIndex]);
            
            //Debug.Log("path: " + pathIndex + "/" + currentPath.slides.Length);
        } else 
        {

            if(currentBranch != null)
            {

                Path unlockPath = FindPath(currentPath.unlockPathID); 
                if(unlockPath != null)
                {
                    //Debug.Log("unlocking...");
                    unlockPath.locked = false;
                }

                Path lockPath = FindPath(currentPath.lockPathID); 
                if(lockPath != null)
                {
                    //Debug.Log("locking...");
                    lockPath.locked = true;
                }

                if(currentPath.milestoneID != "")
                {
                    GameManager.instance.AddMilestone(currentPath.milestoneID);
                    
                }
            }
            
            //Debug.Log("reached end of path: " + currentPath.pathEndBehaviour);
            switch(currentPath.pathEndBehaviour)
            {
                case PathEndBehaviour.GOTO:
                {
//                    Debug.Log("trying to spawn new branch with id: " + currentPath.gotoID);
                    SpawnBranch(FindBranch(currentPath.gotoID));
                    break;
                }
                
                case PathEndBehaviour.END:
                {
                    EndConversation();
                    break;
                }
                
                case PathEndBehaviour.CONTINUE:
                {
                    //Debug.Log("trying to continue new branch with id: " + currentPath.gotoID);
                    SpawnBranch(FindBranch(currentPath.gotoID));
                    break;
                }
            }



        }





    }

    public void LoadPath(Path p)
    {
        p.myBranch = currentBranch;

        if(p.pathEndBehaviour.Equals(PathEndBehaviour.CONTINUE))
        {
            //Slide temp = csvLoader.slides[1 + csvLoader.slides.FindIndex(x => x.ID == p.endSlide.ID)];
            Slide temp = csvLoader.slides[1 + csvLoader.slides.FindIndex(x => x.ID == p.endSlide.ID)];
            //Debug.Log(temp.Body + " " + p.endSlide.ID + " " + (1 + csvLoader.slides.FindIndex(x => x.ID == p.endSlide.ID)));

            p.endBranch = FindBranch(temp.ID);

        }
        currentPath = p;
        pathIndex = 0;
        LoadNextInPath();
    }


    void SpawnSlide(Slide s)
    {
        
        Destroy(currentUIObject);
        SlideObject spawnSlide = Instantiate(slidePrefab, Vector3.zero, Quaternion.identity, dialogueParent).GetComponent<SlideObject>();
        spawnSlide.gameObject.transform.localPosition = Vector3.zero;
        spawnSlide.GetComponent<Button>().onClick.AddListener(DialogueLoader.instance.LoadNextInPath);
        currentUIObject = spawnSlide.gameObject;
        spawnSlide.slide = s;
        spawnSlide.PopulateTexts();
        
    }

    BranchObject SpawnBranch(Branch b)
    {
        Destroy(currentUIObject);
        pathIndex = 0;

        BranchObject spawnBranch = Instantiate(branchPrefab, Vector3.zero, Quaternion.identity, dialogueParent).GetComponent<BranchObject>();
        spawnBranch.gameObject.transform.localPosition = Vector3.zero;
        spawnBranch.branch = b;
        currentUIObject = spawnBranch.gameObject;

        if(currentBranch != spawnBranch.branch)
        {
            spawnBranch.branch.parentBranch = currentBranch;
        }
        currentBranch = spawnBranch.branch;
        spawnBranch.PopulateTexts();

        return spawnBranch;
    }

    void RespawnBranch()
    {
        pathIndex = 0;

        BranchObject spawnBranch = Instantiate(branchPrefab, Vector3.zero, Quaternion.identity, dialogueParent).GetComponent<BranchObject>();
        spawnBranch.gameObject.transform.localPosition = Vector3.zero;
        spawnBranch.branch = currentBranch;
        currentUIObject = spawnBranch.gameObject;
        spawnBranch.PopulateTexts();
    }

    Branch FindBranch(string id)
    {
        Branch b = myBranches.Find(x => x.myPathOptions[0].firstSlide.ID == id);
        Debug.Log("is branch found: " + (b != null));
        return b;

    }

    Path FindPath(string id)
    {

        Debug.Log("looking for path with firstSlide id: " + id);
        Path p = null;

            
        p = currentBranch.myPathOptions.Find(p => p.firstSlide.ID == id);
    
        




        return p;
    }








}
