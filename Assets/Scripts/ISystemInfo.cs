using System;
using UnityEngine;

public interface ISystemInfo
{
    /// <summary>
    /// システム上の時間情報を取得する
    /// </summary>
    /// <returns>時間情報</returns>
    public DateTime GetSystemDate();
}
