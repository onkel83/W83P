using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W83P.Logging
{
    public enum LogLevel
    {
        Info,
        Warnung,
        Error
    }

    public class ErrorModel
    {
        public LogLevel Level { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }

        public ErrorModel(){
            Message = "";
            TimeStamp = DateTime.Now;
            Level = LogLevel.Info;
        }
    }
}
