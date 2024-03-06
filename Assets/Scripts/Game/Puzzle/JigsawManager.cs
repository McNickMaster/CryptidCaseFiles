using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawManager : MonoBehaviour
{
    public static JigsawManager instance;
    public GameObject jigsawParent;
    public Interactable_PuzzleObject[] jigsaw_pieces;
    //public Transform[] spawnBounds;

    void Awake()
    {
        instance = this;
        //jigsawParent.SetActive(false);
/*
        Vector3 TR, BL;
        TR = spawnBounds[0].localPosition;
        BL = spawnBounds[1].localPosition;

        Vector3 spawnPosition;
        float x, z;
        foreach(Interactable_PuzzleObject p in jigsaw_pieces)
        {
            x = Random.Range(BL.x, TR.x);
            z = Random.Range(BL.z, TR.z);
            spawnPosition = new Vector3(x, -2.5f, z);

            //p.GetComponent<Rigidbody>().isKinematic = true;
            //p.transform.localPosition = spawnPosition;
        }
        */

        

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void CheckPuzzle()
    {
        if(IsPuzzleSolved())
        {
            Debug.Log("yeowwwwww i solved the puzzle");
            GameEvents.instance.Event_Puzzle2_Done.Invoke();
        }
    }

    bool IsPuzzleSolved()
    {
        bool flag = true;
        foreach(Interactable_PuzzleObject puzz in jigsaw_pieces)
        {
            if(puzz.locked)
            {

            } else 
            {
                flag = false;
            }
        }

        return flag;
    }



}
