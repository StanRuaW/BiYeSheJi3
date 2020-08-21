using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
//TODO:可以做个检测，看父类的scale变没变
/// <summary>
/// 磁性物体，分为N极、S极、无极三个状态。磁力的处理由controller控制
/// </summary>
public class MagneticObject : MyElement
{
    [Header("磁力计算参数")]
    public float distanceRatio;//距离对磁力的影响系数
    public float MessRatio;//质量对磁力的影响系数
    public float MaxForce;//最大磁力，以防物体过近然后弹飞
    public float MinForce;//最小磁力，太小了谁都不动怪尴尬
    public float RangeDistance;//最大距离，超过这个距离不做计算

    [Header("其他")]
    [SerializeField]
    private Nullable<bool> isn;

    private float mass;

    [SerializeField] private GameObject magneticRange;

    ///三个状态，是N极，不是N极，不含有磁力
    public Nullable<bool> isN
    {
        get { return isn; }
        set
        {
            if (value != isn)
            {
                isn = value;
                ChangeCollor();
                Debug.Log("磁性被修改了");
                SetMagneticRange();
                //PlaySound();
                //PlayAnime();
                //PlayShader();
            }
        }
    }


    private void Start()
    {
        app.magneticController.RegistObject(this);
        mass = gameObject.GetComponent<Rigidbody>().mass;

        SetMagneticRange();

    }

    private void ChangeCollor()
    {
        if (isn == true)
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

    public void AddMagneticForce(GameObject other)
    {
        Vector3 force = ComputeMagneticForce(gameObject.GetComponent<MagneticObject>(), other.GetComponent<MagneticObject>());

        other.gameObject.GetComponent<Rigidbody>().AddForce(force);
    }

    private Vector3 ComputeMagneticForce(MagneticObject o1, MagneticObject o2)
    {
        if (o1.isN == null || o2.isN == null)
            return new Vector3(0, 0, 0);
        if (o1.Equals(o2))
            return new Vector3(0, 0, 0);

        Vector3 direction = o1.gameObject.transform.position - o2.gameObject.transform.position;

        float force = Time.fixedDeltaTime * (distanceRatio / direction.magnitude + Mathf.Pow(o1.mass, 2) * o2.mass * MessRatio);

        if (force > MaxForce)
            force = MaxForce;
        else if (force < MinForce)
            force = MinForce;

        Vector3 f = direction.normalized * force;

        Nullable<bool> bool1 = o1.isN;
        Nullable<bool> bool2 = o2.isN;

        Debug.Log(f + "," + o1.gameObject.name + "," + o2.gameObject.name);

        if (bool1 != bool2)
            return f;
        else
            return -f;
    }

    private void SetMagneticRange()
    {
        float X = transform.localScale.x * (RangeDistance + 1f);
        float Y = transform.localScale.y * (RangeDistance + 1f);
        float Z = transform.localScale.z * (RangeDistance + 1f);

        magneticRange.GetComponent<MagneticRange>().SetScale(X, Y, Z);
        ActiveRange();

    }

    private void ActiveRange()
    {
        if (isn == null)
            magneticRange.GetComponent<MagneticRange>().HideMaterial();
        else
        {
            magneticRange.GetComponent<MagneticRange>().ShowMaterial();
            if (isn == true)
                magneticRange.GetComponent<MagneticRange>().ChangeColor(Color.blue);
            else
                magneticRange.GetComponent<MagneticRange>().ChangeColor(Color.red);

        }
    }

    public void SetCalculateParam(float dR, float mR, float minF, float maxF, float rD)
    {
        distanceRatio = dR;
        MessRatio = mR;
        MaxForce = maxF;
        MinForce = minF;
        RangeDistance = rD;
    }
}
