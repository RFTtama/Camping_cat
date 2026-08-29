using UnityEngine;
using System;
using Assets.Scripts.BehaviorId;

namespace Assets.Scripts.View
{
	public interface IView : IDisposable
	{
		/// <summary>
		/// 情報の更新通知
		/// </summary>
		/// <param name="arg">通知する情報</param>
		public void Update(ViewUpdateData arg);
	}

	public struct ViewUpdateData
	{
		public BehaviorId.BehaviorId BehaviorId;
		public string BehaviorName;
	}
}
