namespace MonetaWMSMobile.SystemClasses
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using WebTrainingMVC.Models;

    public class WebRequestManager
    {
        public static BaseResultModel HttpPost(string url, object obj)
        {
            var result = new BaseResultModel();
            try
            {
               var serializer = new JsonSerializer();
                string json = JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
                byte[] bytes = Encoding.UTF8.GetBytes(json);
                

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.ContentLength = bytes.Length;
                httpWebRequest.Method = "POST";
                httpWebRequest.Timeout = 20 * 1000;
                using (var streamWriter = httpWebRequest.GetRequestStream())
                {
                    streamWriter.Write(bytes, 0, bytes.Length);
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    result.Msg = responseText;
                    result.Code = 1;
                    return result;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException;
                result.Code = -1;
                result.Msg = "Грешка с връзката към сървъра.";
                return result;
            }
        }
        public static BaseResultModel FromJsonGet(string url)
        {
            var result = new BaseResultModel();
            try
            {
                var serializer = new JsonSerializer();

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.Timeout = 20*1000;
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                    result.Msg = responseText;
                    result.Code = 1;
                    return result;
                }
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException;
                result.Code = -1;
                result.Msg = "Грешка с връзката към сървъра.";
                return result;
            }
        }
    }
}
