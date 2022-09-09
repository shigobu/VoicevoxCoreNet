using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VoicevoxCoreNet.Native
{
    /// <summary>
    /// 初期化オプション
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct InitializeOptionsNative
    {
        /// <summary>
        /// ハードウェアアクセラレーションモード
        /// </summary>
        public AccelerationMode acceleration_mode;

        /// <summary>
        /// CPU利用数を指定
        /// 0を指定すると環境に合わせたCPUが利用される
        /// </summary>
        public ushort cpu_num_threads;

        /// <summary>
        /// 全てのモデルを読み込む
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool load_all_models;

        /// <summary>
        /// open_jtalkの辞書ディレクトリ
        /// </summary>
        /// <remarks>null終端utf8</remarks>
        public IntPtr open_jtalk_dict_dir;
    }
}