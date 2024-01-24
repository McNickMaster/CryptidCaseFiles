using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextDay : MonoBehaviour
{
    public Button buttonNextDay;
    private GameObject goingHome;
    public static int dayNum;

    void Start()
    {
        buttonNextDay.onClick.AddListener(ToNextDay);
        goingHome = GameObject.FindWithTag("Travel");
    }

    private void Update()
    {
        
    }

    void ToNextDay()
    {
        goingHome.transform.GetChild(0).gameObject.SetActive(true);
        dayNum += 1;
        Debug.Log(dayNum);
    }
}
