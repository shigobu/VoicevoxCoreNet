using System;
using System.Text;
using VoicevoxCoreNet.Native;
using System.Runtime.InteropServices;

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

#region プロパティ
        /// <summary>
        /// ハードウェアアクセラレーションがGPUモードかどうかを取得します。
        /// </summary>
        /// <value>GPUモードならtrue、そうでないならfalse</value>
        public bool IsGpuMode
        {
            get
            {
                return CoreNative.voicevox_is_gpu_mode();
            }
        }

#endregion

#region メソッド
        /// <summary>
        /// モデルを読み込む
        /// </summary>
        /// <param name="speakerId">読み込むモデルの話者ID</param>
        public void LoadModel(uint speakerId)
        {
            VoicevoxResultCode resultCode = CoreNative.voicevox_load_model(speakerId);
            VoicevoxCoreException.ThrowIfNotOk(resultCode);
        }

        /// <summary>
        /// 指定したspeakerIdのモデルが読み込まれているか判定する
        /// </summary>
        /// <param name="speakerId">読み込むモデルの話者ID</param>
        /// <returns>モデルが読み込まれているのであればtrue、そうでないならfalse</returns>
        public bool IsModelLoaded(uint speakerId)
        {
            return CoreNative.voicevox_is_model_loaded(speakerId);
        }

        /// <summary>
        /// メタ情報をjsonで取得する
        /// </summary>
        /// <returns>メタ情報のjson文字列</returns>
        public string GetMetasJson()
        {
            IntPtr pJson = CoreNative.voicevox_get_metas_json();
            return Utf8Converter.MarshalNativeUtf8ToManagedString(pJson);
        }

        /// <summary>
        /// サポートデバイス情報をjsonで取得する
        /// </summary>
        /// <returns>サポートデバイス情報のjson文字列</returns>
        public string GetSupportedDevicesJson()
        {
            IntPtr pJson = CoreNative.voicevox_get_supported_devices_json();
            return Utf8Converter.MarshalNativeUtf8ToManagedString(pJson);
        }

        /// <summary>
        /// 音素ごとの長さを推論する
        /// </summary>
        /// <param name="length">phoneme_vector, output のデータ長</param>
        /// <param name="phonemeVector">音素データ</param>
        /// <param name="speakerId">話者ID</param>
        /// <returns>データ</returns>
        public float[] predictDuration(uint length, long[] phonemeVector, uint speakerId)
        {
            IntPtr data = IntPtr.Zero;
            try
            {
                VoicevoxResultCode resultCode = CoreNative.voicevox_predict_duration((UIntPtr)length, phonemeVector, speakerId, out UIntPtr dataLength, out data);
                VoicevoxCoreException.ThrowIfNotOk(resultCode);
                float[] retVal = new float[dataLength.ToUInt32()];
                Marshal.Copy(data, retVal, 0, (int)dataLength.ToUInt32());
                return retVal;
            }
            finally
            {
                if (data != IntPtr.Zero)
                {
                    CoreNative.voicevox_predict_duration_data_free(data);
                }
            }
        }

        /// <summary>
        /// モーラごとのF0を推論する
        /// </summary>
        /// <param name="length">vowel_phoneme_vector, consonant_phoneme_vector, start_accent_vector, end_accent_vector, start_accent_phrase_vector, end_accent_phrase_vector, output のデータ長</param>
        /// <param name="vowelPhonemeVector">母音の音素データ</param>
        /// <param name="consonantPhonemeVector">子音の音素データ</param>
        /// <param name="startAccentVector">開始アクセントデータ</param>
        /// <param name="endAccentVector">終了アクセントデータ</param>
        /// <param name="startAccentPhraseVector">開始アクセントフレーズデータ</param>
        /// <param name="endAccentPhraseVector">終了アクセントフレーズデータ</param>
        /// <param name="speakerId">話者ID</param>
        /// <returns>データ</returns>
        public float[] predictIntonation(uint length, 
                                         long[] vowelPhonemeVector, 
                                         long[] consonantPhonemeVector, 
                                         long[] startAccentVector, 
                                         long[] endAccentVector, 
                                         long[] startAccentPhraseVector, 
                                         long[] endAccentPhraseVector, 
                                         uint speakerId)
        {
            IntPtr data = IntPtr.Zero;
            try
            {
                VoicevoxResultCode resultCode = CoreNative.voicevox_predict_intonation((UIntPtr)length, 
                                                                                       vowelPhonemeVector, 
                                                                                       consonantPhonemeVector, 
                                                                                       startAccentVector, 
                                                                                       endAccentVector, 
                                                                                       startAccentPhraseVector, 
                                                                                       endAccentPhraseVector, 
                                                                                       speakerId, 
                                                                                       out UIntPtr dataLength, 
                                                                                       out data);
                VoicevoxCoreException.ThrowIfNotOk(resultCode);
                float[] retVal = new float[dataLength.ToUInt32()];
                Marshal.Copy(data, retVal, 0, (int)dataLength.ToUInt32());
                return retVal;
            }
            finally
            {
                if (data != IntPtr.Zero)
                {
                    CoreNative.voicevox_predict_intonation_data_free(data);
                }
            }
        }

        /// <summary>
        /// decodeを実行する
        /// </summary>
        /// <param name="length">f0 , output のデータ長及び phoneme のデータ長に関連する</param>
        /// <param name="phonemSize">音素のサイズ phoneme のデータ長に関連する</param>
        /// <param name="f0">基本周波数</param>
        /// <param name="phonemeVector">音素データ</param>
        /// <param name="speakerId">話者ID</param>
        /// <returns>データ</returns>
        public float[] Decode(uint length,
                              uint phonemSize,
                              float[] f0,
                              float[] phonemeVector,
                              uint speakerId)
        {
            IntPtr data = IntPtr.Zero;
            try
            {
                VoicevoxResultCode resultCode = CoreNative.voicevox_decode((UIntPtr)length, 
                                                                           (UIntPtr)phonemSize, 
                                                                           f0, 
                                                                           phonemeVector,  
                                                                           speakerId, 
                                                                           out UIntPtr dataLength, 
                                                                           out data);
                VoicevoxCoreException.ThrowIfNotOk(resultCode);
                float[] retVal = new float[dataLength.ToUInt32()];
                Marshal.Copy(data, retVal, 0, (int)dataLength.ToUInt32());
                return retVal;
            }
            finally
            {
                if (data != IntPtr.Zero)
                {
                    CoreNative.voicevox_decode_data_free(data);
                }
            }
        }

        /// <summary>
        /// AudioQuery を実行する
        /// </summary>
        /// <param name="text">テキスト</param>
        /// <param name="speakerId">話者ID</param>
        /// <param name="options">AudioQueryのオプション</param>
        /// <returns>AudioQuery を json でフォーマットしたもの</returns>
        public string AudioQuery(string text, uint speakerId, VoicevoxAudioQueryOptions options)
        {
            IntPtr pJsonString = IntPtr.Zero;
            try
            {
                byte[] UTF8text = Utf8Converter.GetUTF8ByteWithNullChar(text);
                VoicevoxResultCode resultCode = CoreNative.voicevox_audio_query(UTF8text, speakerId, options, out pJsonString);
                VoicevoxCoreException.ThrowIfNotOk(resultCode);
                return Utf8Converter.MarshalNativeUtf8ToManagedString(pJsonString);
            }
            finally
            {
                if (pJsonString != IntPtr.Zero)
                {
                    CoreNative.voicevox_audio_query_json_free(pJsonString);
                }
            }
        }



#endregion

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