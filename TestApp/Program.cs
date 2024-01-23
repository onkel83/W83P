// See https://aka.ms/new-console-template for more information
using System.Windows.Markup;
using Newtonsoft.Json.Linq;
using W83P.Basic;
using W83P.EveOnline;
using W83P.EveOnline.OnlineData;

Console.WriteLine("Hello, nice to meet you");

VMServerStatus _vMSS = new VMServerStatus();
_vMSS.Load();
Console.WriteLine($"Es sind Momentan : {_vMSS.Values.Count()} Einträge gespeichert !"); 
_vMSS.IterateThroughJToken(await _vMSS.GetJsonFromUrlAsync(Konstanten.GetValueByKey("Status")));
_vMSS.AddToValues();
_vMSS.Save();
_vMSS.Values.Clear();
Console.WriteLine($"Es sind Momentan : {_vMSS.Values.Count()} Einträge gespeichert !"); 
_vMSS.Load();
Console.WriteLine($"Es sind Momentan : {_vMSS.Values.Count()} Einträge gespeichert !"); 
foreach(W83P.EveOnline.Model.MServerStatus _mss in _vMSS.Values){
    Console.WriteLine($"ServerStatus ID  : {_mss.ID}");
    Console.WriteLine($"Server Version   : {_mss.ServerVersion}");
    Console.WriteLine($"Server Startzeit : {_mss.StartZeit}");
    Console.WriteLine($"Server Spieler   : {_mss.SpielerZahl}");
    Console.WriteLine("####################################################################");
}

/*  SerialisationHelper httpClientHelper = new SerialisationHelper<W83P.Modelle.Basic>();
    string url = "https://esi.evetech.net/latest/status/?datasource=tranquility"; // Ersetzen Sie dies durch Ihre eigene URL

    try
    {
        JToken json = await httpClientHelper.GetJsonFromUrlAsync(url);
        if(httpClientHelper.GetJsonArray(json)){
            Console.WriteLine("WoW es Funktioniert und sagt mir hier ist ein JArray");
        }else if(httpClientHelper.GetJsonObject(json)){
            Console.WriteLine("WoW es Funktioniert und sagt mir hier ist ein JObject");
        }

    }catch (ArgumentException ex){
        // Behandeln Sie ungültige URLs oder ungültiges JSON hier
        Console.WriteLine(ex.Message);
    }catch (HttpRequestException ex){
        // Behandeln Sie HTTP-Fehler hier
        Console.WriteLine(ex.Message);
    }
*/



//W83P.EveOnline.XML.MarktPreisXml region = new W83P.EveOnline.XML.MarktPreisXml();
//region.JsonToObj("MPXml");
//var jsonArray = await region.GetNameOfTypAsync(10000001);
//Console.WriteLine(jsonArray.Value<string>("name"));
//Console.WriteLine(region.Values.Count + " Einträge erzeugt!");

/*
W83P.Logging.ErrorLogger.Initialize("test.xml");
W83P.Logging.ErrorModel em = new W83P.Logging.ErrorModel { Level = W83P.Logging.LogLevel.Info, Message = "Test Eintrag" };
W83P.Logging.ErrorLogger.Log(em);
*/

//marketXML(ParseJsonArrayFromUrl("https://evetycoon.com/api/v1/market/groups"));
//regionXML(ParseJsonArrayFromUrl("https://evetycoon.com/api/v1/market/regions"));
//jumpsXML();
//MarketTypXml(ParseJsonArrayFromUrl("https://esi.evetech.net/latest/markets/prices/?datasource=tranquility"));

/*
    Console.WriteLine("Es gibt " + DeSerializeJumps().Count + " Jump Einträge");
    Console.WriteLine("Es gibt " + DeSerializeMarketGroups().Count + " MarketGroup Einträge");
    Console.WriteLine("Es gibt " + DeSerializeRegions().Count + " Regions Einträge");
    Console.WriteLine("Es gibt " + DeSerializeMarketTyp().Count + " Markt Einträge");
*/
/*
static JArray ParseJsonArrayFromUrl(string url)
{
    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
    StreamReader reader = new StreamReader(response.GetResponseStream());
    string json = reader.ReadToEnd();
    JArray jsonArray = JArray.Parse(json);
    return jsonArray;
}
static void regionXML(JArray jsonArray)
{
    List<Regionen> regions = new List<Regionen>();
    foreach (JObject obj in jsonArray)
    {
        Regionen regionen = new Regionen();
        regionen.Name = obj.GetValue("name").ToString();
        regionen.RegionID = Convert.ToInt32(obj.GetValue("id"))+"";
        regions.Add(regionen);
    }
    XmlSerializer serializer = new XmlSerializer(typeof(List<Regionen>));
    using (TextWriter writer = new StreamWriter("Regions.xml"))
    {
        serializer.Serialize(writer, regions);
    }
}
static void MarketTypXml(JArray jsonArray)
{
    List<MarketTyp> _Typs = new List<MarketTyp>();
    foreach(JObject obj in jsonArray)
    {
        MarketTyp tp = new MarketTyp();
        tp.ID = Convert.ToInt32(obj.GetValue("type_id"))+"";
        tp.Price = Convert.ToDouble(obj.GetValue("average_price"))+"";
        _Typs.Add(tp);
    }
    XmlSerializer serializer = new XmlSerializer(typeof(List<MarketTyp>));
    using (TextWriter writer = new StreamWriter("MarketTyp.xml"))
    {
        serializer.Serialize(writer, _Typs);
    }
    Console.WriteLine("Es wurden " + _Typs.Count + " Markt Einträge eingefügt!");
}
static void marketXML(JArray jsonArray)
{
    List<MarketGroup> groups = new List<MarketGroup>();
    foreach (JObject obj in jsonArray)
    {
        MarketGroup group = new MarketGroup();
        group.MarketGroupID = Convert.ToString(obj.GetValue("marketGroupID"));
        group.Name = Convert.ToString(obj.GetValue("marketGroupName"));
        if (
            obj.GetValue("parentGroupID") != null && 
            (obj.GetValue("parentGroupID").ToString() != "0" && 
             obj.GetValue("parentGroupID").ToString().Length > 1)
           ){
            group.ParentGroupID = Convert.ToString(obj.GetValue("parentGroupID"));
        } else {
            group.ParentGroupID = "0";
        }
        groups.Add(group);
    }
    XmlSerializer serializer = new XmlSerializer(typeof(List<MarketGroup>));
    using (TextWriter writer = new StreamWriter("MarketGroup.xml"))
    {
        serializer.Serialize(writer, groups);
    }
}

static void jumpsXML(JArray jsonArray = null)
{
    List<Jumps> jumps = new List<Jumps>();
    Jumps jump;
    int i = 0;
    string line;
    using (StreamReader reader = new StreamReader("D://Jumps.txt"))
    {
        while ((line = reader.ReadLine()) != null)
        {
            if (i != 0)
            {
                jump = new Jumps();
                string[] _tmp;
                _tmp = line.Split(new char[] { ',' });
                jump.From = _tmp[0];
                jump.To = _tmp[1];
                jumps.Add(jump);
            }
            i++;
        }
    }
    XmlSerializer serializer = new XmlSerializer(typeof(List<Jumps>));
    using (TextWriter writer = new StreamWriter("Jumps.xml"))
    {
        serializer.Serialize(writer, jumps);
    }
    Console.WriteLine("Es sind " + i + " Einträge verarbeitet worden");
}

static List<Jumps> DeSerializeJumps()
{
    List<Jumps> _Jumps;
    XmlSerializer serializer = new XmlSerializer(typeof(List<Jumps>));
    using (TextReader reader = new StreamReader("Jumps.xml"))
    {
        _Jumps = (List<Jumps>)serializer.Deserialize(reader);
    }
    return _Jumps;
}
static List<MarketGroup> DeSerializeMarketGroups()
{
    List<MarketGroup> _MarketGroups;
    XmlSerializer serializer = new XmlSerializer(typeof(List<MarketGroup>));
    using (TextReader reader = new StreamReader("MarketGroup.xml"))
    {
        _MarketGroups = (List<MarketGroup>)serializer.Deserialize(reader);
    }
    return _MarketGroups;
}
static List<Regionen> DeSerializeRegions()
{
    List<Regionen> _Regions;
    XmlSerializer serializer = new XmlSerializer(typeof(List<Regionen>));
    using (TextReader reader = new StreamReader("Regions.xml"))
    {
        _Regions = (List<Regionen>)serializer.Deserialize(reader);
    }
    return _Regions;
}
static List<MarketTyp> DeSerializeMarketTyp()
{
    List<MarketTyp> marketTyps;
    XmlSerializer serializer = new XmlSerializer(typeof(List<MarketTyp>));
    using (TextReader reader = new StreamReader("MarketTyp.xml"))
    {
        marketTyps = (List<MarketTyp>)serializer.Deserialize(reader);
    }
    return marketTyps;
}
*/