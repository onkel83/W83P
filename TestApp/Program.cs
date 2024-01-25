using System;
using System.Timers;
using W83P.AppSettings.Model;
using W83P.Basic;
using W83P.EveOnline.Model;
using W83P.EveOnline.ViewModel;
Console.WriteLine("TestConsole : ");

MAppSettings.LoadSettingsBin("settings.bin");
//MAppSettings.SetSetting("EOMarktPriceBin", AppDomain.CurrentDomain.BaseDirectory + "EOMarktPrice.bin");
//MAppSettings.SetSetting("MarktPriceUrl", "https://esi.evetech.net/latest/markets/prices/?datasource=tranquility");

//MAppSettings.SaveSettingsBin("settings.bin");
/*
    System.Timers.Timer timer = new System.Timers.Timer(5 * 60 * 1000);

        // Define the function that will be called when the timer triggers
    timer.Elapsed += new ElapsedEventHandler(GetServerStatus);

        // Start the timer
    timer.Start();
    DoTheMagic();
        // Wait for the user to press a key
    while (Console.In.ReadLine().Length < 0) { 
    
    }
*/
HttpHelper hh = new HttpHelper();
    string result = await hh.SendGetRequestAsync(MAppSettings.GetSetting("MarktPriceUrl"));
    VMServerStatus vM = new VMServerStatus();
    //vM.LoadFromFile(MAppSettings.GetSetting("EOServerStatusBin"));
    await vM.AddJsonObjectToListAsync(result);
    Console.WriteLine($"Einträge : {vM.Items.Count} zu letzt geupdatet um {DateTime.Now}");
    vM.SaveToFile(MAppSettings.GetSetting("EOMarktPriceBin"));

static async void GetServerStatus(object source, ElapsedEventArgs e){
    DoTheMagic();
}

static async void DoTheMagic(){
    HttpHelper hh = new HttpHelper();
    string result = await hh.SendGetRequestAsync(MAppSettings.GetSetting("ServerStatusUrl"));
    VMServerStatus vM = new VMServerStatus();
    vM.LoadFromFile(MAppSettings.GetSetting("EOServerStatusBin"));
    await vM.AddJsonObjectToListAsync(result);
    Console.WriteLine($"Einträge : {vM.Items.Count} zu letzt geupdatet um {DateTime.Now}");
    vM.SaveToFile(MAppSettings.GetSetting("EOServerStatusBin"));
}