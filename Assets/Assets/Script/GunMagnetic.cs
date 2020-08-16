using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMagnetic :GunPrototype
{
    // Start is called before the first frame update
    new void Start()
    {
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
}
