using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VoicevoxCoreNet
{
    /// <summary>
    /// 初期化オプション
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct VoicevoxInitializeOptions
    {
        /// <summary>
        /// ハードウェアアクセラレーションモード
        /// </summary>
        public VoicevoxAccelerationMode acceleration_mode;

        /// <summary>
        /// CPU利用数を指定
        /// 0を指定すると環境に合わせたCPUが利用される
        /// </summary>
        public ushort cpu_num_threads;

        /// <summary>
        /// 全てのモデルを読み込む
        /// </summary>
        public bool load_all_models;

        /// <summary>
        /// open_jtalkの辞書ディレクトリ
        /// </summary>
        /// <remarks>null終端utf8</remarks>
        byte[] open_jtalk_dict_dir;

        /// <summary>
        /// open_jtalkの辞書ディレクトリ
        /// </summary>
        public string OpenJtalkDictDir
        {
            get
            {
                return Encoding.UTF8.GetString(open_jtalk_dict_dir, 0,  open_jtalk_dict_dir.Length - 1);
            }
            set
            {
                open_jtalk_dict_dir = Encoding.UTF8.GetBytes(value + '\0');
            }
        }
    }
}