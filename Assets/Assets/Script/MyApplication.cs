using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所有其他的controller类都要从这里引用，来互相通信和调用
//提供了一个方法用来：找到中央控制器的入口
public class MyElement : MonoBehaviour
{
    //中央控制器的入口，中央控制器可以通信和互相调用
    public MyApplication app { get { return GameObject.FindObjectOfType<MyApplication>(); } }

    virtual public void OnNotification(string eventName, Object obj, params object[] data) { }

}

//中央控制器  
public class MyApplication : MonoBehaviour
{
    // Start is called before the first frame update
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

    public void Notify(string eventName,Object obj,params object[] data)
    {
        foreach(MyElement c in controllerList)
        {
            c.OnNotification(eventName, obj, data);
        }
    }
}
