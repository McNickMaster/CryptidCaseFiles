using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{

    public GameObject objToBlink;

    public float blinkDuration, blinkFrequency;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(BlinkIn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BlinkOut()
    {
        objToBlink.SetActive(false);
        yield return new WaitForSeconds(blinkDuration);
        StartCoroutine(BlinkIn());
    }
    IEnumerator BlinkIn()
    {
        objToBlink.SetActive(true);
        yield return new WaitForSeconds(blinkFrequency);
        StartCoroutine(BlinkOut());
    }
}
