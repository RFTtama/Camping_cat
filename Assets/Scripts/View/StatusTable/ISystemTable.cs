using System;
using UnityEngine;

namespace Assets.Scripts.View.StatusTable
{
	public interface ISystemTable
	{
		/// <summary>
		/// 情報更新時間を取得する
		/// </summary>
		/// <returns></returns>
		public DateTime GetUpDateTime();
		/// <summary>
		/// 現在の指示行動を取得する
		/// </summary>
		/// <returns></returns>
		public BehaviorId.BehaviorId GetNowBehaviorId();
		/// <summary>
		/// 現在の指示行動名を取得する
		/// </summary>
		/// <returns></returns>
		public string GetNowBehaviorName();
	}
}
