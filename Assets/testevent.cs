using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class testevent : MyElement
{
    private VRTK_ControllerEvents controllerEvents;
    // Start is called before the first frame update
    protected void OnEnable()
    {
        controllerEvents = GetComponent<VRTK_ControllerEvents>();
        if (controllerEvents == null)
            Debug.LogError("枪找不到ControllerEvent");
        //controllerEvents.TriggerPressed += FireBullet;
        controllerEvents.StartMenuPressed += FireBullet;
        Debug.Log("jiazaiwanbi");
    }
    public void FireBullet(object sender, ControllerInteractionEventArgs e)
    {

        app.Notify(MyEvent.TryShot, gameObject);
    }
}
