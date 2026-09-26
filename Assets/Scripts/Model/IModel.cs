using System;
using Assets.Scripts.BehaviorId;

namespace Assets.Scripts.Model
{
	public interface IModel : IDisposable
	{
		/// <summary>
		/// 今のキャラの行動を取得する
		/// </summary>
		/// <returns>行動名</returns>
		public string GetNowBehavior();

		/// <summary>
		/// 今のキャラの行動IDを取得する
		/// </summary>
		/// <returns>行動ID</returns>
		public BehaviorId.BehaviorId GetNowBehaviorId();

		/// <summary>
		/// 日の出の時間を取得する
		/// </summary>
		/// <returns>日の出時刻</returns>
		public DateTime GetSunRiseTime();

		/// <summary>
		/// 日の入の時間を取得する
		/// </summary>
		/// <returns>日の入時刻</returns>
		public DateTime GetSunSetTime();
	}
}