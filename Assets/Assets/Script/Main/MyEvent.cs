using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 全局的变量，当event一样用
/// </summary>
/// 有人进行全局的notify给所有的controller，然后每个controller自己判断
/// 为什么用这种形式而不是用事件系统。因为个人还是不太熟悉委托、事件这些回调的写法，更习惯直接调用。而且符合自己定的设计模式。
public class MyEvent 
{
    public static string TryShot="try.shot";
    public static string TryChangeGun = "try.change.gun";
    public static string TryReloadGun = "try.reload.gun";
}