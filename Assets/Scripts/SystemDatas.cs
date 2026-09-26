using System;
using UnityEngine;

namespace Assets.Scripts
{
	public class SystemDatas : ISystemDatas
	{
		private static Lazy<SystemDatas> _lazy = new Lazy<SystemDatas>(() => new SystemDatas(), isThreadSafe: true);
		public static ISystemDatas Instance => _lazy.Value;

		private SystemDatas()
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