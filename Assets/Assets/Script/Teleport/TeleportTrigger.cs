using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
public class TeleportTrigger : VRTK_DestinationMarker
{
    public Transform destination;
    [SerializeField] private Transform player;
    [SerializeField] private VRTK_HeightAdjustTeleport basicTelepot;
    // Start is called before the first frame update
    void Start()
    {
        if (basicTelepot == null)
            Debug.LogError("传送器没有传送脚本");

        if (player == null)
            Debug.LogError("传送器没有玩家引用");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            VRTK_ControllerEvents controller = player.gameObject.GetComponent<VRTK_ControllerEvents>();
            if (controller == null)
                Debug.LogError("进入传送区域时没得到controllerevent");

            //TODO:我也不知道为什么这里设置的传送地点不合法。但是强制传送好用了，就这样吧
            basicTelepot.ForceTeleport(destination.position, destination.rotation);

            Debug.Log("我传送了");
        }
    }

}
