using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VoicevoxCoreNet
{
    /// <summary>
    /// 初期化オプション
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct InitializeOptions
    {
        /// <summary>
        ///オブジェクトを初期値で初期化します。
        /// </summary>
        public InitializeOptions()
        {
            InitializeOptions option = Native.CoreNative.voicevox_make_default_initialize_options();
            this.acceleration_mode = option.acceleration_mode;
            this.cpu_num_threads = option.cpu_num_threads;
            this.load_all_models = option.load_all_models;
            this.open_jtalk_dict_dir = null;
            OpenJtalkDictDir = "";
        }

        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        /// <param name="accelerationMode">ハードウェアアクセラレーションモード</param>
        /// <param name="cpuNumThreads">CPU利用数を指定</param>
        /// <param name="loadAllModels">全てのモデルを読み込むかどうか</param>
        /// <param name="openJtalkDictDir">open_jtalkの辞書ディレクトリ</param>
        public InitializeOptions(AccelerationMode accelerationMode, ushort cpuNumThreads, bool loadAllModels, string openJtalkDictDir)
        {
            this.acceleration_mode = accelerationMode;
            this.cpu_num_threads = cpuNumThreads;
            this.load_all_models = loadAllModels;
            this.open_jtalk_dict_dir = null;
            OpenJtalkDictDir = openJtalkDictDir;
        }

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
        public bool load_all_models;

        /// <summary>
        /// open_jtalkの辞書ディレクトリ
        /// </summary>
        /// <remarks>null終端utf8</remarks>
        private byte[] open_jtalk_dict_dir;

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
                open_jtalk_dict_dir = Utf8Converter.GetUTF8ByteWithNullChar(value);
            }
        }
    }
}