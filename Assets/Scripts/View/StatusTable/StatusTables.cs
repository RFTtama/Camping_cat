using System;
using UnityEngine;

namespace Assets.Scripts.View.StatusTable
{
	public class StatusTables : IDisposable, IStatusTables
	{
		private static Lazy<StatusTables> _lazy = new Lazy<StatusTables>(() => new StatusTables(), isThreadSafe: true);
		public static IStatusTables Instance => _lazy.Value;

		public ISystemTable SystemTbl { get; }

		private StatusTables()
		{
			SystemTbl = SystemTable.Instance;
		}

		public void Dispose()
		{
			// Dispose resources if needed
		}
	}
}
