using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAddForce : MonoBehaviour
{
    public float force = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector3( force * Time.deltaTime,0,0));
    }
}
