using System;
using System.Runtime.InteropServices;

namespace VoicevoxCoreNet
{
    /// <summary>
    /// 初期化オプション
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    struct VoicevoxInitializeOptions
    {
        /// <summary>
        /// ハードウェアアクセラレーションモード
        /// </summary>
        VoicevoxAccelerationMode acceleration_mode;
        /// <summary>
        /// CPU利用数を指定
        /// 0を指定すると環境に合わせたCPUが利用される
        /// </summary>
        ushort cpu_num_threads;
        /// <summary>
        /// 全てのモデルを読み込む
        /// </summary>
        bool load_all_models;
        /// <summary>
        /// open_jtalkの辞書ディレクトリ
        /// </summary>
        IntPtr open_jtalk_dict_dir;
    };
}