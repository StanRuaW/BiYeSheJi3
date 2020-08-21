using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticRange : MonoBehaviour
{
    [SerializeField] Transform myParent;


    //TODO：磁力的几个形状设置可以归到主脚本里
    // Start is called before the first frame update
    void Start()
    {
        myParent = gameObject.transform.parent.GetChild(0);
        HideMaterial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("有trigger进来");
        if (other.gameObject.tag == "MagneticRange")
            Debug.Log("有磁体的范围进来了");
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "MagneticObject")
        {
            myParent.gameObject.GetComponent<MagneticObject>().AddMagneticForce(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
    public void SetScale(float X,float Y,float Z)
    {
        transform.localScale = new Vector3(X, Y, Z);
    }

    public void ChangeColor(Color color)
    {
        gameObject.GetComponent<Renderer>().material.SetColor("Color_4B5A0B46", color);
    }

    public  void ShowMaterial()
    {
        gameObject.SetActive(true);
    }

    public void HideMaterial()
    {
        gameObject.SetActive(false);
    }
}
