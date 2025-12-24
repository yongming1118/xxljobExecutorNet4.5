using JobClient.impl;
using JobClient.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JobClient.executor
{
    public class ServerDispose
    {
        public static int Start(int port)
        {
            HttpServer httpServer = new HttpServer(1);
            var newPort = httpServer.Start(port);
            httpServer.ProcessRequest += HttpServer_ProcessRequest;

            return newPort;
        }

        private static void HttpServer_ProcessRequest(HttpListenerContext obj)
        {
            var request = obj.Request;

            string text = "";
            // convert stream to string
            using (StreamReader reader = new StreamReader(request.InputStream))
            {
                text = reader.ReadToEnd();
            }


            ExecutorBizImpl executorBizImpl = new ExecutorBizImpl();

            object invokeResult = null; 

            switch (request.RawUrl)
            {
                case "/run":
                    {

                        var triggerParam = Newtonsoft.Json.JsonConvert.DeserializeObject<TriggerParam>(text);
                        invokeResult = executorBizImpl.run(triggerParam);
                        break;
                    }
                case "/kill":
                    {
                        var triggerParam = Newtonsoft.Json.JsonConvert.DeserializeObject<TriggerParam>(text);
                        invokeResult = executorBizImpl.kill(triggerParam.jobId);

                        break;
                    }
                case "/log":
                    {
                        var LogParam = Newtonsoft.Json.JsonConvert.DeserializeObject<LogParam>(text);
                        invokeResult = executorBizImpl.log(LogParam.logDateTim, LogParam.logId, LogParam.fromLineNum);

                        break;
                    }
                case "/idleBeat":
                    {
                        var triggerParam = Newtonsoft.Json.JsonConvert.DeserializeObject<TriggerParam>(text);
                        invokeResult = executorBizImpl.idleBeat(triggerParam.jobId);
                        break;
                    }

                case "/beat":
                    {
                        invokeResult = executorBizImpl.beat();
                        break;
                    }
                default:
                    throw new NotImplementedException();
                    break;
            }

            ResponeHandler.ResponseStr(obj.Response, Newtonsoft.Json.JsonConvert.SerializeObject(invokeResult), "application/json");
        }
    }
}
