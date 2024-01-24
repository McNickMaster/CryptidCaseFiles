using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public Button close;

    private void Start()
    {
        close.onClick.AddListener(Close);
    }
    void Close()
    {
        Destroy(gameObject);
    }
}
