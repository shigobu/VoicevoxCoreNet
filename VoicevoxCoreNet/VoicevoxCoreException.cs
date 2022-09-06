using System;
using System.Collections.Generic;
using System.Text;
using VoicevoxCoreNet.Native;

namespace VoicevoxCoreNet
{
    /// <summary>
    /// VOICEVOX COREで発生したエラーを表します。
    /// </summary>
    public class VoicevoxCoreException : Exception
    {
        /// <summary>
        /// VoicevoxResultCodeを使用して、オブジェクトを初期化します。
        /// </summary>
        /// <param name="resultCode">結果コード</param>
        public VoicevoxCoreException(VoicevoxResultCode resultCode) : base()
        {
            IntPtr pMessage = CoreNative.voicevox_error_result_to_message(resultCode);
            _message = Utf8Converter.MarshalNativeUtf8ToManagedString(pMessage);
        }

        private string _message;

        /// <inheritdoc/>
        public override string Message { get => _message; }
    }
}
