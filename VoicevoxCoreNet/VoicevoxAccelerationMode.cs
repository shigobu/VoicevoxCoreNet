namespace VoicevoxCoreNet
{
    /// <summary>
    /// ハードウェアアクセラレーションモードを設定する設定値
    /// </summary>
    public enum VoicevoxAccelerationMode
    {
        /// <summary>
        /// 実行環境に合った適切なハードウェアアクセラレーションモードを選択する
        /// </summary>
        VOICEVOX_ACCELERATION_MODE_AUTO = 0,
        /// <summary>
        /// ハードウェアアクセラレーションモードを"CPU"に設定する
        /// </summary>
        VOICEVOX_ACCELERATION_MODE_CPU = 1,
        /// <summary>
        /// ハードウェアアクセラレーションモードを"GPU"に設定する
        /// </summary>
        VOICEVOX_ACCELERATION_MODE_GPU = 2,
    };
}