using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUIStateName
{
    public static string GunMagnetic = "gun.magnetic";
    public static string GunDamageAble = "gun.damageable";
}

public class UIController : MyElement
{
    public Text GunType;
    public Text BulletNum;
    public Text MagneticType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeGunUIState(string gunType,string gunName,object bullet)
    {
        try
        {
            if (gunType == GunUIStateName.GunMagnetic)
                ChangeToGunMagnetic(gunName, (int)bullet);
            else if (gunType == GunUIStateName.GunDamageAble)
                ChangeToGunDamageable(gunName, (Nullable<bool>)bullet);
        }
        catch(System.InvalidCastException)
        {
            Debug.Log("ui接受信息的时候第三个参数不对");
        }
    }

    private void ChangeToGunMagnetic(string gunNanme,int num)
    {
        ChangeGunName(gunNanme);
        ChangeGunNum(num);
    }

    private void ChangeToGunDamageable(string gunName,Nullable<bool> bullet)
    {
        ChangeGunName(gunName);
        ChangeMagneticType(bullet);
    }

    private void ChangeGunName(string name)
    {
        GunType.text = "枪械类型:" + name;
    }

    private void ChangeGunNum(int num)
    {
        MagneticType.gameObject.SetActive(false);
        BulletNum.gameObject.SetActive(true);

        BulletNum.text = "剩余子弹:" + num;
    }

    private void ChangeMagneticType(Nullable<bool> isn)
    {
        BulletNum.gameObject.SetActive(false);
        MagneticType.gameObject.SetActive(true);

        string type= "磁极类型:";
        if (isn == true)
            type += "N极";
        else if (isn == false)
            type += "S极";
        else
            type += "消去磁极";

        MagneticType.text = type;
    }
}
