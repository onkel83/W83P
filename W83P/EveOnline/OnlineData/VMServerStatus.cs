using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using W83P.Basic;

namespace W83P.EveOnline.OnlineData
{
    public class VMServerStatus : SerialisationHelper<Model.MServerStatus>
    {
        public VMServerStatus(){
            Value = new Model.MServerStatus();
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
    }
}