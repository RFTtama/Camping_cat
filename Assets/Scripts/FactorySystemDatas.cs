using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts
{
	internal static class FactorySystemDatas
	{
		public static ISystemDatas Create()
		{
			if (DebugSettings.SYSTEM_DATA_STUB)
			{
				return DSystemDatas.Instance;
			}

			return SystemDatas.Instance;
		}
	}
}
