using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDamageAble : GunPrototype
{
    public float damage;

    // Start is called before the first frame update
    void Awake()
    {
        gunType = GunUIStateName.GunDamageAble;
        base.Start();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    override protected void SetCloneBullet(GameObject clone)
    {
        clone.GetComponent<BulletDamageable>().damage = damage;
    }

    override public object GetBulletState()
    {
        return CurrentBulletNum;
    }
}
