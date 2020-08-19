using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticObjectOutFrame : MonoBehaviour
{
    [SerializeField]
    private MagneticObject source;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendIsNToSource(Nullable<bool> isN)
    {
        source.isN = isN;
    }
}
