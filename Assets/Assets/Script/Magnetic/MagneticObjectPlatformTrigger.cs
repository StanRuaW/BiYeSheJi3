using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticObjectPlatformTrigger : MonoBehaviour
{
    //这里看能不能改成直接根据tag获取，省的每次都要绑定
    [SerializeField] private Transform Player_Parent;
    [SerializeField] private Transform Player;
    [SerializeField] private float Thick;
    [SerializeField] private float MidSpaceLength;

    //TODO这里命名有歧义
    private BoxCollider myCollider;
    private Transform my;

    [SerializeField] GameObject myParent;
    // Start is called before the first frame update
    void Start()
    {
        my = gameObject.transform;
        myCollider = gameObject.GetComponent<BoxCollider>();
        SetTriggerShape();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player进来了");
            Player.SetParent(my);
                //最好做个检测有没有多个物体同时绑定了player
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //TODO：可能会出现玩家在磁铁上传送导致父子层级出问题的bug
        //可能会出现玩家在磁铁上传送导致父子层级出问题的bug
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player出来了");
            Player.SetParent(Player_Parent);
        }
    }

    private void SetTriggerShape()
    {
        float colliderScaleY = (myParent.transform.localScale.y + Thick) / myParent.transform.localScale.y;
        float colliderScaleX = (myParent.transform.localScale.x - MidSpaceLength) / myParent.transform.localScale.x;
        float colliderScaleZ = (myParent.transform.localScale.z - MidSpaceLength) / myParent.transform.localScale.z;
        myCollider.size = new Vector3(colliderScaleX, colliderScaleY, colliderScaleZ);
        myCollider.center = new Vector3(0, 0 + (colliderScaleY - 1) / 2, 0);
    }
}