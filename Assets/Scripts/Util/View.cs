using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    public Camera myCamera;
    public Transform myBackPlane;
    public Vector3 normal;

    public Transform TL, BR;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 InBounds(Vector3 pos)
    {

        if(TL == null)
        {
            return pos;
        }   


        bool flag, inX, inY;
        float x = pos.x, y = pos.y, clampedZ;
        inX = (x < BR.position.x && x > TL.position.x);
        inY = (y < TL.position.y && y > BR.position.y);
        flag = inX && inY;

        //clampedZ = Mathf.Clamp(pos.z, myBackPlane.position.z - 0.3f, myBackPlane.position.z);
        clampedZ = myBackPlane.position.z - 0.3f;

        if(flag)
        { 
            return new Vector3(pos.x, pos.y, clampedZ);
        }
         //if not in bounds then continue


        if(inX)
        {
            return new Vector3(pos.x, Mathf.Clamp(pos.y, BR.position.y, TL.position.y), clampedZ);
        }
       
        if(inY)
        {
            return new Vector3(Mathf.Clamp(pos.x, TL.position.x, BR.position.x), pos.y, clampedZ);
        }

        return new Vector3(Mathf.Clamp(pos.x, TL.position.x, BR.position.x), Mathf.Clamp(pos.y, BR.position.y, TL.position.y), clampedZ);

    
    }

    public Vector3 InBoundsTop(Vector3 pos)
    {

        if(TL == null)
        {
            return pos;
        }   


        bool flag, inX, inZ;
        float x = pos.x, z = pos.z, clampedY;
        inX = (x < BR.position.x && x > TL.position.x);
        inZ = (z < TL.position.z && z > BR.position.z);
        flag = inX && inZ;

        //clampedZ = Mathf.Clamp(pos.z, myBackPlane.position.z - 0.3f, myBackPlane.position.z);
        clampedY = myBackPlane.position.y + 0.3f;

        if(flag)
        { 
            return new Vector3(pos.x, clampedY, pos.z);
        }
         //if not in bounds then continue


        if(inX)
        {
            return new Vector3(pos.x, clampedY, Mathf.Clamp(pos.z, BR.position.z, TL.position.z));
        }
      
        if(inZ)
        {
            return new Vector3(Mathf.Clamp(pos.x, TL.position.x, BR.position.x), clampedY, pos.z);
        }

        return new Vector3(Mathf.Clamp(pos.x, TL.position.x, BR.position.x), clampedY, Mathf.Clamp(pos.z, BR.position.z, TL.position.z));

    
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
