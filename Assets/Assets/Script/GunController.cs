﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪控制器，有枪们的引用，负责枪械相关事件，从controller哪里接受事件
/// </summary>
public class GunController : MyElement
{
    public List<GameObject> guns;
    [SerializeField]
    private int currentGunNum;
    private GunPrototype currentGunModule;


    void Start()
    {
        currentGunNum = 0;
    }

    void Update()
    {
        
    }

    public void Shot()
    {
        currentGunModule.Shot();
        Debug.Log("接收到了射击信息");
    }


    public void Reload()
    {
        currentGunModule.Reload();
        Debug.Log("接收到了换单信息");
    }

    public void ChangeGun()
    {
        ChangeGunState();
        currentGunModule.GrabThisGun();

        Debug.Log("接收到了换枪信息");
    }

    /// <summary>
    /// 切换当前枪的引用和序号，不用的枪隐藏，用的枪激活
    /// </summary>
    private void  ChangeGunState()
    {
        guns[currentGunNum].SetActive(false);

        if (currentGunNum + 1 == guns.Count)
            currentGunNum = 0;
        else
            currentGunNum++;

       guns[currentGunNum].SetActive(true);
        currentGunModule = guns[currentGunNum].GetComponent<GunPrototype>();
    }



}
