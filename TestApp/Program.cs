using System;
using W83P.AppSettings.Model;
Console.WriteLine("TestConsole");
/*
MAppSettings.SetSetting("ErrorXml", AppDomain.CurrentDomain.BaseDirectory + "error.xml");
MAppSettings.SetSetting("ErrorBin", AppDomain.CurrentDomain.BaseDirectory + "error.bin");

MAppSettings.SaveSettingsBin("settings.bin");
*/
MAppSettings.LoadSettingsBin("settings.bin");
Console.WriteLine($"Key : ErrorXml ; Value : {MAppSettings.GetSetting("ErrorXml")}");
Console.WriteLine($"Key : ErrorBin ; Value : {MAppSettings.GetSetting("ErrorBin")}");