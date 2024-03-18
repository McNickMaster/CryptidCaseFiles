using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeViewButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeViewCork()
    {
        GameManager.instance.SetView(0);
    }
    public void ChangeViewDesk()
    {
        GameManager.instance.SetView(1);
    }
}
