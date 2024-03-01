using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleComplete : MonoBehaviour
{


    void Update()
    {
        if (GameObject.Find("PuzzlePiece").GetComponent<Interactable_PuzzleObject>().locked && GameObject.Find("PuzzlePiece (2)").GetComponent<Interactable_PuzzleObject>().locked && GameObject.Find("PuzzlePiece (3)").GetComponent<Interactable_PuzzleObject>().locked && GameObject.Find("PuzzlePiece (4)").GetComponent<Interactable_PuzzleObject>().locked && GameObject.Find("PuzzlePiece (5)").GetComponent<Interactable_PuzzleObject>().locked && GameObject.Find("PuzzlePiece (6)").GetComponent<Interactable_PuzzleObject>().locked && GameObject.Find("PuzzlePiece (7)").GetComponent<Interactable_PuzzleObject>().locked && GameObject.Find("PuzzlePiece (8)").GetComponent<Interactable_PuzzleObject>().locked && GameObject.Find("PuzzlePiece (9)").GetComponent<Interactable_PuzzleObject>().locked && GameObject.Find("PuzzlePiece (10)").GetComponent<Interactable_PuzzleObject>().locked && GameObject.Find("PuzzlePiece (11)").GetComponent<Interactable_PuzzleObject>().locked && GameObject.Find("PuzzlePiece (12)").GetComponent<Interactable_PuzzleObject>().locked)
        {
            Debug.Log("Yippee");
        }
    }
}
