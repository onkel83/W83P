using System;
using W83P.AppSettings.Model;
using W83P.Basic;
using W83P.EveOnline.Model;
using W83P.EveOnline.ViewModel;
Console.WriteLine("TestConsole : ");

MAppSettings.LoadSettingsBin("settings.bin");
//MAppSettings.SetSetting("EOServerStatusBin", AppDomain.CurrentDomain.BaseDirectory + "EOServerStatus.bin");

//MAppSettings.SaveSettingsBin("settings.bin");

HttpHelper hh = new HttpHelper();
string result = await hh.SendGetRequestAsync(MAppSettings.GetSetting("ServerStatusUrl"));
VMServerStatus vM = new VMServerStatus();
vM.LoadFromFile(MAppSettings.GetSetting("EOServerStatusBin"));
await vM.AddJsonObjectToListAsync(result);
foreach(MServerStatus mss in vM.Items){
    Console.WriteLine($"Spieler        : {mss.Players}");
    Console.WriteLine($"Server Version : {mss.Server_Version.ToString()}");
    Console.WriteLine($"Server Start   : {mss.Start_Time.ToUniversalTime()}");
}
vM.SaveToFile(MAppSettings.GetSetting("EOServerStatusBin"));