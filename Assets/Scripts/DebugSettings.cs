using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts
{
    public static class DebugSettings
    {
        // API取得をエミュレートする
        public static readonly bool WEATHER_TASK_STUB = true;

        // SystemTableの値を手動設定する
        public static readonly bool SYSTEM_TABLE_STUB = false;

        // システムデータの値を手動設定する
        public static readonly bool SYSTEM_DATA_STUB = false;
    }
}
