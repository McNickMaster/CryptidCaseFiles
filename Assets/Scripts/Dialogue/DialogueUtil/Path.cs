using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Path
{
    public Slide[] slides;
    public Slide firstSlide, endSlide;
    [HideInInspector]
    public Branch myBranch;

    public PathEndBehaviour pathEndBehaviour = PathEndBehaviour.CONTINUE;
    public string gotoID = "";
    public Branch endBranch;

    public bool locked = false;
    public bool chosenBefore = false;
    public string unlockPathID = "", lockPathID = "", milestoneID = "";
    


    public Path(Slide[] slides)
    {
        this.slides = slides;
        firstSlide = slides[0];
        endSlide = slides[slides.Length-1];

        DetectPathEnd();
    }

    public void DetectPathEnd()
    {
        //Debug.Log(endSlide.Body);

        locked = firstSlide.Body.Contains("[locked]");
        if(locked)
        {
            firstSlide.Body = firstSlide.Body.Substring(8, firstSlide.Body.Length-8);
        }

        //endSlide.Body.Replace("\"","");
        if(endSlide.Body.Contains("[unlock"))
        { 
            int index = endSlide.Body.IndexOf("[unlock")+7;
            unlockPathID = endSlide.Body.Substring(index, endSlide.Body.Length - index - 1);
            endSlide.Body = endSlide.Body.Substring(0, endSlide.Body.Length - unlockPathID.Length - 8);
 //           Debug.Log(unlockPathID);
//            Debug.Log("endSlide after unlock removal: " + endSlide.Body);
        }
       

        if(endSlide.Body.Contains("[lock"))
        {
            int index = endSlide.Body.IndexOf("[lock")+5;
            lockPathID = endSlide.Body.Substring(index, endSlide.Body.Length - index - 1);
            endSlide.Body = endSlide.Body.Substring(0, endSlide.Body.Length - unlockPathID.Length - 5);
            //Debug.Log(endSlide.Body);
        }
        

        if(endSlide.Body.Contains("[end]"))
        {
            pathEndBehaviour = PathEndBehaviour.END;

            int index = endSlide.Body.IndexOf("[end]");
            endSlide.Body = endSlide.Body.Substring(0, index);
            //Debug.Log(endSlide.Body);
        } else if(endSlide.Body.Contains("[back"))
        {
            pathEndBehaviour = PathEndBehaviour.GOTO;
            //whole section     Debug.Log(endSlide.Body.Substring(endSlide.Body.IndexOf("[back"), endSlide.Body.Length - endSlide.Body.IndexOf("[back")));
            int index = endSlide.Body.IndexOf("[back")+5;
            gotoID = endSlide.Body.Substring(index, endSlide.Body.Length - index - 1) + "1";
//            Debug.Log("gotoID: " + gotoID);
            endSlide.Body = endSlide.Body.Substring(0, endSlide.Body.Length - gotoID.Length - 5);
            //Debug.Log(endSlide.Body);
        } else 
        {
//            Debug.Log("back not found in " + endSlide.ID);
            gotoID = endSlide.ID + "1";
            pathEndBehaviour = PathEndBehaviour.CONTINUE;
        }

        
        if(endSlide.Body.Contains("[get"))
        {
            int index = endSlide.Body.IndexOf("[get")+4;
            milestoneID = endSlide.Body.Substring(index, endSlide.Body.Length - index -1);
            endSlide.Body = endSlide.Body.Substring(0, endSlide.Body.Length - milestoneID.Length - 5);
            //Debug.Log(endSlide.Body);
        }


        endSlide.Body.Replace("[","");
        endSlide.Body.Replace("]","");

    }
    




}

public enum PathEndBehaviour 
{
    END, CONTINUE, GOTO
}
