﻿using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPrototype : MonoBehaviour
{



    public GameObject BulletImpact;
    // Start is called before the first frame update
    void Start()
    {
        BulletImpact.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual protected void OnCollisionEnter(Collision collision)
    {
        Debug.Log("基类碰上了");
    }

}