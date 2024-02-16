using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SaveData
{

    public string[] milestones;

    public SaveData (GameManager game)
    {
        milestones = new string[game.completedMilestones.Count];
        for(int i = 0; i < milestones.Length; i++)
        {
            milestones[i] = game.completedMilestones[i].ToString();
        }

    }

    public List<Milestone> GetMilestones()
    {
        List<Milestone> milestoneList = new List<Milestone>();
        Milestone m = new Milestone();

        for(int i = 0; i < milestones.Length; i++)
        {
            
            if(Enum.TryParse<Milestone>(milestones[i], out m))
            {   
                milestoneList.Add(m);
            }
        }

        return milestoneList;
    }


}



