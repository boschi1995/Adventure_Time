using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scene;

public class Scene_Runing : SceneManager
{
    public int Life;
    public Transform Pololing;

    protected override void Awake()
    {
        base.Awake();
        Awake_Object();
        Custom.Init(this.mPlayer);
        StartCoroutine(GameUpdata());
    }

    void Awake_Object()
    {
        mPlayer = GameObject.Find("Player").GetComponent<Transform>();
        Pololing = GameObject.Find("Pooling").GetComponent<Transform>();
    }

    IEnumerator GameUpdata()
    {
        int max = 6;
        WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
        
        while (Life != 0)
        {
            Poling_Setting(max * 2, Pololing.GetChild(1).childCount);

            for(int i = 0; i < Pololing.childCount; i++)
            {
                // Ç®¸µ 


            }
            yield return fixedUpdate;
        }
    }

    private void Poling_Setting(int full, int state)
    {
        int number = 0;
        GameObject pool;

        while(state < full)
        {
            if (state != 0) { number = Random.Range(0, 8); }
            pool = Instantiate(Resources.Load<GameObject>("Prefab/Scene_Running/Patten_" + number),Vector3.back*20,Quaternion.identity);
            pool.transform.SetParent(Pololing.GetChild(1));
            pool.SetActive(false);
            state++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
         
    }
}
