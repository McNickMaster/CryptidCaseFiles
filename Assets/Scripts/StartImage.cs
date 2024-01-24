using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartImage : MonoBehaviour
{
    public GameObject GameObject;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject.SetActive(true);
        StartCoroutine(Deletion());
    }

    private IEnumerator Deletion()
    {
        yield return new WaitForSeconds(4);
        GameObject.SetActive(false);
    }
}
