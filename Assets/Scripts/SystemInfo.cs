using System;
using UnityEngine;

namespace Assets.Scripts
{
	public class SystemInfo : ISystemInfo
	{
		private static Lazy<SystemInfo> _lazy = new Lazy<SystemInfo>(() => new SystemInfo(), isThreadSafe: true);
		public static ISystemInfo Instance => _lazy.Value;

		private SystemInfo()
		{

		}

		/// <summary>
		/// システム上の時間情報を取得する
		/// </summary>
		/// <returns>時間情報</returns>
		public DateTime GetSystemDate()
		{
			return DateTime.Now;
		}
	}
}