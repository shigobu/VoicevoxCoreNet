namespace VoicevoxCoreNet
{
    /// <summary>
    /// 処理結果を示す結果コード
    /// </summary>
    public enum ResultCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        VOICEVOX_RESULT_OK = 0,
        /// <summary>
        /// open_jtalk辞書ファイルが読み込まれていない
        /// </summary>
        VOICEVOX_RESULT_NOT_LOADED_OPENJTALK_DICT_ERROR = 1,
        /// <summary>
        /// modelの読み込みに失敗した
        /// </summary>
        VOICEVOX_RESULT_LOAD_MODEL_ERROR = 2,
        /// <summary>
        /// サポートされているデバイス情報取得に失敗した
        /// </summary>
        VOICEVOX_RESULT_GET_SUPPORTED_DEVICES_ERROR = 3,
        /// <summary>
        /// GPUモードがサポートされていない
        /// </summary>
        VOICEVOX_RESULT_GPU_SUPPORT_ERROR = 4,
        /// <summary>
        /// メタ情報読み込みに失敗した
        /// </summary>
        VOICEVOX_RESULT_LOAD_METAS_ERROR = 5,
        /// <summary>
        /// ステータスが初期化されていない
        /// </summary>
        VOICEVOX_RESULT_UNINITIALIZED_STATUS_ERROR = 6,
        /// <summary>
        /// 無効なspeaker_idが指定された
        /// </summary>
        VOICEVOX_RESULT_INVALID_SPEAKER_ID_ERROR = 7,
        /// <summary>
        /// 無効なmodel_indexが指定された
        /// </summary>
        VOICEVOX_RESULT_INVALID_MODEL_INDEX_ERROR = 8,
        /// <summary>
        /// 推論に失敗した
        /// </summary>
        VOICEVOX_RESULT_INFERENCE_ERROR = 9,
        /// <summary>
        /// コンテキストラベル出力に失敗した
        /// </summary>
        VOICEVOX_RESULT_EXTRACT_FULL_CONTEXT_LABEL_ERROR = 10,
        /// <summary>
        /// 無効なutf8文字列が入力された
        /// </summary>
        VOICEVOX_RESULT_INVALID_UTF8_INPUT_ERROR = 11,
        /// <summary>
        /// aquestalk形式のテキストの解析に失敗した
        /// </summary>
        VOICEVOX_RESULT_PARSE_KANA_ERROR = 12,
        /// <summary>
        /// 無効なAudioQuery
        /// </summary>
        VOICEVOX_RESULT_INVALID_AUDIO_QUERY_ERROR = 13,
    }
}