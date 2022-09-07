using System;
using System.Text;
using VoicevoxCoreNet.Native;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;

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
        Core(InitializeOptions options)
        {
            ResultCode resultCode = CoreNative.voicevox_initialize(options);
            CoreException.ThrowIfNotOk(resultCode);
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
            ResultCode resultCode = CoreNative.voicevox_load_model(speakerId);
            CoreException.ThrowIfNotOk(resultCode);
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
        /// <param name="phonemeVector">音素データ</param>
        /// <param name="speakerId">話者ID</param>
        /// <returns>データ</returns>
        public float[] PredictDuration(long[] phonemeVector, uint speakerId)
        {
            IntPtr data = IntPtr.Zero;
            try
            {
                ResultCode resultCode = CoreNative.voicevox_predict_duration((UIntPtr)phonemeVector.Length, phonemeVector, speakerId, out UIntPtr dataLength, out data);
                CoreException.ThrowIfNotOk(resultCode);
                float[] retVal = new float[dataLength.ToUInt32()];
                Marshal.Copy(data, retVal, 0, retVal.Length);
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
        /// <param name="vowelPhonemeVector">母音の音素データ</param>
        /// <param name="consonantPhonemeVector">子音の音素データ</param>
        /// <param name="startAccentVector">開始アクセントデータ</param>
        /// <param name="endAccentVector">終了アクセントデータ</param>
        /// <param name="startAccentPhraseVector">開始アクセントフレーズデータ</param>
        /// <param name="endAccentPhraseVector">終了アクセントフレーズデータ</param>
        /// <param name="speakerId">話者ID</param>
        /// <returns>データ</returns>
        public float[] predictIntonation(long[] vowelPhonemeVector, 
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
                ResultCode resultCode = CoreNative.voicevox_predict_intonation((UIntPtr)vowelPhonemeVector.Length, 
                                                                                       vowelPhonemeVector, 
                                                                                       consonantPhonemeVector, 
                                                                                       startAccentVector, 
                                                                                       endAccentVector, 
                                                                                       startAccentPhraseVector, 
                                                                                       endAccentPhraseVector, 
                                                                                       speakerId, 
                                                                                       out UIntPtr dataLength, 
                                                                                       out data);
                CoreException.ThrowIfNotOk(resultCode);
                float[] retVal = new float[dataLength.ToUInt32()];
                Marshal.Copy(data, retVal, 0, retVal.Length);
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
        /// <param name="f0">基本周波数</param>
        /// <param name="phonemeVector">音素データ</param>
        /// <param name="speakerId">話者ID</param>
        /// <returns>データ</returns>
        public float[] Decode(float[] f0, float[] phonemeVector, uint speakerId)
        {
            IntPtr data = IntPtr.Zero;
            try
            {
                ResultCode resultCode = CoreNative.voicevox_decode((UIntPtr)f0.Length, 
                                                                           (UIntPtr)phonemeVector.Length, 
                                                                           f0, 
                                                                           phonemeVector,  
                                                                           speakerId, 
                                                                           out UIntPtr dataLength, 
                                                                           out data);
                CoreException.ThrowIfNotOk(resultCode);
                float[] retVal = new float[dataLength.ToUInt32()];
                Marshal.Copy(data, retVal, 0, retVal.Length);
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
        public string AudioQuery(string text, uint speakerId, AudioQueryOptions options)
        {
            IntPtr pJsonString = IntPtr.Zero;
            try
            {
                byte[] UTF8text = Utf8Converter.GetUTF8ByteWithNullChar(text);
                ResultCode resultCode = CoreNative.voicevox_audio_query(UTF8text, speakerId, options, out pJsonString);
                CoreException.ThrowIfNotOk(resultCode);
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

        /// <summary>
        /// AudioQuery から音声合成する
        /// </summary>
        /// <param name="audioQueryJson">jsonフォーマットされた AudioQuery</param>
        /// <param name="speakerId">話者ID</param>
        /// <param name="options">AudioQueryから音声合成オプション</param>
        /// <returns>wavデータ</returns>
        public byte[] Synthesis(string audioQueryJson, uint speakerId, SynthesisOptions options)
        {
            IntPtr pWaveData = IntPtr.Zero;
            try
            {
                byte[] UTF8text = Utf8Converter.GetUTF8ByteWithNullChar(audioQueryJson);
                ResultCode resultCode = CoreNative.voicevox_synthesis(UTF8text, speakerId, options, out UIntPtr outputWavLength, out pWaveData);
                CoreException.ThrowIfNotOk(resultCode);
                byte[] wavData = new byte[outputWavLength.ToUInt32()];
                Marshal.Copy(pWaveData, wavData, 0, wavData.Length);
                return wavData;
            }
            finally
            {
                if (pWaveData != IntPtr.Zero)
                {
                    CoreNative.voicevox_wav_free(pWaveData);
                }
            }
        }

        /// <summary>
        /// テキスト音声合成を実行する
        /// </summary>
        /// <param name="text">テキスト</param>
        /// <param name="speakerId">話者ID</param>
        /// <param name="options">テキスト音声合成オプション</param>
        /// <returns>wavデータ</returns>
        public byte[] Tts(string text, uint speakerId, TtsOptions options)
        {
            IntPtr pWaveData = IntPtr.Zero;
            try
            {
                byte[] UTF8text = Utf8Converter.GetUTF8ByteWithNullChar(text);
                ResultCode resultCode = CoreNative.voicevox_tts(UTF8text, speakerId, options, out UIntPtr outputWavLength, out pWaveData);
                CoreException.ThrowIfNotOk(resultCode);
                byte[] wavData = new byte[outputWavLength.ToUInt32()];
                Marshal.Copy(pWaveData, wavData, 0, wavData.Length);
                return wavData;
            }
            finally
            {
                if (pWaveData != IntPtr.Zero)
                {
                    CoreNative.voicevox_wav_free(pWaveData);
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