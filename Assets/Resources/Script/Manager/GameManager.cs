using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Customs;

public class GameManager : MonoBehaviour
{
    public static GameManager OnlyClass;
    public string AsyncScene;
    public Custom PlayerCustoms;
    private SoundManager Sound;
    public SceneManager Scene;

    protected void Awake()
    {
        Singleturn();
        GetManager();
    }

    private void Singleturn()
    {
        if (OnlyClass != null)
        {
            Destroy(gameObject);
            return;
        }
        OnlyClass = this;
        DontDestroyOnLoad(gameObject);
        OnlyClass.gameObject.name = "GameManager";
    }

    private void GetManager()
    {
        Sound = transform.Find("SoundManager").GetComponent<SoundManager>();
        PlayerCustoms = Resources.Load<Custom>("ScripTableObject/PlayerCustom");
    }

    public void SceneChange(string Scene)
    {
        AsyncScene = Scene;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene_Load");
    }
}
