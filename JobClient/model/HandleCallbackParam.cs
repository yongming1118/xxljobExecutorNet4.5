using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobClient.model
{
    public class HandleCallbackParam
    {


        public int logId { get; set; }

        public long logDateTim { get; set; }

        public ReturnT<String> executeResult { get; set; }

        public HandleCallbackParam() { }
        public HandleCallbackParam(int logId, long logDateTime, ReturnT<String> executeResult)
        {
            this.logId = logId;
            this.logDateTim = logDateTime;
            this.executeResult = executeResult;
        }



        public override string ToString()
        {
            return "HandleCallbackParam{" + logId +
                    ", logDateTim=" + logDateTim +
                    ", executeResult=" + executeResult +
                    '}';
        }
    }
}
