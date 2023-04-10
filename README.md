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
dotnet script ./tools/download_voicevox_core.csx -- --output ./VoicevoxCoreNet.Tests/bin/Debug/net6.0
```

テストの実行は、以下のコマンドです。
```
dotnet test
```

## ダウンローダーをダウンロードして実行するスクリプト
voicevox_coreのダウンローダーをダウンロードして実行するC#スクリプトが含まれています。これは、実行中のOSとCPUアーキテクチャを自動で判断し、適切なダウンローダーをダウンロードし、実行します。  
実行するには、.NET sdkと`dotnet-script`ツールが必要です。`dotnet-script`がインストールされていない場合は、以下のコマンドを使用してインストールできます。
```
dotnet tool install -g dotnet-script
```

実行するには、以下のコマンドを実行します。
```
dotnet script ./tools/download_voicevox_core.csx
```

ダウンローダーに引数を渡したい場合は、` -- `を書いた後に引数を書いてください。そのままダウンローダーに渡されます。
```
dotnet script ./tools/download_voicevox_core.csx -- --output ./hoo/bar --device directml
```

## 開発環境
Windows 10  
Microsoft Visual Studio Community 2022  
C#  
.NET Standard 2.0
