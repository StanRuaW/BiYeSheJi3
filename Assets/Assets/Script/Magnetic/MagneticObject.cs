using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

/// <summary>
/// 磁性物体，分为N极、S极、无极三个状态。磁力的处理由controller控制
/// </summary>
public class MagneticObject : MyElement
{
    public float MessRatio;

    [SerializeField]
    private Nullable<bool> isn;
    

    ///三个状态，是N极，不是N极，不含有磁力
    public Nullable<bool> isN {
        get { return isn; }
        set
        {
            if (value != isn)
            {
                isn = value;
                ChangeCollor();
                Debug.Log("磁性被修改了");
                //PlaySound();
                //PlayAnime();
                //PlayShader();
            }
        }
    }


    public float Mess{
        get
        {
            return MessRatio;
            //return gameObject.GetComponent<Rigidbody>().mass*MessRatio;}
        }
    }

    private void Start()
    {
        app.magneticController.RegistObject(this);
    }

    private void ChangeCollor()
    {
        if (isn == true)
            //gameObject.GetComponent<Renderer>().material.color = Color.blue;
            gameObject.GetComponent<Renderer>().material.SetColor(Shader.PropertyToID("_BaseColor"), Color.blue);

        else if (isn == false)
            gameObject.GetComponent<Renderer>().material.SetColor(Shader.PropertyToID("_BaseColor"), Color.red);
        else if (isn == null)
        {
            gameObject.GetComponent<Renderer>().material.SetColor(Shader.PropertyToID("_BaseColor"), Color.gray);
        }
    }

    private void OnDestroy()
    {
        app.magneticController.LogOut(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.tag=="Player")
    }
}
