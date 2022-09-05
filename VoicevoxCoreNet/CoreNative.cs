using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VoicevoxCoreNet
{
    internal class CoreNative
    {
        /// <summary>
        /// デフォルトの初期化オプションを生成する
        /// </summary>
        /// <returns>デフォルト値が設定された初期化オプション</returns>
        [DllImport("core.dll")]
        extern internal static VoicevoxInitializeOptions voicevox_make_default_initialize_options();

        /// <summary>
        /// 初期化する
        /// </summary>
        /// <param name="options">初期化オプション</param>
        /// <returns>結果コード</returns>
        [DllImport("core.dll")]
        extern internal static VoicevoxResultCode voicevox_initialize(VoicevoxInitializeOptions options);

        /// <summary>
        /// モデルを読み込む
        /// </summary>
        /// <param name="speaker_id">読み込むモデルの話者ID</param>
        /// <returns>結果コード</returns>
        [DllImport("core.dll")]
        extern internal static VoicevoxResultCode voicevox_load_model(uint speaker_id);

        /// <summary>
        /// ハードウェアアクセラレーションがGPUモードか判定する
        /// </summary>
        /// <returns>GPUモードならtrue、そうでないならfalse</returns>
        [DllImport("core.dll")]
        extern internal static bool voicevox_is_gpu_mode();

        /// <summary>
        /// 指定したspeaker_idのモデルが読み込まれているか判定する
        /// </summary>
        /// <param name="speaker_id">読み込むモデルの話者ID</param>
        /// <returns>モデルが読み込まれているのであればtrue、そうでないならfalse</returns>
        [DllImport("core.dll")]
        extern internal static bool voicevox_is_model_loaded(uint speaker_id);

        /// <summary>
        /// このライブラリの利用を終了し、確保しているリソースを解放する
        /// </summary>
        [DllImport("core.dll")]
        extern internal static void voicevox_finalize();

        /// <summary>
        /// メタ情報をjsonで取得する
        /// </summary>
        /// <returns>メタ情報のjson文字列</returns>
        [DllImport("core.dll")]
        [return : MarshalAs(UnmanagedType.LPUTF8Str)]
        extern internal static string voicevox_get_metas_json();

        /// <summary>
        /// サポートデバイス情報をjsonで取得する
        /// </summary>
        /// <returns>サポートデバイス情報のjson文字列</returns>
        [DllImport("core.dll")]
        [return: MarshalAs(UnmanagedType.LPUTF8Str)]
        extern internal static string voicevox_get_supported_devices_json();

        /// <summary>
        /// 音素ごとの長さを推論する
        /// </summary>
        /// <param name="length">phoneme_vector, output のデータ長</param>
        /// <param name="phoneme_vector">音素データ</param>
        /// <param name="speaker_id">話者ID</param>
        /// <param name="output_predict_duration_data_length">出力データのサイズ</param>
        /// <param name="output_predict_duration_data">データの出力先</param>
        /// <returns>結果コード</returns>
        /// <remarks>
        /// # Safety
        /// @param phoneme_vector 必ずlengthの長さだけデータがある状態で渡すこと
        /// @param output_predict_duration_data_length uintptr_t 分のメモリ領域が割り当てられていること
        /// @param output_predict_duration_data 成功後にメモリ領域が割り当てられるので::voicevox_predict_duration_data_free で解放する必要がある
        /// </remarks>
        [DllImport("core.dll")]
        extern internal static VoicevoxResultCode voicevox_predict_duration(UIntPtr length,
                                                                            long[] phoneme_vector,
                                                                            uint speaker_id,
                                                                            out UIntPtr output_predict_duration_data_length,
                                                                            out IntPtr output_predict_duration_data);

        /// <summary>
        /// voicevox_predict_durationで出力されたデータを解放する
        /// </summary>
        /// <param name="predict_duration_data">確保されたメモリ領域</param>
        [DllImport("core.dll")]
        extern internal static void voicevox_predict_duration_data_free(IntPtr predict_duration_data);

    }
}
