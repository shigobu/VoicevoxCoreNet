using System.Runtime.InteropServices;
using VoicevoxCoreNet.Native;

namespace VoicevoxCoreNet
{
    /// <summary>
    /// テキスト音声合成オプション
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TtsOptions
    {
        /// <summary>
        /// 初期値でオブジェクトを初期化します。
        /// </summary>
        public TtsOptions()
        {
            TtsOptions options = CoreNative.voicevox_make_default_tts_options();
            kana = options.kana;
            enable_interrogative_upspeak = options.enable_interrogative_upspeak;
        }

        /// <summary>
        /// aquestalk形式のkanaとしてテキストを解釈する
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool kana;

        /// <summary>
        /// 疑問文の調整を有効にする
        /// </summary>
        [MarshalAs(UnmanagedType.U1)]
        public bool enable_interrogative_upspeak;
    }
}