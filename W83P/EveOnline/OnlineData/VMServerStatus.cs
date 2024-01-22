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
            FileName =  Konstanten.GetValueByKey("AppPath")+Konstanten.GetValueByKey("Sys");
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
            Konstanten.SaveObject<List<Model.MServerStatus>>(Values, FileName);
        }
        public override void Load(){
            Values.Clear();
            foreach(MServerStatus mss in Konstanten.LoadObject<List<Model.MServerStatus>>(FileName)){
                Values.Add(mss);
            }
        }

        public void AddToValues(){
            Values.Add(Value);
            Value = new();
        }
    }
}