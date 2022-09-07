using System.Runtime.InteropServices;

namespace VoicevoxCoreNet
{
    /// <summary>
    /// voicevox_synthesis` のオプション
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SynthesisOptions
    {
        /// <summary>
        /// 疑問文の調整を有効にする
        /// </summary>
        public bool enable_interrogative_upspeak;
    }
}