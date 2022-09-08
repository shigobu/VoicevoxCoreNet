namespace VoicevoxCoreNet.Tests;

public class VoicevoxCoreNet_IsEvenShould
{
    [Fact]
    public void CoreObject_Create()
    {
        GetDefaultCoreObject();
    }

    [Fact]
    public void CoreObject_LoadModel()
    {
        Core core = GetDefaultCoreObject();
        core.LoadModel(0);
    }

    /// <summary>
    /// デフォルトのcoreオブジェクトを取得します。
    /// </summary>
    /// <returns></returns>
    public Core GetDefaultCoreObject()
    {
        InitializeOptions options = new InitializeOptions();
        options.OpenJtalkDictDir = GetOpenJtalkDictDir();
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