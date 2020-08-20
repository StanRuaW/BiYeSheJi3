using System.Collections;
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

    //TODO:还没设置不能扔枪
    //因为start阶段不能grab，因此在update的第一帧抓枪
    private bool isGunGrabOnStart = false;

    void Start()
    {
        currentGunNum = 0;
        currentGunModule = guns[currentGunNum].GetComponent<GunPrototype>();

        //SendGunStateToUI();
        //ChangeGun(currentGunNum);
    }

    override public void OnNotification(string eventName, UnityEngine.Object obj, params object[] data)
    {
        switch (eventName)
        {
            case "try.shot":
                Shot();
                break;

            case "try.change.gun":
                ChangeGun();
                break;

            case "try.reload.gun":
                Reload();
                break;
            case "try.change.gun.state":
                ChangeGunState();

                break;
           /* case "on.new.gun.pick.up":
                AddNewGun((GameObject)obj);
                break;
            case "on.gun.remove":
                RemoveGun((GameObject)obj);
                break;*/
        }
    }

    void Update()
    {
        //TODO:因为start不能grab，因此在第一帧抓枪，有隐藏bug的可能性，开局0.1秒之后抓枪
        if (isGunGrabOnStart == false)
        {
            StartCoroutine("StartGrabGun");
        }
    }

    public void Shot()
    {
        if (guns.Count == 0)
            return;
        currentGunModule.Shot();
        Debug.Log("接收到了射击信息");
        SendGunStateToUI();
    }


    public void Reload()
    {
        if (guns.Count == 0)
            return;
        currentGunModule.Reload();
        Debug.Log("接收到了换弹信息");
        SendGunStateToUI();
    }

    public bool ChangeGun(int num)
    {
        if (num >= guns.Count || num < 0)
        {
            Debug.LogError("切枪切大了");
            return false;
        }
        if (guns.Count == 0)
        {
            Debug.Log("无枪");
            return false;
        }
        guns[currentGunNum].SetActive(false);

        Debug.Log("我切枪了啊");
        currentGunNum = num;
        guns[currentGunNum].SetActive(true);
        currentGunModule = guns[currentGunNum].GetComponent<GunPrototype>();

        currentGunModule.GrabThisGun();
        SendGunStateToUI();
        return true;

    }

    /// <summary>
    /// 切到下一把枪
    /// </summary>
    public bool ChangeGun()
    {
        return ChangeGun(currentGunNum+1==guns.Count?0:currentGunNum+1);
        /* if (guns.Count == 0)
             return;
         ChangeGunState();
         currentGunModule.GrabThisGun();

         Debug.Log("接收到了换枪信息");
         SendGunStateToUI();*/
    }



    private void SendGunStateToUI()
    {
        string gunType;
        string gunName;
        object bullet;
        GetGunState(out gunType, out gunName, out bullet);
        app.uiController.ChangeGunUIState(gunType, gunName, bullet);
    }

    private void GetGunState(out string gunType,out string gunName,out object bullet)
    {
        gunType = currentGunModule.GUNTYPE;
        gunName = currentGunModule.GUNNAME;
        bullet = currentGunModule.GetBulletState();
    }

    /// <summary>
    /// /////
    /// </summary>
    /// <param name="newGun"></param>
    /*private void AddNewGun(GameObject newGun)
    {
        guns.Add(newGun);
        ChangeGun(guns.Count - 1);
    }*/

    /*private void RemoveGun(GameObject gun)
    {
        guns.Remove(gun);
        ChangeGun(currentGunNum == 0 ? 0 : currentGunNum - 1);
    }*/
    void ChangeGunState()
    {
        currentGunModule.ChangeGunState();
        SendGunStateToUI();
    }

    IEnumerator StartGrabGun()
    {
        yield return new WaitForSeconds(0.1f);
        ChangeGun(currentGunNum);
        isGunGrabOnStart = true;
       // Debug.Log("我开局爪子刀枪了");
    }
}
