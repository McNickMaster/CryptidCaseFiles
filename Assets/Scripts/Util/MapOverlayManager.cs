using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapOverlayManager : MonoBehaviour
{

    public GameObject[] overlays;
    [SerializeField]
    private GameObject currentOverlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetNewOverlay(int i)
    {
        currentOverlay.SetActive(false);
        currentOverlay = overlays[i];
        currentOverlay.SetActive(true);
    }

    void OnEnable()
    {
        currentOverlay = overlays[0];
        SetNewOverlay(CurrentSceneOverlayIndex());
    }

    void OnDisable()
    {
        currentOverlay.SetActive(false);
    }

    int CurrentSceneOverlayIndex()
    {
        return (int)GameManager.instance.currentScene;
    }


}
