using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Assets.Scripts.View.StatusTable
{
	internal class DSystemTable : ISystemTable
	{
		private static Lazy<DSystemTable> _lazy = new Lazy<DSystemTable>(() => new(), isThreadSafe: true);
		public static ISystemTable Instance => _lazy.Value;

		private DSystemTable()
		{

		}

		/// <summary>
		/// 情報更新時間を取得する
		/// </summary>
		/// <returns></returns>
		public DateTime GetUpDateTime()
		{
			return new DateTime(2026, 9, 27, 8, 0, 0);
		}

		/// <summary>
		/// 現在の指示行動を取得する
		/// </summary>
		/// <returns></returns>
		public BehaviorId.BehaviorId GetNowBehaviorId()
		{
			return BehaviorId.BehaviorId.Idle;
		}

		/// <summary>
		/// 現在の指示行動名を取得する
		/// </summary>
		/// <returns></returns>
		public string GetNowBehaviorName()
		{
			return BehaviorId.BehaviorId.Idle.ToString();
		}

		/// <summary>
		/// 日の入りの時間を取得する
		/// </summary>
		/// <returns></returns>
		public DateTime GetSunSetTime()
		{
			return DateTime.Now.Date.AddHours(19);
		}

		/// <summary>
		/// 日の出の時間を取得する
		/// </summary>
		/// <returns></returns>
		public DateTime GetSunRiseTime()
		{
			return DateTime.Now.Date.AddDays(6);
		}

		/// <summary>
		/// 天気を取得する
		/// </summary>
		/// <returns></returns>
		public string GetNowWeather()
		{
			return "Clear";
		}
	}
}
