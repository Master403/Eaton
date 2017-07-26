using System.IO;
using System.Net;
using System.Text;

namespace Eaton.Homework.Business
{
    /// <summary>A helper class for HTTP requests</summary>
    public class HttpUtility
    {
        /// <summary>Creates a HTTP POST request</summary>
        /// <param name="url">A URL to be requested</param>
        /// <param name="jsonData">Data to sent</param>
        /// <returns>TRUE if the response status is 200 or 202; otherwise FALSE</returns>
        public bool PostRequest(string url, string jsonData)
        {
            HttpWebRequest request = (HttpWebRequest)
            WebRequest.Create(url);
            request.KeepAlive = false;
            request.Method = "POST";

            byte[] postBytes = Encoding.UTF8.GetBytes(jsonData);
            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "application/json";
            request.ContentLength = postBytes.Length;
            
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            return (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted);
        }
    }
}
