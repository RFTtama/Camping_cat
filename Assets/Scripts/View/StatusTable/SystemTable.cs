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

		public DateTime UpDateTime;

		public BehaviorId.BehaviorId NowBehaviorId;

		public string NowBehaviorName;


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
	}
}
