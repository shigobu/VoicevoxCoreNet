# VoicevoxCoreNet
[VOICEVOX CORE](https://github.com/VOICEVOX/voicevox_core) の.NETラッパーです。C#で記述されています。VB.NETやF#,C++/CLIでも使えるはずです。  
OSに依存するコード含んでいないので、マルチプラットフォーム(Windows,mac,Linux等)で動きます。

## ビルド方法
ビルドには、.NET sdk が必要です。最新版をインストールしてください。

```
dotnet build
```

Visual Studioでビルドもできます。

## テスト
テストを実行するには、voicevox_coreが必要です。下記のコマンドで簡単にダウンロードできます。詳細は「ダウンローダーをダウンロードして実行するスクリプト」の項参照。
```
dotnet script ./download_voicevox_core.csx
```

テストの実行は、以下のコマンドです。
```
dotnet test
```

## ダウンローダーをダウンロードして実行するスクリプト
voicevox_coreのダウンローダーをダウンロードして実行するC#スクリプトが含まれています。これは、実行中のOSとCPUアーキテクチャを自動で判断し、適切なダウンローダーをダウンロードし、実行します。ダウンローダーの引数には`--output`のみ指定されるので、CPU版がダウンロードされます。  
実行するには、.NET sdkと`dotnet-script`ツールが必要です。`dotnet-script`がインストールされていない場合は、以下のコマンドを使用してインストールできます。
```
dotnet tool install -g dotnet-script
```

実行するには、以下のコマンドを実行します。
```
dotnet script ./download_voicevox_core.csx
```

1つ目の引数で、出力先を指定できます。
```
dotnet script ./download_voicevox_core.csx -- ./hoo/bar
```

出力先の指定を省略した場合、規定の出力先は以下の場所です。これは、テストを実行するときに使います。
```
./VoicevoxCoreNet.Tests/bin/Debug/net6.0
```

## 開発環境
Windows 10  
Microsoft Visual Studio Community 2022  
C#  
.NET Standard 2.0
