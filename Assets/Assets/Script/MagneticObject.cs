using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

/// <summary>
/// 磁性物体，分为N极、S极、无极三个状态。磁力的处理由controller控制
/// </summary>
public class MagneticObject : MyElement
{
    //因为Nullable<bool>isn不可序列化，所以用这两个来替代一下。
    public bool HeIsN;
    public bool heee;

    public float MessRatio;

    [SerializeField]
    private Nullable<bool> isn;
    

    ///三个状态，是N极，不是N极，不含有磁力
    public Nullable<bool> isN {
        get { return isn; }
        set
        {
            Debug.Log("想换磁性");
            Debug.Log( ((isn.Equals(true))?"N":"S/null") + "." + ((value==true) ? "N" : "S/null"));
            if (value != isn)
            {
                isn = value;
                Debug.Log("换了磁性");
                ChangeCollor();
                //PlaySound();
                //PlayAnime();
                //PlayShader();
            }
        }
    }


    public float Mess{
        get{return gameObject.GetComponent<Rigidbody>().mass*MessRatio;}}

    private void Start()
    {
        app.magneticController.RegistObject(this);
    }

    private void ChangeCollor()
    {
        if (isn == true)
            //gameObject.GetComponent<Renderer>().material.color = Color.blue;
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);

        else if (isn == false)
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        else if (isn == null)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.gray;
        }
    }

    private void OnDestroy()
    {
        app.magneticController.LogOut(this);
    }

    private void Update()
    {
        heisn();
    }

    /// <summary>
    /// 因为Nullable物体不会再inspector上显示，所以用这个来代替一下
    /// </summary>
    void heisn()
    {
        if(isN!=null)
        {
            isN = HeIsN;
        }
        else if (heee != HeIsN)
        {
           
            isN = HeIsN;
        }
        heee = HeIsN;
    }
}
