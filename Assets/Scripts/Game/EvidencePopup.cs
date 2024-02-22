using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EvidencePopup : MonoBehaviour
{
    public static EvidencePopup instance;

    public TextMeshProUGUI title_textBox;
    public TextMeshProUGUI id_textBox;
    public Animator animator;

    string evidenceID = "";

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn(string evidenceID)
    {
        id_textBox.text = evidenceID;
        Spawn();
    }
    public void Spawn(string title, string evidenceID)
    {
        title_textBox.text = title + " Found!";
        Spawn(evidenceID);
    }
    public void Spawn()
    {
        
        transform.GetChild(0).gameObject.SetActive(true);
        animator.SetTrigger("start");
        SoundManager.instance.PlaySFX_Delay(GameData.instance.evidenceFound, 0.5f);
    }
    public void Despawn()
    {
        
        transform.GetChild(0).gameObject.SetActive(false);
    }
    
}
