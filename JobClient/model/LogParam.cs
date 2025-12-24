using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobClient.model
{
    [Serializable]
    public class LogParam
    {

        public long logDateTim { get; set; }
        public int logId { get; set; }
        public int fromLineNum { get; set; }
        public LogParam()
        {

        }
        public LogParam(int fromLineNum, int logId, int logDateTim)
        {
            this.logDateTim = logDateTim;
            this.logId = logId;
            this.fromLineNum = fromLineNum;
        }


    }

}
