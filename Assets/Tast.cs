using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tast : MonoBehaviour
{
    // Start is called before the first frame update

    public List<int> InputList;

    void Start()
    {
        for(int i = 0; i < 100; i++) { InputList.Add(i); }

        for (int i =0; i < InputList.Count; i++)
        {
            if(!(InputList[i] % 2 == 1))
            {
                Debug.Log(i); 
            }
        }
    }
}
