using System;
using UnityEngine;
using Assets.Scripts.BehaviorId;

namespace Assets.Scripts.View.StatusTable
{
    [Serializable]
    public class SystemTable : ISystemTable
	{
		private static Lazy<SystemTable> _lazy = new Lazy<SystemTable>(() => new SystemTable(), isThreadSafe: true);
		public static SystemTable Instance => _lazy.Value;

		private SystemTable()
		{
			UpDateTime = new();
			NowBehaviorId = BehaviorId.BehaviorId.Idle;
		}

		// 要素定義
		public string UpDateTimeString; //ログ用時間
        public string SunSetTimeString; //ログ用時間
        public string SunRiseTimeString; //ログ用時間

        public DateTime UpDateTime;

		public BehaviorId.BehaviorId NowBehaviorId;

		public string NowBehaviorName;

		public DateTime SunSetTime;
		public DateTime SunRiseTime;


        // 取得関数

        /// <summary>
        /// 情報更新時間を取得する
        /// </summary>
        /// <returns></returns>
        public DateTime GetUpDateTime()
		{
			return UpDateTime;
		}

		/// <summary>
		/// 現在の指示行動を取得する
		/// </summary>
		/// <returns></returns>

		public BehaviorId.BehaviorId GetNowBehaviorId()
		{
			return NowBehaviorId;
		}

		/// <summary>
		/// 現在の指示行動名を取得する
		/// </summary>
		/// <returns></returns>

		public string GetNowBehaviorName()
		{
			return NowBehaviorName;
		}

		/// <summary>
		/// 日の入りの時間を取得する
		/// </summary>
		/// <returns></returns>
		public DateTime GetSunSetTime()
		{
			return SunSetTime;
		}

		/// <summary>
		/// 日の出の時間を取得する
		/// </summary>
		/// <returns></returns>
		public DateTime GetSunRiseTime()
		{
			return SunRiseTime;
		}

    }
}
