using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    public Camera myCamera;
    public Transform myBackPlane;
    public Vector3 normal;


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
        myCamera.gameObject.SetActive(true);
    }

    void OnDisable()
    {
        myCamera.gameObject.SetActive(false);
    }


}
