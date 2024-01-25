using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempNode : MonoBehaviour
{
    public static TempNode instance;
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
