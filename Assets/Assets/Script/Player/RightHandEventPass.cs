using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// 右手的事件处理转发器和动作处理
/// </summary>
public class RightHandEventPass : MyElement
{

    private VRTK_ControllerEvents controllerEvents;

    private VRTK_InteractTouch interActTouch;
    private VRTK_InteractGrab interActGrab;

    protected void OnEnable()
    {
        interActTouch = gameObject.GetComponent<VRTK_InteractTouch>();
        interActGrab = gameObject.GetComponent<VRTK_InteractGrab>();

        controllerEvents = GetComponent<VRTK_ControllerEvents>();
        if (controllerEvents == null)
            Debug.LogError("右手找不到ControllerEvent");

        controllerEvents.TriggerPressed += SendTryFireMessage;
        controllerEvents.ButtonTwoPressed += SendReloadMessage;
        controllerEvents.TouchpadPressed += SendChangeGunMessage;

    }

    protected void OnDisable()
    {
        controllerEvents.TriggerPressed -= SendTryFireMessage;
        controllerEvents.ButtonTwoPressed -= SendReloadMessage;
        controllerEvents.TouchpadPressed -= SendChangeGunMessage;

    }

    private void SendTryFireMessage(object sender, ControllerInteractionEventArgs e)
    {
        app.Notify(MyEvent.TryShot, gameObject, e);
    }

    private void SendChangeGunMessage(object sender, ControllerInteractionEventArgs e)
    {
        app.Notify(MyEvent.TryChangeGun, gameObject, e);
    }

    private void SendReloadMessage(object sender, ControllerInteractionEventArgs e)
    {
        app.Notify(MyEvent.TryReloadGun, gameObject, e);
    }

    /// <summary>
    /// 把枪绑定到手上，切枪时调用
    /// </summary>
    /// <param name="obj"></param>
    public void GrabGun(GameObject obj)
    {
        interActGrab.ForceRelease();
        interActTouch.ForceStopTouching();

        interActTouch.ForceTouch(obj);
        interActGrab.AttemptGrab();
    }
}
