using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadHelper : MonoBehaviour
{
    public static SceneLoadHelper instance;

    Scene to, from;
    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadFirstScene(string s)
    {
        this.from = from;
        this.to = to;

        StartCoroutine(LoadSceneA());
    }

    public void LoadNewScene(Scene from, Scene to)
    {
        this.from = from;
        this.to = to;

        StartCoroutine(UnloadSceneA());
    }

    IEnumerator UnloadSceneA()
    {
        AsyncOperation op = SceneManager.UnloadSceneAsync(from);

        while(!op.isDone)
        { 
            StartCoroutine(LoadSceneA());
            yield return null;
        }
    }
    IEnumerator LoadSceneA()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(to.name, LoadSceneMode.Additive);

        while(!op.isDone)
        {
            yield return null;
        }
    }
}
