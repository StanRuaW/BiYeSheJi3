using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletMagnetic : BulletPrototype
{
    public Nullable<bool> isn;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

     override protected void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "MagneticObject")
        {
            collision.gameObject.GetComponent<MagneticObject>().isN = isn;
        }
        else if(collision.gameObject.tag == "MagneticObjectOuterFrame")
        {
            collision.gameObject.GetComponent<MagneticObjectOutFrame>().SendIsNToSource(isn);
        }
        else if(collision.gameObject.tag=="MagneticRange")
        {
            Debug.Log("碰到磁力范围");
            return;
        }
        base.OnTriggerEnter(collision);
    }


}
