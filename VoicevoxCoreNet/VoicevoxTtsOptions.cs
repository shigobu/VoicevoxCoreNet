using System.Runtime.InteropServices;

namespace VoicevoxCoreNet
{
    /// <summary>
    /// テキスト音声合成オプション
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    struct VoicevoxTtsOptions
    {
        /// <summary>
        /// aquestalk形式のkanaとしてテキストを解釈する
        /// </summary>
        bool kana;
        /// <summary>
        /// 疑問文の調整を有効にする
        /// </summary>
        bool enable_interrogative_upspeak;
    }
}