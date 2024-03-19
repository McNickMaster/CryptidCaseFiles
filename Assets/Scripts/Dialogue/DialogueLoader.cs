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

    public string fileToLoad = "";
    public string convoIDToLoad = "";

    string[] simpleTextPages;

    public List<Branch> myBranches = new List<Branch>();
    public List<Slide> mySlides = new List<Slide>();

    public Transform dialogueParent;
    public GameObject slidePrefab, branchPrefab;
    public GameObject monologuePrefab, phoneCallPrefab;

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
    private bool evidence = false;

    void Awake()
    {
        instance = this;

        //jsonLoader = GetComponent<LoadTextFromJson>();

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
        PlayerInput.instance.gameObject.SetActive(currentUIObject==null);
        
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
    public void LoadPhonecall(string file)
    {
        if(file == "-1")
        {
            
        } else 
        {
            Debug.Log("loading mono with id: " + file);
            SimpleTextFileData dataFile = SaveLoadData.LoadText(file);
            simpleTextPages = dataFile.GetFilePages();

        }

        StartMonologue(phoneCallPrefab);
    }

    public void LoadMonologue(string file)
    {
        if(file == "-1")
        {
            
        } else 
        {
            if(file.Contains("Evidence_"))
            {
                evidence = true;
            }
            Debug.Log("loading mono with id: " + file);
            SimpleTextFileData dataFile = SaveLoadData.LoadText(file);
            simpleTextPages = dataFile.GetFilePages();

        }

        StartMonologue(monologuePrefab);
    }
    private void StartMonologue(GameObject prefab)
    {
        dialogueIndex = 0;
        PlayerInput.instance.enabled = false;


        objInstance = Instantiate(prefab, Vector3.zero, Quaternion.identity, dialogueParent);
        objInstance.transform.localPosition = Vector3.zero;
        title = objInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        body = objInstance.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

        Button btn = objInstance.GetComponent<Button>();
        btn.onClick.AddListener(NextPage);

        numPages = simpleTextPages.Length;

        string milestoneID = "";
        if(simpleTextPages[0].Contains("[get"))
        {
            int index = simpleTextPages[0].IndexOf("[get")+4;
            milestoneID = simpleTextPages[0].Substring(index, simpleTextPages[0].Length - index - 1);
            simpleTextPages[0] = simpleTextPages[0].Substring(0, simpleTextPages[0].Length - milestoneID.Length - 5);
        }
        //Debug.Log(milestoneID + " " + milestoneID.Length);
        if(milestoneID != "")
        {
            GameManager.instance.AddMilestone(Enum.Parse<Milestone>(milestoneID));
            
            
            
        }

        dialogueIndex = 0;
        title.text = "Monologue";
        body.text = simpleTextPages[dialogueIndex];

        
    }


    public void NextPage()
    {
        dialogueIndex++;


        if(dialogueIndex >= numPages)
        {

            Destroy(objInstance);
            PlayerInput.instance.enabled = true;
            
            if(evidence)
            {
                GameEvents.instance.Event_Evidence.Invoke();
            }
            evidence = false;

        } else 
        {
            body.text = simpleTextPages[dialogueIndex];
        }
    }
    
    
    public void LoadConversation(string id)
    {
        Debug.Log("loading convo with id: " + id);
        DialogueFileData dialogueFile = SaveLoadData.LoadDialogue(id);       

        myBranches = dialogueFile.GetBranches();
        mySlides = dialogueFile.GetSlides();

        StartConversation();
    }

    public void StartConversation()
    {
        Path initPath = new Path(new Slide[]{mySlides[0]});
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
        GameEvents.instance.Event_NPC.Invoke();
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
                    //Debug.Log("unlocking " + unlockPath.firstSlide.ID);
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
                    GameManager.instance.AddMilestone(Enum.Parse<Milestone>(currentPath.milestoneID));
                    
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
            Slide temp = mySlides[1 + mySlides.FindIndex(x => x.ID == p.endSlide.ID)];
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
//        Debug.Log("is branch found: " + (b != null));
        return b;

    }

    Path FindPath(string id)
    {

//        Debug.Log("looking for path with firstSlide id: " + id);
        Path p = null;

            
        p = currentBranch.myPathOptions.Find(p => p.firstSlide.ID == id);
    
        
        return p;
    }


    void OnDisable()
    {
        //EndConversation();
    }





}
