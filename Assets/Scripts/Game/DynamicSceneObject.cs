using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSceneObject : MonoBehaviour
{

    public Milestone myMilestone;
    


    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            transform.GetChild(0).gameObject.SetActive(GameManager.instance.CheckMilestone(myMilestone));
            //Debug.Log(GameManager.instance.CheckMilestone(myMilestone));
        }
    }
}
