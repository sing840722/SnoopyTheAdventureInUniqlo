﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour
{
    public int CountEmpty()
    {
        int count = 0;
        Transform pTr = this.transform;
        foreach (Transform tr in pTr)
        {
            if (tr.tag == "Empty")
            {
                count++;
            }
        }
        return count;
    }

    public int GetCounter()
    {
        return CountEmpty();
    }
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}