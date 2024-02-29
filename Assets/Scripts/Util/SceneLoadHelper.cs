using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadHelper : MonoBehaviour
{
    public static SceneLoadHelper instance;

    string to, from;


    void Awake()
    {
        //instance = this;
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
        to = s;

        StartCoroutine(LoadSceneA());

        from = s;
    }

    public void LoadNewScene(Scene from, Scene to)
    {
        this.from = from.name;
        this.to = to.name;

        StartCoroutine(UnloadSceneA());
    }

    public void LoadNewScene(string s)
    {
        to = s;

        StartCoroutine(UnloadSceneA());

        from = s;
    }

    IEnumerator UnloadSceneA()
    {
        AsyncOperation op = SceneManager.UnloadSceneAsync(from);
    
        while(!op.isDone)
        { 
            yield return null;
        }
        
        StartCoroutine(LoadSceneA());
    }
    IEnumerator LoadSceneA()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);

        while(!op.isDone)
        {
            yield return null;
        }
    }
}
