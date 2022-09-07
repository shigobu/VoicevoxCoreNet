using System.Runtime.InteropServices;
using VoicevoxCoreNet.Native;

namespace VoicevoxCoreNet
{
    /// <summary>
    /// Audio query のオプション
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct AudioQueryOptions
    {
        /// <summary>
        /// オブジェクトを初期値で初期化します。
        /// </summary>
        public AudioQueryOptions()
        {
            AudioQueryOptions options = CoreNative.voicevox_make_default_audio_query_options();
            kana = options.kana;
        }

        /// <summary>
        /// aquestalk形式のkanaとしてテキストを解釈する
        /// </summary>
        public bool kana;
    }
}