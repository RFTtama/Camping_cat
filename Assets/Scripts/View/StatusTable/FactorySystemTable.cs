using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.View.StatusTable
{
	internal static class FactorySystemTable
	{
		public static ISystemTable Create()
		{
			if (DebugSettings.SYSTEM_TABLE_STUB)
			{
                return DSystemTable.Instance;
            }
			return SystemTable.Instance;
		}
	}
}
