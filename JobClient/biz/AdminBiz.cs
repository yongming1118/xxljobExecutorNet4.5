using JobClient.model;
using JobClient.utils;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JobClient.biz
{
    public class AdminBiz
    {
        private static ILog logger = Log4netManager.GetLogger(typeof(AdminBiz));
        public static string MAPPING = "/api";
        private string address;
        private string accessToken;

        public AdminBiz(string address, string accessToken)
        {
            this.address = address;
            this.accessToken = accessToken;
        }


        public ReturnT<String> callback(List<HandleCallbackParam> callbackParamList)
        {
            var requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(callbackParamList);
            return PostPackage("callback", requestStr);
        }



        public ReturnT<String> registry(RegistryParam registryParam)
        {
            var requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(registryParam);
            return PostPackage("registry", requestStr);
        }




        public ReturnT<String> registryRemove(RegistryParam registryParam)
        {
            var requestStr = Newtonsoft.Json.JsonConvert.SerializeObject(registryParam);
            return PostPackage("registryRemove", requestStr);
        }



        public ReturnT<String> triggerJob(int jobId)
        {
            throw new NotImplementedException();
        }

        #region  Http Send Package functions 

        private ReturnT<String> PostPackage(string methodName, string requestStr)
        {
            var result = requestTo(address + MAPPING + "/" + methodName, requestStr, this.accessToken);

            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<ReturnT<String>>(result);

            if (response == null)
            {
                logger.Error(">>>>>>>>>>> xxl-rpc netty response not found.");
                throw new Exception(">>>>>>>>>>> xxl-rpc netty response not found.");
            }
            if (response.code!= 200)
            {
                throw new Exception(response.msg);
            }
            else
            {
                return response;
            }
        }

        private static string requestTo(string url, string requestStr, string strToken)
        {
            HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;

            httpWebRequest.Timeout = 30 * 1000;// timeout;
            httpWebRequest.Method = "POST";

            httpWebRequest.Headers.Add("XXL-JOB-ACCESS-TOKEN", strToken);
            httpWebRequest.ContentType = "application/json";

            Stream myResponseStream = null;
            StreamReader myStreamReader = null;
            Stream requestStream = null;

            try
            {
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                byte[] postData = encoding.GetBytes(requestStr);
                httpWebRequest.ContentLength = postData.Length;
                requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(postData, 0, postData.Length);

                HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
                myResponseStream = response.GetResponseStream();
                myStreamReader = new StreamReader(myResponseStream, System.Text.Encoding.GetEncoding("UTF-8"));
                string result = myStreamReader.ReadToEnd();
                return result;
            }
            finally
            {
                if (myStreamReader != null)
                {
                    myStreamReader.Close();
                }

                if (myResponseStream != null)
                {
                    myResponseStream.Close();
                }
                if (requestStream != null)
                {
                    requestStream.Close();
                }
            }
        }
        #endregion
    }
}
