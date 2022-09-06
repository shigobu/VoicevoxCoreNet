using System;
using System.Collections.Generic;
using System.Text;

namespace VoicevoxCoreNet
{
    internal class Utf8Converter
    {
        public static unsafe string MarshalNativeUtf8ToManagedString(IntPtr pStringUtf8)
            => MarshalNativeUtf8ToManagedString((byte*)pStringUtf8);

        public static unsafe string MarshalNativeUtf8ToManagedString(byte* pStringUtf8)
        {
            var len = 0;
            while (pStringUtf8[len] != 0) len++;
            return Encoding.UTF8.GetString(pStringUtf8, len);
        }
    }
}
