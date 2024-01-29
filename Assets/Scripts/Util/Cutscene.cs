using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{

    public GameObject cutsceneObj;
    private GameObject destination;
    public Animation cutscene;

    public bool flag_cutscene_done = false;

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(flag_cutscene_done)
        {
            EndCutscene();
        }
    }


    public void StartCutscene(GameObject destination)
    {
        cutscene.Play();
        this.destination = destination;
    }


    private void EndCutscene()
    {
        destination.SetActive(true);
        cutsceneObj.SetActive(false);
        PlayerInput.instance.UpdateBackplane();
    }
}
