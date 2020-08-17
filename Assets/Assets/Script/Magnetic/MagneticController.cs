using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理所有的磁力物体
/// </summary>
public class MagneticController : MyElement
{
    public float distanceRatio;//距离对磁力的影响系数
    public float MessRatio;//质量对磁力的影响系数
    public float MaxForce;//最大磁力，以防物体过近然后弹飞
    public float MinForce;//最小磁力，太小了谁都不动怪尴尬
    public float MaxDistance;//最大距离，超过这个距离不做计算

    private List<MagneticObject> mObjects;
    // Start is called before the first frame update

    private void Awake()
    {
        mObjects = new List<MagneticObject>();
    }

    private void FixedUpdate()
    {

        //计算所有物体之间的磁力并添加
        //遍历，做两层循环，确保每个物体都会和其他所有物体计算一次磁力
        foreach (MagneticObject o1 in mObjects)
        {
            foreach (MagneticObject o2 in mObjects)
            {
                //如果是同一个物体,不做计算
                if (mObjects.IndexOf(o1) != mObjects.IndexOf(o2))
                {
                    //如果太远了,就不做计算
                    Vector3 direction = o1.transform.position - o2.transform.position;
                    if (direction.magnitude < MaxDistance)
                    {
                        //如果有磁体没有磁力，就不做计算
                        if (o1.isN != null && o2.isN != null)
                        {  //计算并添加磁力
                            Vector3 force1To2 = ComputeMagneticForce(o1, o2, direction);
                            o2.GetComponent<Rigidbody>().AddForce(force1To2);
                            //Debug.Log("力的大小"+ force1To2.x+","+ force1To2.y +"," + force1To2.z);
                        }
                    }
                }
            }

        }
    }

   

    /// <summary>
    /// 计算得到o1对o2的力的向量，公式为=distanceRatio * distance + mess1 * mess1 * mess2 * messratio
    /// </summary>
    /// <param name="o1"></param>
    /// <param name="o2"></param>
    /// <returns></returns>
    public Vector3 ComputeMagneticForce(MagneticObject o1, MagneticObject o2,Vector3 direction)
    {
        float force = Time.fixedDeltaTime * (distanceRatio / direction.magnitude + Mathf.Pow(o1.Mess, 2) * o2.Mess * MessRatio);

        if (force > MaxForce)
            force = MaxForce;
        else if (force < MinForce)
            force = MinForce;

        Vector3 f = direction.normalized * force;

        Nullable<bool> bool1 = o1.isN;
        Nullable<bool> bool2 = o2.isN;

        if (bool1 != bool2)
            return f;
        else
            return -f;

    }
    /// <summary>
    /// 磁体awake时候磁体会调用这个来注册到list里面
    /// </summary>
    /// <param name="obj"></param>
    public void RegistObject(MagneticObject obj)
    {
        mObjects.Add(obj);
        Debug.Log("RegistObject 成功");
    }

    /// <summary>
    /// 磁体destroy时候磁体会调用这个取消注册
    /// </summary>
    /// <param name="obj"></param>
    public void LogOut(MagneticObject obj)
    {
        mObjects.Remove(obj);
    }
}
