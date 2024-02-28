using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{

    public GameObject cutsceneObj;
    private GameObject destination;
    public Animation cutscene;

    public bool flag_cutscene_done = true;

    // Start is called before the first frame update
    void Awake()
    {
        flag_cutscene_done = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(flag_cutscene_done)
        {
            EndCutscene();
        }
    }


    public void StartCutscene()
    {
        cutscene.Play();
    }


    private void EndCutscene()
    {
        //destination.SetActive(true);
        cutsceneObj.SetActive(false);
        PlayerInput.instance.UpdateBackplane();

    }
}
