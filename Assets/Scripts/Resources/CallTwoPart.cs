using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallTwoPart : MonoBehaviour
{
    public Button pickUp;
    public GameObject ringRing, message;

    void Start()
    {
        pickUp.onClick.AddListener(PickUp);
        ringRing.SetActive(true);
        message.SetActive(false);
    }

    void PickUp()
    {
        ringRing.SetActive(false);
        message.SetActive(true);
    }
}
