﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageable : BulletPrototype
{
    public float damage;
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
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().GetDamage(damage);
            BulletImpact.SetActive(true);
            Destroy(gameObject, 0.2f);
        }
        base.OnCollisionEnter(collision);
    }
}
