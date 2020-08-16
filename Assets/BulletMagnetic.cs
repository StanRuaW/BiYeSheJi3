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

     override protected void OnCollisionEnter(Collision collision)
    {
        if (isn==true)
            Debug.Log("碰撞时候是n");
        else
            Debug.Log("碰撞不是n");
        Debug.Log("碰上了");
        if (collision.gameObject.tag == "MagneticObject")
        {
            Debug.Log("碰到了磁铁"); 
            collision.gameObject.GetComponent<MagneticObject>().isN = isn;
            BulletImpact.SetActive(true);
            Destroy(gameObject,0.2f);
        }
        base.OnCollisionEnter(collision);
    }
}
