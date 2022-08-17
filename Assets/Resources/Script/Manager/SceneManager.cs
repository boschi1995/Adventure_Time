using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Customs;

public class SceneManager : MonoBehaviour
{
    protected GameManager GameManager { get { return GameManager.OnlyClass; } }
    protected Custom Custom { get { return GameManager.OnlyClass.PlayerCustoms; } }
    public Transform Canvas;
    public Transform mPlayer;

    protected virtual void Awake()
    {
        ManagerSearch();
        UISearch();
    }

    private void ManagerSearch()
    {
        if (GameManager == null) { Instantiate(Resources.Load<GameObject>("Prefab/GameManager")); }
        GameManager.Scene = this;
    }

    private void UISearch()
    {
        if (Canvas != null) { return; }
        string Ui = "Prefab/" + gameObject.name;
        if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Ui += "_PcUi";
        }
        else
        {
            Ui += "_AndroidUi";
        }

        try
        {
            Canvas = Instantiate(Resources.Load<GameObject>(Ui)).transform;
        }
        catch
        {
            Canvas = Instantiate(Resources.Load<GameObject>("Prefab/"+gameObject.name+"_AllUi")).transform;
        }
        Canvas.name = "Canvas";
    }
}
