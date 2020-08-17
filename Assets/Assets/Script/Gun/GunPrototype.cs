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
    public GameObject ShotPoint;

    public int MaxBulletNum;
    private int currentBulletNum;
    public int CurrentBulletNum { private set { currentBulletNum = value; } get { return currentBulletNum; } }

    public RightHandEventPass right;//枪激活时候要挂载到这个手上

    public GameObject GunSpark;

    public float shotInterval;
    private float lastShotTime;


    [SerializeField] protected string gunName;
    public string GUNNAME { get { return gunName; } }
    protected string gunType;
    public string GUNTYPE { get { return gunType; } }



    [Header("snapPoint设置")]
    [SerializeField] private Vector3 snapPosition;
    [SerializeField] private Vector3 snapRotation;
    [SerializeField] private Vector3 snapScale;
    [SerializeField] private GameObject snapPoint;

    protected void Start()
    {
        currentBulletNum = MaxBulletNum;
        GunSpark.SetActive(false);

        //初始化射击的时间间隔
        lastShotTime = Time.time;

    }

   /*  private void OnEnable()
    {
        base.OnEnable();
        ResetSnapPoint();
    }*/

     virtual public void Shot()
    {
        if(!IsTimeToShot())
        {
            Debug.Log("不是开枪的时间");
            return ;
        }

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
        //设置克隆子弹的属性。因为nullable数在Instantiate的时候有bug，要单独设置。
        SetCloneBullet(bulletClone);

        Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
        rb.AddForce(-gameObject.transform.forward * bulletSpeed);
        Destroy(bulletClone, bulletLife);

        currentBulletNum--;

        StartCoroutine("ShowGunSpark");
        //飞出弹壳
        //音效
        //爆炸

    }

    virtual public void Reload()
    {
        CurrentBulletNum = MaxBulletNum;
        //play
    }

    /// <summary>
    /// 用来限制开枪的速度
    /// </summary>
    private bool IsTimeToShot()
    {
        if (Time.time - lastShotTime > shotInterval)
        {
            lastShotTime = Time.time + shotInterval;
            return true;
        }
        else
            return false;
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

    //展示一小会枪口火焰
    IEnumerator ShowGunSpark()
    {
        GunSpark.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        GunSpark.SetActive(false);
    }

    /// <summary>
    /// 设置被克隆的东西的属性。好像是因为，Instantiate复制的时候，如果被复制的物体有nullable数，这个数会被重置为null。。。所以在这里单独设置一次。而且直接修改原先的子弹prefeb也确实不妥
    /// </summary>
    /// <param name="clone"></param>
    protected virtual void SetCloneBullet(GameObject clone)
    {

    }

    virtual public object GetBulletState()
    {
        return null;
    }

    public virtual void ChangeGunState() { }
}
