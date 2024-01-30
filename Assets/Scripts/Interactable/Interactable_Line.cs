using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_Line : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public override void Interact()
    {

        Destroy(this.gameObject);


    }
}
