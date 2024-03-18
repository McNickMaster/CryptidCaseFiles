using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public static LocationManager instance;
    public View defaultView;

    public View[] views;
    //public Light myDirectional;
    
    void Awake()
    {
        
        GameManager.instance.views = views;
        GameManager.instance.currentLocation = this;
        GameManager.instance.SetNewView(defaultView);
        
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
