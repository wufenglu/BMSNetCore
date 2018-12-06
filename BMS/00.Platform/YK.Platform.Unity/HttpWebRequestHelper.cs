using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace YK.Platform.Unity
{
    public class HttpWebRequestHelper
    {
        /// <summary>
        /// get 数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string Get(string url)
        {
            string responseData = null;

            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "GET";
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.Timeout = 20000;

            StreamReader responseReader = null;
            try
            {
                responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch
            {
            }
            finally
            {
                webRequest.GetResponse().GetResponseStream().Close();
                responseReader.Close();
                responseReader = null;
                webRequest = null;
            }
            return responseData;
        }

        /// <summary>
        /// post 数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Post(string url, string data)
        {
            byte[] postBytes = Encoding.GetEncoding("utf-8").GetBytes(data);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = postBytes.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(postBytes, 0, postBytes.Length);
            }
            string bakData = "";
            using (WebResponse wr = req.GetResponse())
            {
                //在这里对接收到的页面内容进行处理   
                StreamReader sr = new StreamReader(wr.GetResponseStream());
                bakData = sr.ReadToEnd().Trim();
            }
            return bakData;
        }
    }
}
