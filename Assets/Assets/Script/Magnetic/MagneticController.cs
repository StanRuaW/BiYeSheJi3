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

    private void Start()
    {
        foreach(MagneticObject m in mObjects)
        {
            m.SetCalculateParam(distanceRatio, MessRatio, MaxForce, MinForce, MaxDistance);
        }
    }

    //TODO公式可以做优化，具体好好的调试
    /// <summary>
    /// 计算得到o1对o2的力的向量，公式为=distanceRatio * distance + mess1 * mess1 * mess2 * messratio
    /// </summary>
    /// <param name="o1"></param>
    /// <param name="o2"></param>
    /// <returns></returns>

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
        //TODO：游戏结束时候这里会空引用，但是应该不影响
        mObjects.Remove(obj);
    }
}


