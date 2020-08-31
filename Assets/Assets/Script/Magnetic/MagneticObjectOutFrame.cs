using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticObjectOutFrame : MonoBehaviour
{
    [SerializeField]
    private MagneticObject source;
    [SerializeField]
    private float Thick = 0.03f;

    private BoxCollider myCollider;
    // Start is called before the first frame update
    void Start()
    {
        myCollider = gameObject.GetComponent<BoxCollider>();
        SetColliderShape();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendIsNToSource(Nullable<bool> isN)
    {
        source.isN = isN;
    }

    private void SetColliderShape()
    {
        float colliderScaleY = 1 + Thick / source.transform.localScale.y;
        float colliderScaleX = 1 + Thick / source.transform.localScale.x;
        float colliderScaleZ = 1 + Thick / source.transform.localScale.z;
        myCollider.size = new Vector3(colliderScaleX, colliderScaleY, colliderScaleZ);
        myCollider.center = new Vector3(0, Thick / source.transform.localScale.y/2, 0);
    }
}
