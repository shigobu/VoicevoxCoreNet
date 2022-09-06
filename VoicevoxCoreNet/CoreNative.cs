using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VoicevoxCoreNet
{
    /// <summary>
    /// coreライブラリのDllImport実装
    /// </summary>
    internal class CoreNative
    {
        /// <summary>
        /// ライブラリ名
        /// </summary>
        /// <remarks>
        /// 拡張子を省く事によって、osごとのファイル名違いを吸収できる。
        /// https://www.mono-project.com/docs/advanced/pinvoke/
        /// </remarks>
        const string dllName = "core";

        /// <summary>
        /// デフォルトの初期化オプションを生成する
        /// </summary>
        /// <returns>デフォルト値が設定された初期化オプション</returns>
        [DllImport(dllName)]
        extern internal static VoicevoxInitializeOptions voicevox_make_default_initialize_options();

        /// <summary>
        /// 初期化する
        /// </summary>
        /// <param name="options">初期化オプション</param>
        /// <returns>結果コード</returns>
        [DllImport(dllName)]
        extern internal static VoicevoxResultCode voicevox_initialize(VoicevoxInitializeOptions options);

        /// <summary>
        /// モデルを読み込む
        /// </summary>
        /// <param name="speaker_id">読み込むモデルの話者ID</param>
        /// <returns>結果コード</returns>
        [DllImport(dllName)]
        extern internal static VoicevoxResultCode voicevox_load_model(uint speaker_id);

        /// <summary>
        /// ハードウェアアクセラレーションがGPUモードか判定する
        /// </summary>
        /// <returns>GPUモードならtrue、そうでないならfalse</returns>
        [DllImport(dllName)]
        extern internal static bool voicevox_is_gpu_mode();

        /// <summary>
        /// 指定したspeaker_idのモデルが読み込まれているか判定する
        /// </summary>
        /// <param name="speaker_id">読み込むモデルの話者ID</param>
        /// <returns>モデルが読み込まれているのであればtrue、そうでないならfalse</returns>
        [DllImport(dllName)]
        extern internal static bool voicevox_is_model_loaded(uint speaker_id);

        /// <summary>
        /// このライブラリの利用を終了し、確保しているリソースを解放する
        /// </summary>
        [DllImport(dllName)]
        extern internal static void voicevox_finalize();

        /// <summary>
        /// メタ情報をjsonで取得する
        /// </summary>
        /// <returns>メタ情報のjson文字列</returns>
        [DllImport(dllName)]
        extern internal static IntPtr voicevox_get_metas_json();

        /// <summary>
        /// サポートデバイス情報をjsonで取得する
        /// </summary>
        /// <returns>サポートデバイス情報のjson文字列</returns>
        [DllImport(dllName)]
        extern internal static IntPtr voicevox_get_supported_devices_json();

        /// <summary>
        /// 音素ごとの長さを推論する
        /// </summary>
        /// <param name="length">phoneme_vector, output のデータ長</param>
        /// <param name="phoneme_vector">音素データ</param>
        /// <param name="speaker_id">話者ID</param>
        /// <param name="output_predict_duration_data_length">出力データのサイズ</param>
        /// <param name="output_predict_duration_data">データの出力先。元の型:float **</param>
        /// <returns>結果コード</returns>
        /// <remarks>
        /// # Safety
        /// @param phoneme_vector 必ずlengthの長さだけデータがある状態で渡すこと
        /// @param output_predict_duration_data_length uintptr_t 分のメモリ領域が割り当てられていること
        /// @param output_predict_duration_data 成功後にメモリ領域が割り当てられるので::voicevox_predict_duration_data_free で解放する必要がある
        /// </remarks>
        [DllImport(dllName)]
        extern internal static VoicevoxResultCode voicevox_predict_duration(UIntPtr length,
                                                                            long[] phoneme_vector,
                                                                            uint speaker_id,
                                                                            out UIntPtr output_predict_duration_data_length,
                                                                            out IntPtr output_predict_duration_data);

        /// <summary>
        /// voicevox_predict_durationで出力されたデータを解放する
        /// </summary>
        /// <param name="predict_duration_data">確保されたメモリ領域</param>
        [DllImport(dllName)]
        extern internal static void voicevox_predict_duration_data_free(IntPtr predict_duration_data);

        /// <summary>
        /// モーラごとのF0を推論す
        /// </summary>
        /// <param name="length">vowel_phoneme_vector, consonant_phoneme_vector, start_accent_vector, end_accent_vector, start_accent_phrase_vector, end_accent_phrase_vector, output のデータ長</param>
        /// <param name="vowel_phoneme_vector">母音の音素データ</param>
        /// <param name="consonant_phoneme_vector">子音の音素データ</param>
        /// <param name="start_accent_vector">開始アクセントデータ</param>
        /// <param name="end_accent_vector">終了アクセントデータ</param>
        /// <param name="start_accent_phrase_vector">開始アクセントフレーズデータ</param>
        /// <param name="end_accent_phrase_vector">終了アクセントフレーズデータ</param>
        /// <param name="speaker_id"> 話者ID</param>
        /// <param name="output_predict_intonation_data_length">出力データのサイズ</param>
        /// <param name="output_predict_intonation_data">データの出力先。元の型:float **</param>
        /// <returns>結果コード</returns>
        /// <remarks>
        /// # Safety
        /// @param vowel_phoneme_vector 必ずlengthの長さだけデータがある状態で渡すこと
        /// @param consonant_phoneme_vector 必ずlengthの長さだけデータがある状態で渡すこと
        /// @param start_accent_vector 必ずlengthの長さだけデータがある状態で渡すこと
        /// @param end_accent_vector 必ずlengthの長さだけデータがある状態で渡すこと
        /// @param start_accent_phrase_vector 必ずlengthの長さだけデータがある状態で渡すこと
        /// @param end_accent_phrase_vector 必ずlengthの長さだけデータがある状態で渡すこと
        /// @param output_predict_intonation_data_length uintptr_t 分のメモリ領域が割り当てられていること
        /// @param output_predict_intonation_data 成功後にメモリ領域が割り当てられるので::voicevox_predict_intonation_data_free で解放する必要がある
        /// </remarks>
        [DllImport(dllName)]
        extern internal static VoicevoxResultCode voicevox_predict_intonation(UIntPtr length,
                                                                              long[] vowel_phoneme_vector,
                                                                              long[] consonant_phoneme_vector,
                                                                              long[] start_accent_vector,
                                                                              long[] end_accent_vector,
                                                                              long[] start_accent_phrase_vector,
                                                                              long[] end_accent_phrase_vector,
                                                                              uint speaker_id,
                                                                              out UIntPtr output_predict_intonation_data_length,
                                                                              out IntPtr output_predict_intonation_data);

        /// <summary>
        /// voicevox_predict_intonationで出力されたデータを解放する
        /// </summary>
        /// <param name="predict_intonation_data">確保されたメモリ領域</param>
        [DllImport(dllName)]
        extern internal static void voicevox_predict_intonation_data_free(IntPtr predict_intonation_data);

        /// <summary>
        /// decodeを実行する
        /// </summary>
        /// <param name="length">f0 , output のデータ長及び phoneme のデータ長に関連する</param>
        /// <param name="phoneme_size">音素のサイズ phoneme のデータ長に関連する</param>
        /// <param name="f0">基本周波数</param>
        /// <param name="phoneme_vector">音素データ</param>
        /// <param name="speaker_id">話者ID</param>
        /// <param name="output_decode_data_length">出力先データのサイズ</param>
        /// <param name="output_decode_data">データ出力先。元の型:float **</param>
        /// <returns>結果コード</returns>
        /// <remarks>
        /// # Safety
        /// @param f0 必ず length の長さだけデータがある状態で渡すこと
        /// @param phoneme_vector 必ず length* phoneme_size の長さだけデータがある状態で渡すこと
        /// @param output_decode_data_length uintptr_t 分のメモリ領域が割り当てられていること
        /// @param output_decode_data 成功後にメモリ領域が割り当てられるので ::voicevox_decode_data_free で解放する必要がある
        /// </remarks>
        [DllImport(dllName)]
        extern internal static VoicevoxResultCode voicevox_decode(UIntPtr length,
                                                                  UIntPtr phoneme_size,
                                                                  float[] f0,
                                                                  float[] phoneme_vector,
                                                                  uint speaker_id,
                                                                  out UIntPtr output_decode_data_length,
                                                                  out IntPtr output_decode_data);

        /// <summary>
        /// voicevox_decodeで出力されたデータを解放する
        /// </summary>
        /// <param name="decode_data">確保されたメモリ領域</param>
        [DllImport(dllName)]
        extern internal static void voicevox_decode_data_free(IntPtr decode_data);

        /// <summary>
        /// デフォルトの AudioQuery のオプションを生成する
        /// </summary>
        /// <returns>デフォルト値が設定された AudioQuery オプション</returns>
        [DllImport(dllName)]
        extern internal static VoicevoxAudioQueryOptions voicevox_make_default_audio_query_options();

        /// <summary>
        /// AudioQuery を実行する
        /// </summary>
        /// <param name="text">テキスト null終端UTF8</param>
        /// <param name="speaker_id">話者ID</param>
        /// <param name="options">AudioQueryのオプション</param>
        /// <param name="output_audio_query_json">AudioQuery を json でフォーマットしたもの</param>
        /// <returns>結果コード</returns>
        /// <remarks>
        /// # Safety
        /// @param text null終端文字列であること
        /// @param output_audio_query_json 自動でheapメモリが割り当てられるので ::voicevox_audio_query_json_free で解放する必要がある
        /// </remarks>
        [DllImport(dllName)]
        extern internal static VoicevoxResultCode voicevox_audio_query(byte[] text,
                                                                       uint speaker_id,
                                                                       VoicevoxAudioQueryOptions options,
                                                                       out IntPtr output_audio_query_json);

        /// <summary>
        /// AudioQuery から音声合成する
        /// </summary>
        /// <param name="audio_query_json">jsonフォーマットされた AudioQuery null終端UTF8</param>
        /// <param name="speaker_id">話者ID</param>
        /// <param name="options">AudioQueryから音声合成オプション</param>
        /// <param name="output_wav_length">出力する wav データのサイズ</param>
        /// <param name="output_wav">データの出力先</param>
        /// <returns>結果コード</returns>
        /// <remarks>
        /// # Safety
        /// @param output_wav_length 出力先の領域が確保された状態でpointerに渡されていること
        /// @param output_wav 自動で output_wav_length 分のデータが割り当てられるので ::voicevox_wav_free で解放する必要がある
        /// </remarks>
        [DllImport(dllName)]
        extern internal static VoicevoxResultCode voicevox_synthesis(byte[] audio_query_json,
                                                                     uint speaker_id,
                                                                     VoicevoxSynthesisOptions options,
                                                                     out UIntPtr output_wav_length,
                                                                     out IntPtr output_wav);

        /// <summary>
        /// デフォルトのテキスト音声合成オプションを生成する
        /// </summary>
        /// <returns>テキスト音声合成オプション</returns>
        [DllImport(dllName)]
        extern internal static VoicevoxTtsOptions voicevox_make_default_tts_options();

        /// <summary>
        /// テキスト音声合成を実行する
        /// </summary>
        /// <param name="text">テキスト null終端UTF8</param>
        /// <param name="speaker_id">話者ID</param>
        /// <param name="options">テキスト音声合成オプション</param>
        /// <param name="output_wav_length">出力する wav データのサイズ</param>
        /// <param name="output_wav">wav データの出力先</param>
        /// <returns>結果コード</returns>
        /// <remarks>
        /// # Safety
        /// @param output_wav_length 出力先の領域が確保された状態でpointerに渡されていること
        /// @param output_wav は自動で output_wav_length 分のデータが割り当てられるので ::voicevox_wav_free で解放する必要がある
        /// </remarks>
        [DllImport(dllName)]
        extern internal static VoicevoxResultCode voicevox_tts(byte[] text,
                                                               uint speaker_id,
                                                               VoicevoxTtsOptions options,
                                                               out UIntPtr output_wav_length,
                                                               out IntPtr output_wav);

        /// <summary>
        /// jsonフォーマットされた AudioQuery データのメモリを解放する
        /// </summary>
        /// <param name="audio_query_json">解放する json フォーマットされた AudioQuery データ</param>
        /// <remarks>
        /// # Safety
        /// @param wav 確保したメモリ領域が破棄される
        /// </remarks>
        [DllImport(dllName)]
        extern internal static void voicevox_audio_query_json_free(IntPtr audio_query_json);

        /// <summary>
        /// wav データのメモリを解放する
        /// </summary>
        /// <param name="wav">解放する wav データ</param>
        /// <remarks>
        /// # Safety
        /// @param wav 確保したメモリ領域が破棄される
        /// </remarks>
        [DllImport(dllName)]
        extern internal static void voicevox_wav_free(IntPtr wav);

        /// <summary>
        /// エラー結果をメッセージに変換する
        /// </summary>
        /// <param name="result_code">メッセージに変換する result_code</param>
        /// <returns>結果コードを元に変換されたメッセージ文字列 null終端utf8</returns>
        [DllImport(dllName)]
        extern internal static IntPtr voicevox_error_result_to_message(VoicevoxResultCode result_code);

    }
}
