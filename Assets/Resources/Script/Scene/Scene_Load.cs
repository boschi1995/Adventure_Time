using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Load : SceneManager
{
    private UIProgressBar ProgressBar;
    private AsyncOperation Operation;

    protected override void Awake()
    {
        base.Awake();
        if(string.IsNullOrEmpty(GameManager.AsyncScene))
        {
            GameManager.AsyncScene = "Scene_Main";
        }
    }

    private void Start()
    {
        StartCoroutine(AsnyLoad());
    }

#if UNITY_ANDROID


#else

    private void FixedUpdate()
    {
        if (ProgressBar == null) { return; }
        if (Input.anyKey && ProgressBar.value <= 1)
        {
            GameManager.AsyncScene = "";
            Operation.allowSceneActivation = true;
        }
    }

    IEnumerator AsnyLoad()
    {
        ProgressBar = GameObject.Find("ProgressBar").GetComponent<UIProgressBar>();
        ProgressBar.value = 0;
        Operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(GameManager.AsyncScene);
        Operation.allowSceneActivation = false;

        while(!Operation.isDone)
        {
            ProgressBar.value = Operation.progress + 0.1f;
            yield return null;
        }
    }

#endif
}
