using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public static LocationManager instance;
    public View defaultView;
    //public Light myDirectional;
    
    void Awake()
    {
        GameManager.instance.SetNewLocation(this);
        
    }

    void Load()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        //myDirectional.enabled = true;
    }
    
    void OnDisable()
    {
        //myDirectional.enabled = false;
    }
}
