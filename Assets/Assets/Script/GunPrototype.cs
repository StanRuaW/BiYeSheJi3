using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// 枪的原型，可以开枪、换弹、挂载到手上
/// </summary>
public class GunPrototype : VRTK_InteractableObject
{
    [Header("枪械设置")]
    public GameObject bullet;
    public GameObject Hand;
    private VRTK_InteractGrab handGrab;

    public float bulletSpeed;
    public float bulletLife;
    public float shotspeed;
    public GameObject ShotPoint;

    public int MaxBulletNum;
    private int currentBulletNum;
    public int CurrentBulletNum { private set { currentBulletNum = value; } get { return currentBulletNum; } }

    public RightHandEventPass right;//枪激活时候要挂载到这个手上

    [Header("snapPoint设置")]
    [SerializeField] private Vector3 snapPosition;
    [SerializeField] private Vector3 snapRotation;
    [SerializeField] private Vector3 snapScale;
    [SerializeField] private GameObject snapPoint;

    protected void Start()
    {
        currentBulletNum = MaxBulletNum;


    }

   /*  private void OnEnable()
    {
        base.OnEnable();
        ResetSnapPoint();
    }*/

     public void Shot()
    {
        if (!HasBullet())
        {
            Debug.Log("No bullet");
            //PlayNoNulletVoice();
            //PlayNoBulletAnime();
            return;
        }

        //克隆子弹，设置速度、方向、几秒之后消失
        GameObject bulletClone = Instantiate(bullet, ShotPoint.transform.position, ShotPoint.transform.rotation);
        bulletClone.SetActive(true);
        Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
        rb.AddForce(-bullet.transform.forward * bulletSpeed);
        Destroy(bulletClone, bulletLife);

        currentBulletNum--;

        //飞出弹壳
        //音效
        //爆炸

        Debug.Log("shot");
    }

    public void Reload()
    {
        CurrentBulletNum = MaxBulletNum;
        Debug.Log("ReLoad");
        //play
    }

    public bool HasBullet()
    {
        if (currentBulletNum == 0)
            return false;
        else if (currentBulletNum > 0)
            return true;
        else
        {
            Debug.LogError("出现了子弹小于0");
            return false;
        }
    }

    /// <summary>
    /// 重置SnapPoint的transform，因为每次重新active的时候snappoint的坐标会乱
    /// </summary>
    public void ResetSnapPoint()
    {
        snapPoint.transform.localPosition = snapPosition;
        snapPoint.transform.localEulerAngles = snapRotation;
        snapPoint.transform.localScale = snapScale;
    }

    /// <summary>
    /// 让手抓住这把枪
    /// </summary>
    public void GrabThisGun()
    {
        ResetSnapPoint();
        right.GrabGun(gameObject);
    }
}
