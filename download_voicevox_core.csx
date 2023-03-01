#r "System.Net.Http.dll"
#r "System.Runtime.InteropServices.RuntimeInformation"

using System.Runtime.InteropServices;
using System.Net.Http;
using System.Diagnostics;

public void DownloadMain()
{
    //OS判定
    string os;
    if (OperatingSystem.IsMacOS())
    {
        os = "osx";
    }
    else if (OperatingSystem.IsLinux())
    {
        os = "linux";
    }
    else
    {
        os = "windows";
    }

    //アーキテクチャ判定
    string arch;
    switch (System.Runtime.InteropServices.RuntimeInformation.OSArchitecture)
    {
        case Architecture.Arm64:
            arch = "arm64";
            break;
        case Architecture.X64:
            arch = "x64";
            break;
        default:
            arch = "x64";
            break;
    }

    //拡張子判定
    string ext;
    if (OperatingSystem.IsWindows())
    {
        ext = ".exe";
    }
    else
    {
        ext = "";
    }

    // ダウンローダーのダウンロード。
    string url = string.Format(@"https://github.com/VOICEVOX/voicevox_core/releases/latest/download/download-{0}-{1}{2}", os, arch, ext);
    string outDir = @"./VoicevoxCoreNet.Tests/bin/Debug/net6.0";
    string downloadPath = Path.Combine(outDir, $"downloader{ext}");
    //ダウンローダーがすでに存在していたら、ライブラリのダウンロードは実行済とみなす
    if (File.Exists(downloadPath))
    {
        Console.WriteLine("ダウンローダーがすでに存在しています。");
        return;
    }
    DownloadFileAsync(url, downloadPath).Wait();

    //ダウンローダーに実行権限を付与
    Process process;
    if (OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
    {
        using (process = new Process())
        {
            process.StartInfo.FileName = "chmod";
            process.StartInfo.Arguments = "u+x " + downloadPath;
            process.Start();
            process.WaitForExit();
        }
    }

    //ダウンローダー実行
    Console.WriteLine("ダウンローダー実行");
    using (process = new Process())
    {
        process.StartInfo.FileName = downloadPath;
        process.StartInfo.Arguments = $"--output {outDir}";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.Start();
        process.WaitForExit();
    }
}

static async Task DownloadFileAsync(string fileUrl, string downloadPath)
{
    //ダウンロード
    var client = new HttpClient();
    var response = await client.GetAsync(fileUrl);
    //ステータスコードで成功したかチェック
    if (response.StatusCode != System.Net.HttpStatusCode.OK) return;

    //保存
    using var stream = await response.Content.ReadAsStreamAsync();
    Directory.CreateDirectory(Path.GetDirectoryName(downloadPath));
    using var outStream = File.Create(downloadPath);
    stream.CopyTo(outStream);
}

DownloadMain();