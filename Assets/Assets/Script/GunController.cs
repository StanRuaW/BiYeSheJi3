using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MyElement
{
    public List<GameObject> guns;
    [SerializeField]
    private int currentGunNum;
    private GunPrototype currentGunModule;

    public RightHandEventPass right;

    // Start is called before the first frame update
    void Start()
    {
        currentGunNum = 0;
      /*  currentGunModule = guns[currentGunNum].GetComponent<GunPrototype>();
        guns[currentGunNum].SetActive(true);
        ChangeGunGrab();*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shot()
    {
        currentGunModule.Shot();
        Debug.Log("接收到了射击信息");
    }


    public void Reload()
    {
        currentGunModule.Reload();
        Debug.Log("接收到了换单信息");
    }

    public void ChangeGun()
    {
        ChangeGunState();
        ChangeGunGrab();

        Debug.Log("接收到了换枪信息");
    }
    private void  ChangeGunState()
    {
        guns[currentGunNum].SetActive(false);

        if (currentGunNum + 1 == guns.Count)
            currentGunNum = 0;
        else
            currentGunNum++;

        guns[currentGunNum].SetActive(true);
        currentGunModule = guns[currentGunNum].GetComponent<GunPrototype>();
    }

    private void ChangeGunGrab()
    {
        GameObject currentGun = guns[currentGunNum];

        right.GrabGun(currentGun);
    }
}
