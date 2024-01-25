using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnify : MonoBehaviour
{
    public float z = 19f;
    public GameObject myGlass;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //shitty implementation, fix this to be event based later
        myGlass.SetActive(Input.GetKey(KeyCode.LeftShift));

        Vector3 mousePos = PlayerInput.instance.GetConvertedMousePos(z);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);

        //myGlass.transform.LookAt(PlayerInput.instance.cam.transform.position);
    }

}
