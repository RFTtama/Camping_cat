using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.View.StatusTable
{
    internal static class FactoryStatusTable
    {
        public static IStatusTables GetInstance()
        {
            return StatusTables.Instance;
        }
    }
}
