using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所有其他的controller类都要从这里引用，来互相通信和调用
//提供了一个方法用来：找到中央控制器的入口
public class MyElement : MonoBehaviour
{
    //中央控制器的入口，中央控制器可以通信和互相调用
    public MyApplication app { get { return GameObject.FindObjectOfType<MyApplication>(); } }

    /// <summary>
    /// 接受全局的消息，收到的消息每个controller自己处理
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="obj"></param>
    /// <param name="data"></param>
    virtual public void OnNotification(string eventName, Object obj, params object[] data) { }

}

/// <summary>
/// 中央控制器  
/// </summary>
public class MyApplication : MonoBehaviour
{
    public MagneticController magneticController;
    public PlayerController playerController;

    private List<MyElement> controllerList;

    private void Awake()
    {
        //检测当前挂的object是不是tag是Application，
        //如果不是，强制报错不让运行
        //我也不知道这么做有啥意义，但是感觉不这么做就会出事
        if("MyApplication"!=gameObject.tag)
            Debug.LogError("中央控制器挂载的tag不对，请调整");

        controllerList = new List<MyElement>();
    }

    private void Start()
    {
        //每个控制器都要这么写一下，不用改，这样的话即插即用，找不到也不会报错
        if (magneticController == null)
            Debug.LogError("magneticController加载失败，请调整");
        else
            controllerList.Add(magneticController);

        if (playerController == null)
            Debug.LogError("magneticController加载失败，请调整");
        else
            controllerList.Add(playerController);

    }

    /// <summary>
    /// 全局通知函数
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="obj"></param>
    /// <param name="data"></param>
    public void Notify(string eventName,Object obj,params object[] data)
    {
        foreach(MyElement c in controllerList)
        {
            c.OnNotification(eventName, obj, data);
        }
    }
}
