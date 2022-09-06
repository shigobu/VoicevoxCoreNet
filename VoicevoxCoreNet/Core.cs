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
            ThrowVoicevoxCoreException(resultCode);
        }

        /// <summary>
        /// デフォルトの初期化オプションを生成する
        /// </summary>
        /// <returns>デフォルト値が設定された初期化オプション</returns>
        public static VoicevoxInitializeOptions MakeDefaultInitializeOption()
        {
            return CoreNative.voicevox_make_default_initialize_options();
        }

        /// <summary>
        /// 結果コードが成功以外のときに、例外を投げます。
        /// </summary>
        /// <param name="resultCode"></param>
        /// <exception cref="VoicevoxCoreException">結果コードは成功ではありませんでした。</exception>
        private static void ThrowVoicevoxCoreException(VoicevoxResultCode resultCode)
        {
            if (resultCode != VoicevoxResultCode.VOICEVOX_RESULT_OK)
            {
                throw new VoicevoxCoreException(resultCode);
            }
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