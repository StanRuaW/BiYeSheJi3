using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticObjectPlatformTrigger : MonoBehaviour
{
    //这里看能不能改成直接根据tag获取，省的每次都要绑定
    [SerializeField] private Transform Player_Parent;
    [SerializeField] private Transform Player;
    [Range(0.05f,10)]
    [SerializeField] private float Thick;
    [SerializeField] private float MidSpaceLength;

    private BoxCollider myCollider;

    //这里命名有问题，不是myparent，是我的。。主任？？？目标？？？不知道咋说
    [SerializeField] Transform myParent;
    // Start is called before the first frame update
    void Start()
    {
        //TODO：这里根据名字查找被许多说过是强耦合，但是我觉得没问题，这种只有一个的东西肯定是可以的
        Player_Parent = GameObject.Find("PlayerParent").transform;
        Player = GameObject.Find("VRTK SDK").transform;
        //TODO:这里也算是强耦合了，先这样
        myParent = gameObject.transform.parent.GetChild(0);
        //myParent = gameObject.transform.parent;

        myCollider = gameObject.GetComponent<BoxCollider>();
        SetTriggerShape();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #region
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player进来了");
            Player.SetParent(transform);
                //最好做个检测有没有多个物体同时绑定了player
        }
    }
    #endregion
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
        float colliderScaleY = (myParent.localScale.y + Thick) ;
        float colliderScaleX = (myParent.localScale.x - MidSpaceLength) ;
        float colliderScaleZ = (myParent.localScale.z - MidSpaceLength);
        myCollider.size = new Vector3(colliderScaleX, colliderScaleY, colliderScaleZ);
        myCollider.center = new Vector3(0, 0 + (colliderScaleY - 1) / 2, 0);
    }
}

