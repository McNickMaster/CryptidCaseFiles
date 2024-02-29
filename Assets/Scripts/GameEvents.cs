using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{

    public UnityEvent Event_FirstEvidence;
    public UnityEvent Event_FirstNPC;
    public UnityEvent Event_EnterCrimeScene1;
    public UnityEvent Event_EnterOffice;

    [SerializeField]
    private List<string> scenesLoadedBefore = new List<string>();

    void Awake()
    {

        if (Event_EnterCrimeScene1 == null)
            Event_EnterCrimeScene1 = new UnityEvent();

        if (Event_EnterOffice == null)
            Event_EnterOffice = new UnityEvent();


        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scenesLoadedBefore.Contains(scene.name))
        {

        } else 
        {
            scenesLoadedBefore.Add(scene.name);

            switch(scene.name)
            {
                case "Office":
                {
                    Debug.Log("office loaded for the first time");
                    Event_EnterOffice.Invoke();
                    break;
                }
                case "CS1":
                {
                    Event_EnterCrimeScene1.Invoke();
                    break;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
