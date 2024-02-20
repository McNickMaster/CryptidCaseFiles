using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawManager : MonoBehaviour
{

    public GameObject jigsawParent;
    public Interactable_PuzzleObject[] jigsaw_pieces;
    //public Transform[] spawnBounds;

    void Awake()
    {
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
    void Update()
    {
        
    }



}
