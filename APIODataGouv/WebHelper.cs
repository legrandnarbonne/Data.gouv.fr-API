using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace APIODataGouv
{
    class WebHelper
    {
        private static readonly Encoding encoding = Encoding.UTF8;

        public static HttpWebResponse MultipartFormDataPost(Dictionary<string, object> postParameters, string postUrl, string userAgent, string certificate)
        {
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; boundary=" + formDataBoundary;

            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

            return PostForm(postUrl, userAgent, contentType, formData, certificate);
        }

        private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            Stream formDataStream = new System.IO.MemoryStream();
            bool needsCLRF = false;

            foreach (var param in postParameters)
            {
                // Thanks to feedback from commenters, add a CRLF to allow multiple parameters to be added.
                // Skip it on the first parameter, add it to subsequent parameters.
                if (needsCLRF)
                    formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));

                needsCLRF = true;

                //if (param.Value is FileParameter)
                //{
                //    FileParameter fileToUpload = (FileParameter)param.Value;

                //    // Add just the first part of this param, since we will write the file data directly to the Stream
                //    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\";\r\nContent-Type: {3}\r\n\r\n",
                //        boundary,
                //        param.Key,
                //        fileToUpload.FileName ?? param.Key,
                //        fileToUpload.ContentType ?? "application/octet-stream");

                //    formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));

                //    // Write the file data directly to the Stream, rather than serializing it to a string.
                //    formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                //}
                //else
                //{
                    ParamDetails d = (ParamDetails)param.Value;
                    string postData = "";

                    if (d.ContentType == null)
                        postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                            boundary,
                            param.Key,
                            d.Value);
                    else
                        postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\nContent-Type: {3}\r\n\r\n{2}",
                        boundary,
                        param.Key,
                        d.Value,
                        d.ContentType);

                    formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
                //}
            }

            // Add the end of the request.  Start with a newline
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }


        private static HttpWebResponse PostForm(string postUrl, string userAgent, string contentType, byte[] formData, string certificate)
        {
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;

            if (request == null)
            {
                throw new NullReferenceException("request is not a http request");
            }

            // Set up the request properties.
            request.Method = "POST";
            request.ContentType = contentType;
            request.UserAgent = userAgent;
            request.CookieContainer = new CookieContainer();
            request.ContentLength = formData.Length;
            if (!string.IsNullOrEmpty(certificate))
            {
                X509Certificate x509certificate = X509Certificate.CreateFromCertFile(certificate);
                request.ClientCertificates.Add(x509certificate);

                ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                ServicePointManager.MaxServicePointIdleTime = 100000;
            }
            // Send the form data to the request.
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
            }

            return request.GetResponse() as HttpWebResponse;
        }

        public class ParamDetails
        {
            public string ContentType;
            public string Value;
            public ParamDetails(string value, string contentType)
            {
                Value = value;
                ContentType = contentType;
            }
        }

        public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy
        {
            public TrustAllCertificatePolicy()
            {
            }

            public bool CheckValidationResult(ServicePoint sp,
               X509Certificate cert,
               WebRequest req,
               int problem)
            {
                return true;
            }
        }
    }
    public static class HttpExtensions
    {
        public static Uri AddQuery(this Uri uri, string name, string value)
        {
            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);

            httpValueCollection.Remove(name);
            httpValueCollection.Add(name, value);

            var ub = new UriBuilder(uri);
            ub.Query = httpValueCollection.ToString();

            return ub.Uri;
        }
    }
}
