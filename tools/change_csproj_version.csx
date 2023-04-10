#r "System.Xml.XDocument"

using System.Linq;
using System.Xml.Linq;
using System.IO;

public void main()
{
    if (Args.Count != 2)
    {
        Console.WriteLine("Invalid argument.");
        Console.WriteLine("[0] file name");
        Console.WriteLine("[1] version");
        return;
    }
    //ファイル名
    string fileName = Args[0];
    //バージョン名
    string versionName = Args[1];

    if (!File.Exists(fileName))
    {
        Console.WriteLine("File is not exists.");
        return;
    }

    XElement xml = XElement.Load(fileName);
    XElement versionElement = xml.Element("PropertyGroup").Element("Version");
    Console.WriteLine($"old version = {versionElement.Value}");

    versionElement.Value = versionName;
    Console.WriteLine($"new version = {versionElement.Value}");
    xml.Save(fileName);
}

main();