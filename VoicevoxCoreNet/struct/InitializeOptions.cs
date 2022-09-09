using System;
using System.Runtime.InteropServices;
using System.Text;

namespace VoicevoxCoreNet
{
    /// <summary>
    /// 初期化オプション
    /// </summary>
    public class InitializeOptions : IDisposable
    {
        /// <summary>
        /// openJtalk辞書の場所を指定して、オブジェクトを初期化します。
        /// </summary>
        /// <param name="openJtalkDictDir">openJtalk辞書の場所</param>
        public InitializeOptions(string openJtalkDictDir)
        {
            nativeObject = Native.CoreNative.voicevox_make_default_initialize_options();
            nativeObject.open_jtalk_dict_dir = Utf8Converter.AllocConvertManagedStringToNativeUtf8(openJtalkDictDir);
        }

        internal Native.InitializeOptionsNative nativeObject;

        /// <summary>
        /// ハードウェアアクセラレーションモード
        /// </summary>
        public AccelerationMode Acceleration 
        {
            get
            {
                return nativeObject.acceleration_mode;
            } 
            set
            {
                nativeObject.acceleration_mode = value;
            }
        }

        /// <summary>
        /// CPU利用数を指定
        /// 0を指定すると環境に合わせたCPUが利用される
        /// </summary>
        public ushort CpuNumThreads
        {
            get
            {
                return nativeObject.cpu_num_threads;
            }
            set
            {
                nativeObject.cpu_num_threads = value;
            }
        }

        /// <summary>
        /// 全てのモデルを読み込む
        /// </summary>
        public bool LoadAllModels
        {
            get
            {
                return nativeObject.load_all_models;
            }
            set
            {
                nativeObject.load_all_models = value;
            }
        }

        /// <summary>
        /// open_jtalkの辞書ディレクトリ
        /// </summary>
        /// <value>open_jtalkの辞書ディレクトリ</value>
        public string OpenJtalkDictDir
        {
            get
            {
                return Utf8Converter.MarshalNativeUtf8ToManagedString(nativeObject.open_jtalk_dict_dir);
            }
        }

        private bool disposedValue;

        /// <summary>
        /// オブジェクトのリソースを安全に開放します。
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: マネージド状態を破棄します (マネージド オブジェクト)
                }

                Marshal.FreeHGlobal(nativeObject.open_jtalk_dict_dir);
                disposedValue = true;
            }
        }

        /// <summary>
        /// ファイナライザー
        /// </summary>
        ~InitializeOptions()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: false);
        }

        /// <summary>
        /// オブジェクトのリソースを安全に開放します。
        /// </summary>
        public void Dispose()
        {
            // このコードを変更しないでください。クリーンアップ コードを 'Dispose(bool disposing)' メソッドに記述します
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}