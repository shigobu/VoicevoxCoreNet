using System;
using VoicevoxCoreNet.Native;

namespace VoicevoxCoreNet
{


    /// <summary>
    /// VOICEVOX CORE のラッパーです
    /// </summary>
    public class Core : IDisposable
    {
        private bool disposedValue;

        /// <summary>
        /// VoicevoxInitializeOptionsを使用して、オブジェクトの初期化を行います。
        /// </summary>
        /// <param name="options">初期化オプション</param>
        Core(VoicevoxInitializeOptions options)
        {
            VoicevoxResultCode resultCode = CoreNative.voicevox_initialize(options);
            VoicevoxCoreException.ThrowIfNotOk(resultCode);
        }

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

                CoreNative.voicevox_finalize();
                disposedValue = true;
            }
        }

        /// <summary>
        /// ファイナライザー
        /// </summary>
        ~Core()
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