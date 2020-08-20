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
        float colliderScaleY = (source.transform.localScale.y + Thick) / source.transform.localScale.y - 1;
        myCollider.size = new Vector3(1, colliderScaleY, 1);
        myCollider.center = new Vector3(0, 0.5f + colliderScaleY, 0);
    }
}
