namespace VoicevoxCoreNet.Tests;

public class VoicevoxCoreNet_IsEvenShould
{
    [Fact]
    public void CoreObject_Create()
    {
        GetDefaultCoreObject();
    }

    [Fact]
    public void CoreObject_IsGpuMode()
    {
        // Given
        Core core = GetDefaultCoreObject();
        // When
        bool isGpuMode = core.IsGpuMode;
        // Then
        Assert.False(isGpuMode);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void CoreObject_LoadModel(uint speakerId)
    {
        Core core = GetDefaultCoreObject();
        core.LoadModel(speakerId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void CoreObject_IsModelLoaded(uint speakerId)
    {
        // Given
        Core core = GetDefaultCoreObject();
        // When
        bool isModelLoaded = core.IsModelLoaded(speakerId);
        // Then
        Assert.False(isModelLoaded);
    }

    [Fact]
    public void CoreObject_GetMetasJson()
    {
        // Given
        Core core = GetDefaultCoreObject();
        // When
        string json = core.GetMetasJson();
        // Then
        Assert.NotEmpty(json);
    }

    [Fact]
    public void CoreObjct_GetSupportedDevicesJson()
    {
        // Given
        Core core = GetDefaultCoreObject();
        // When
        string json = core.GetSupportedDevicesJson();
        // Then
        Assert.NotEmpty(json);
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(0, false)]
    [InlineData(1, true)]
    [InlineData(1, false)]
    public void CoreObject_AudioQuery(uint speakerId, bool kana)
    {
        // Given
        Core core = GetDefaultCoreObject();
        core.LoadModel(speakerId);
        AudioQueryOptions options = new AudioQueryOptions()
        {
            kana = kana
        };
        // When
        string json = core.AudioQuery("ディイプラ'アニングワ/バンノ'オヤクデワ/アリマセ'ン", speakerId, options);
        // Then
        Assert.NotEmpty(json);
    }

    [Theory]
    [InlineData(0, true)]
    [InlineData(0, false)]
    [InlineData(1, true)]
    [InlineData(1, false)]
    public void CoreObject_Synthesis(uint speakerId, bool enableInterrogativeUpspeak)
    {
        // Given
        Core core = GetDefaultCoreObject();
        core.LoadModel(speakerId);
        AudioQueryOptions audioQueryOptions = new AudioQueryOptions()
        {
            kana = false
        };
        string json = core.AudioQuery("あいうえお", speakerId, audioQueryOptions);
        SynthesisOptions synthesisOptions = new SynthesisOptions()
        {
            enable_interrogative_upspeak = enableInterrogativeUpspeak
        };
        // When
        byte[] wavData = core.Synthesis(json, speakerId, synthesisOptions);
        // Then
        Assert.NotEmpty(wavData);
    }

    /// <summary>
    /// デフォルトのcoreオブジェクトを取得します。
    /// </summary>
    /// <returns></returns>
    public Core GetDefaultCoreObject()
    {
        InitializeOptions options = new InitializeOptions(GetOpenJtalkDictDir());
        options.LoadAllModels = false;
        return new Core(options);
    }

    /// <summary>
    /// OpenJtalk辞書のパスを取得します。
    /// </summary>
    /// <returns>OpenJtalk辞書のパス</returns>
    public string GetOpenJtalkDictDir()
    {
        return Path.Combine(GetThisAppDirectory() ?? "", "open_jtalk_dic_utf_8-1.11");
    }

    /// <summary>
    /// 現在実行中のコードを含むアセンブリを返します。
    /// </summary>
    /// <returns></returns>
    static public Assembly GetThisAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }

    /// <summary>
    /// 実行中のコードを格納しているアセンブリのある場所を返します。
    /// </summary>
    /// <returns></returns>
    static public string? GetThisAppDirectory()
    {
        string appPath = GetThisAssembly().Location;
        return Path.GetDirectoryName(appPath);
    }


}