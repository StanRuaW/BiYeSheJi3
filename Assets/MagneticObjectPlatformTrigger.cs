using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticObjectPlatformTrigger : MonoBehaviour
{
    //这里看能不能改成直接根据tag获取，省的每次都要绑定
    [SerializeField] private Transform Player_Parent;
    [SerializeField] private Transform Player;
    private Transform my;
    // Start is called before the first frame update
    void Start()
    {
        my = gameObject.transform;
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
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player出来了");
            Player.SetParent(Player_Parent);
        }
    }
}