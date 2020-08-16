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
                ChangeToGunMagnetic(gunName, (Nullable<bool>)bullet);
            else if (gunType == GunUIStateName.GunDamageAble)
                ChangeToGunDamageable(gunName, (int)bullet);
        }
        catch(InvalidCastException e)
        {
            Debug.Log("枪的ui的数据转换不对");
        }
    }

    private void ChangeToGunMagnetic(string gunNanme,Nullable<bool> num)
    {
        ChangeGunName(gunNanme);
        ChangeMagneticType(num);
      
    }

    private void ChangeToGunDamageable(string gunName,int bullet)
    {
        ChangeGunName(gunName);
        ChangeGunNum(bullet);
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
