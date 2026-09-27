using System;
using System.Drawing;
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

		/// <summary>
		/// スクリーンサイズを取得する
		/// </summary>
		/// <returns></returns>
		public Size GetScreenSize()
		{
			Size ret_size = new(Screen.width, Screen.height);
			return ret_size;
		}
	}
}