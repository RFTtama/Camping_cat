using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Assets.Scripts
{
	internal class DSystemDatas : ISystemDatas
	{
		private static Lazy<DSystemDatas> _lazy = new Lazy<DSystemDatas>(() => new(), isThreadSafe: true);
		public static ISystemDatas Instance => _lazy.Value;

		private DSystemDatas() { }

		/// <summary>
		/// システム上の時間情報を取得する
		/// </summary>
		/// <returns>時間情報</returns>
		public DateTime GetSystemDate()
		{
			return new DateTime(2026, 9, 26, 7, 00, 00);
		}

		/// <summary>
		/// スクリーンサイズを取得する
		/// </summary>
		/// <returns></returns>
		public Size GetScreenSize()
		{
			Size ret_size = new(1280, 720);
			return ret_size;
		}
	}
}
