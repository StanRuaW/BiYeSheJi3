using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

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

    private VRTK_ControllerEvents controllerEvents;
    /*  public override void StartUsing(VRTK_InteractUse usingObject)
      {
          base.StartUsing(usingObject);
          FireBullet();
      }*/

    private void Start()
    {
        currentBulletNum = MaxBulletNum;


    }
    public void Shot()
    {
        if (!HasBullet())
        {
            Debug.Log("No bullet");
            //PlayNoNulletVoice();
            //PlayNoBulletAnime();
            return;
        }

        GameObject bulletClone = Instantiate(bullet, ShotPoint.transform.position, ShotPoint.transform.rotation);
        bulletClone.SetActive(true);
        Rigidbody rb = bulletClone.GetComponent<Rigidbody>();
        rb.AddForce(-bullet.transform.forward * bulletSpeed);
        Destroy(bulletClone, bulletLife);

        currentBulletNum--;

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
}
