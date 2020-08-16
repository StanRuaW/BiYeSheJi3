using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMagnetic :GunPrototype
{
    public Nullable<bool> isN;
    // Start is called before the first frame update
    new void Start()
    {
        gunType = GunUIStateName.GunMagnetic;

        isN = true;

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override protected void SetCloneBullet(GameObject clone)
    {
        clone.GetComponent<BulletMagnetic>().isn = false;
    }

    override public object GetBulletState()
    {
        return isN;
    }
}
