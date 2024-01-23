using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using W83P.Basic;
using W83P.EveOnline.Model;

namespace W83P.EveOnline.OnlineData
{
    public class VMServerStatus : SerialisationHelper<Model.MServerStatus>
    {
        public VMServerStatus(){
            Value = new Model.MServerStatus();
            Values = new List<Model.MServerStatus>();
            FileName =  Konstanten.GetValueByKey("AppPath")+Konstanten.GetValueByKey("SysBin");
        }
        public override void InsertIntoClass(string propertyName, string propertyValue)
        {
            switch(propertyName){
                case "players" : Value.SpielerZahl = propertyValue; break;
                case "server_version" : Value.ServerVersion = propertyValue; break;
                case "start_time" : Value.StartZeit = propertyValue; break;
                default : break;
            }
        }
        public override void Save(){
            FileHelper.SaveObject<List<Model.MServerStatus>>(Values, FileName);
        }
        public override void Load(){
            Values.Clear();
            Values = new();
            foreach(Model.MServerStatus mss in FileHelper.LoadObject<List<Model.MServerStatus>>(FileName)){
                Value = new MServerStatus{ 
                    ID = mss.ID,
                    ServerVersion = mss.ServerVersion,
                    StartZeit = mss.StartZeit,
                    SpielerZahl = mss.SpielerZahl
                };
                Values.Add(Value);
            }
        }

        public void AddToValues(){
            Value.ID = (Values.Count() + 1).ToString();
            Values.Add(Value);
            Value = new();
        }

        public override void AddToXML()
        {
            throw new NotImplementedException();
        }

        public override void LoadFromXML()
        {
            throw new NotImplementedException();
        }
    }
}