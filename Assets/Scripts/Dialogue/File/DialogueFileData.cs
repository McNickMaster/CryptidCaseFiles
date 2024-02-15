using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class DialogueFileData
{

    public List<Slide> slides = new List<Slide>();
    public List<Branch> branches = new List<Branch>();

    public DialogueFileData (List<Branch> branches, List<Slide> slides)
    {

        this.slides = slides;
        this.branches = branches;

    }

    public List<Slide> GetSlides()
    {
        return slides;
    }

    public List<Branch> GetBranches()
    {
        return branches;
    }


}
