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
        UpdateObject();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void UpdateObject()
    {
        transform.GetChild(0).gameObject.SetActive(GameManager.instance.CheckMilestone(myMilestone));
    }

    void OnEnable()
    {
        
    }
}
