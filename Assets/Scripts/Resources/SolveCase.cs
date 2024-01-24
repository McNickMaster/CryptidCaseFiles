using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SolveCase : MonoBehaviour
{
    public Button solve, close;
    public GameObject Newspaper;
    public GameObject AnnoyedCall, AngryCall;
    public GameObject GoneWhenSolved, InputText;
    public TMP_Text CulpritPrint, DeathPrint;
    private GameObject Parent, PhoneParent;

    [SerializeField] private TMP_Dropdown dropdownCryptid;
    [SerializeField] private TMP_Dropdown dropdownDeath;

    void Start()
    {
        solve.onClick.AddListener(ClickedSolve);
        Parent = GameObject.FindWithTag("NewspaperParent");
        PhoneParent = GameObject.FindWithTag("PhoneParent");
        close.onClick.AddListener(Close);
    }

    void ClickedSolve()
    {
        if (dropdownCryptid.value == 1 && dropdownDeath.value == 2)
        {
            Debug.Log("Correct!");
            Instantiate(Newspaper, Parent.transform.position, Parent.transform.rotation, Parent.transform);
        }
        else if (dropdownCryptid.value == 1 || dropdownDeath.value == 2)
        {
            Debug.Log("You got one right");
            Instantiate(AnnoyedCall, PhoneParent.transform.position, PhoneParent.transform.rotation, PhoneParent.transform);
        }
        else
        {
            Debug.Log("Both wrong");
            Instantiate(AngryCall, PhoneParent.transform.position, PhoneParent.transform.rotation, PhoneParent.transform);
        }
        CulpritPrint.text = (dropdownCryptid.options[dropdownCryptid.value].text);
        DeathPrint.text = (dropdownDeath.options[dropdownDeath.value].text);
        GoneWhenSolved.SetActive(false);
        InputText.SetActive(true);
    }
    void Close()
    {
        Destroy(gameObject);
    }
}
