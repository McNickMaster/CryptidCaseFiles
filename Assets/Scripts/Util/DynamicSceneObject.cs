using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DynamicSceneObject : MonoBehaviour
{

    public bool useMilestone = true;
    public Milestone myMilestone;
    
    public bool useCurrentScene = false;
    public e_Scene scene;


    void Awake()
    {
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        StartCoroutine(UpdateObject());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator UpdateObject()
    {
        yield return 0;

        if(useMilestone)
        {
            transform.GetChild(0).gameObject.SetActive(GameManager.instance.CheckMilestone(myMilestone));  
        }
        if(useCurrentScene)
        {
            transform.GetChild(0).gameObject.SetActive(GameManager.instance.currentScene == scene);
        }
    }

}
