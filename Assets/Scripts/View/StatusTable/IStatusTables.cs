using System;
using UnityEngine;

namespace Assets.Scripts.View.StatusTable
{
	public interface IStatusTables : IDisposable
	{
		public ISystemTable SystemTbl { get; }
	}
}
