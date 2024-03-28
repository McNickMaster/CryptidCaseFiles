using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    
    public LineRenderer line;
    public MeshCollider meshCollider;
    
    private Vector3[] linePos = new Vector3[5];
    private int lineIndex = 0;

    public int maxIndex = 2;
/**

    okay so the problem with this implementation is that it should only connect to other tacks, but it just reacts to any click for the second tack. 
    this code might be better off in PlayerINput, where they could detect whether or not the second click is on a tack. 
    it def should NOT be in InteractableButton, because the line drawing stuff is NOT a button.


**/

    void Awake()
    {
        line.positionCount = 2;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
        BakeMesh(); // this is so sketchy, i sohuldnt do this every frame !!!!!
    }

    public void SetPoint(int index, Vector3 pos)
    {
        line.SetPosition(index, pos + (Vector3.forward * -0.25f));

    }

    public void SetNextPoint(Vector3 pos)
    {
        
        if(lineIndex < maxIndex)
        {
            //line.positionCount = lineIndex + 1;
            SetPoint(lineIndex, pos);
            lineIndex++;
        } else 
        {
            //this.enabled = false;
        }
    }

    public void DisableLineDraw()
    {
        if(Vector3.Distance(line.GetPosition(0), line.GetPosition(1) )< 2)
        {
            Destroy(this.gameObject);
        } else 
        {
            BakeMesh();
            
        }

   }

   public void BakeMesh()
   {
        Mesh mesh = new Mesh();
        line.BakeMesh(mesh, GameManager.instance.currentView.myCamera, true);

        meshCollider.sharedMesh = mesh;
   }

    public void ClearLine()
    {
        line.positionCount = 0;
        lineIndex = 0;
        
    }

    public void DestroyLine()
    {
        Destroy(this.gameObject);
    }


}
