using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MyElement
{
    public GunController gunController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    override public void OnNotification(string eventName, UnityEngine.Object obj, params object[] data)
    {
        switch (eventName)
        {
            case "try.shot":
                gunController.Shot();

                break;

            case "try.change.gun":
                gunController.ChangeGun();
                break;
        }
    }

}