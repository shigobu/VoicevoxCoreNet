using System.Runtime.InteropServices;

namespace VoicevoxCoreNet
{
    /// <summary>
    /// Audio query のオプション
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    struct VoicevoxAudioQueryOptions
    {
        /// <summary>
        /// aquestalk形式のkanaとしてテキストを解釈する
        /// </summary>
        bool kana;
    }
}