using System;
using System.Drawing;
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
		/// <summary>
		/// 日の入りの時間を取得する
		/// </summary>
		/// <returns></returns>
		public DateTime GetSunSetTime();
		/// <summary>
		/// 日の出の時間を取得する
		/// </summary>
		/// <returns></returns>
		public DateTime GetSunRiseTime();
		/// <summary>
		/// スクリーンサイズを取得する
		/// </summary>
		/// <returns></returns>
		public Size GetScreenSize();
	}
}
