using UnityEngine;
using System;
using Assets.Scripts.BehaviorId;
using Assets.Scripts.View.StatusTable;
using System.IO;
using System.Text;

namespace Assets.Scripts.View
{
	public class View : IDisposable, IView
	{
		private static Lazy<View> _lazy = new Lazy<View>(() => new View(), isThreadSafe: true);
		public static View Instance => _lazy.Value;

		// テーブル
		IStatusTables statusTable;
		SystemTable systemTable;


		public void Dispose()
		{

		}

		private View()
		{
			statusTable = StatusTables.Instance;
			systemTable = SystemTable.Instance;
		}

		/// <summary>
		/// 情報の更新通知
		/// </summary>
		/// <param name="arg">通知する情報</param>
		public void Update(ViewUpdateData arg)
		{
			// 各テーブルの情報を更新する
			systemTable.UpDateTime = SystemInfo.Instance.GetSystemDate();
			systemTable.UpDateTimeString = systemTable.UpDateTime.ToString();
			systemTable.NowBehaviorId = arg.BehaviorId;
			systemTable.NowBehaviorName = arg.BehaviorName;

			UnityEngine.Debug.Log("View Update Executed");

			StringBuilder sb = new();

			string json = JsonUtility.ToJson(systemTable);

			sb.Append(json);

			using (StreamWriter sw = new("viewupdatelog.txt", true))
			{
				sw.WriteLine(json);
				sw.WriteLine();
                UnityEngine.Debug.Log("Model Log Wrote");
            }
		}
	}
}
