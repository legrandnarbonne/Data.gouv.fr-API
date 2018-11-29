using APIODataGouv.Classes;
using APIODataGouv.Classes.APIObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace APIODataGouv
{
    public class APIRequest
    {

        Uri _requestedAPIURL;
        Uri _APIUrl;


        #region Properties
        [Description("Current API object ID")]
        public string Id { get; set; }

        [Description("Current API object parent ID. Parent Id is needed for request on some object type. Take a look at IsParentIdRequired method.")]
        public string ParentId { get; set; }

        [Description("Requested Object name. Use only to search by name")]
        public string ObjectName { get; set; }

        [Description("API server URI")]
        public Uri APIURL
        {
            get
            {
                return _APIUrl;
            }
            set
            {
                if (!value.ToString().EndsWith("/", StringComparison.InvariantCultureIgnoreCase)) value = new Uri($"{value}/");
                _APIUrl = value;
            }
        }

        [Description("API key for request indentification")]
        public string APIKey { get; set; }

        [Description("Read only property representing last query uri")]
        public Uri RequestedAPIURL
        {
            get
            {
                return _requestedAPIURL;
            }
        }

        [Description("Define page length of response list"), DefaultValue(20)]
        public int PageSize { get; set; }

        [Description("Define requested page"), DefaultValue(1)]
        public int RequestedPage { get; set; }

        [Description("Store last response."), DefaultValue(1)]
        public APIResponse LastResponse { get; internal set; }

        [Description("Store last response list total item.")]
        public int LastResponseTotalItem { get; internal set; }
        #endregion

        #region constructors
        /// <summary>
        /// APIRequest constructor
        /// </summary>
        /// <param name="url">api url for exemple https://www.data.gouv.fr/api/1/ </param>
        public APIRequest(string url)
        {
            APIURL = new Uri(url);
            JsonHelper.SetDefaultValues(this);
        }

        /// <summary>
        /// APIRequest constructor
        /// </summary>
        /// <param name="url">api url for exemple https://www.data.gouv.fr/api/1/ </param>
        /// <param name="key">api key </param>
        public APIRequest(string url, string key)
        {
            APIURL = new Uri(url);
            APIKey = key;
            JsonHelper.SetDefaultValues(this);
        }

        #endregion

        #region method    
        /// <summary>
        /// Get item
        /// </summary>
        /// <typeparam name="T">Type of requested item : Organisation,DataSet</typeparam>
        /// <returns></returns>
        public T GetItem<T>() where T : APIObject, new()
        {
            if (string.IsNullOrEmpty(Id) && string.IsNullOrEmpty(ObjectName)) throw new Exception("L'Id ou ObjectName doit être reseignée pour cette requête.");

            var result = new T();

            _requestedAPIURL = new Uri(apiURIBuilder(typeof(T), false));

            return Request<T>();
        }

        /// <summary>
        /// Get List of items
        /// </summary>
        /// <typeparam name="T">Type of requested item : Organisation,DataSet</typeparam>
        /// <returns></returns>
        public ResponseList<T> GetList<T>()
        {
            var result = new ResponseList<T>();

            _requestedAPIURL = new Uri(apiURIBuilder(typeof(T), true))
               .AddQuery("page_size", PageSize.ToString())
               .AddQuery("page", RequestedPage.ToString());

            return Request<ResponseList<T>>();
        }
        /// <summary>
        /// Build url using request properties 
        /// </summary>
        /// <param name="objType">requested object</param>
        /// <param name="listType">set true if requesting list, or false for a single element</param>
        /// <returns></returns>
        private string apiURIBuilder(Type objType, bool listType)
        {
            var uriPart = $"{APIURL}";

            var typeProperty = APIObject.APIObjectPropertiesDictionary[objType];

            if (!string.IsNullOrEmpty(ParentId))
            {
                var parent = typeProperty.ParentName;
                uriPart += $"{parent}/{ParentId}/";
            }
            else
                if (typeProperty.ParentIdNeeded)
                throw new Exception("La propriété IDParent doit être renseignée pour cette requête!");

            uriPart += $"{getCollection(objType)}/";

            if (!listType)
            {
                var request = string.IsNullOrEmpty(Id) ? ObjectName : Id;
                if (!string.IsNullOrEmpty(request)) uriPart += $"{request}/";
            }

            return uriPart;
        }

        private T Request<T>() where T : new()
        {
            LastResponse = new APIResponse();
            var result = new T();
            HttpWebResponse response;
            WebRequest request = WebRequest.Create(_requestedAPIURL);

            request.Headers.Add("X-API-KEY", APIKey);
            request.Credentials = CredentialCache.DefaultCredentials;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException we)
            {
                response = (HttpWebResponse)we.Response;
            }

            LastResponse.Status = response.StatusCode;

            var encoding = Encoding.ASCII;
            using (var reader = new StreamReader(response.GetResponseStream(), encoding))
            {
                LastResponse.Content = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<T>(LastResponse.Content, new JsonSerializerSettings { ContractResolver = new JsonDeserializePropertiesResolver() });
            }

            LastResponseTotalItem = 1;

            if (result is IAPIListInformation)
                LastResponseTotalItem = ((IAPIListInformation)result).TotalPage;

            return result;
        }
        /// <summary>
        /// Update Ressource file
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string UploadFile(string source)
        {
            LastResponse = new APIResponse();
            LastResponseTotalItem = 1;

            Uri destinationUrl = new Uri($"{apiURIBuilder(typeof(Resource), false)}/upload/");
            
            WebClient myWebClient = new WebClient();
            myWebClient.Headers.Add("X-API-KEY", APIKey);
            
            try
            {
                byte[] responseArray = myWebClient.UploadFile(destinationUrl, source);
                LastResponse.Status = HttpStatusCode.OK;
                return Encoding.ASCII.GetString(responseArray);
            }
            catch (WebException e)//thanks https://stackoverflow.com/questions/3614034/system-net-webexception-http-status-code
            {
                LastResponse.Status = HttpStatusCode.InternalServerError;
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = e.Response as HttpWebResponse;
                    if (response != null)
                        LastResponse.Status = response.StatusCode;
                }
            }
            catch (Exception e)
            {
                LastResponse.Status = HttpStatusCode.InternalServerError;
            }

            return null;
        }

        public string UploadFile(MemoryStream source,string filename)
        {
            LastResponse = new APIResponse();
            LastResponseTotalItem = 1;

            Uri destinationUrl = new Uri($"{apiURIBuilder(typeof(Resource), false)}/upload/");

            WebClient myWebClient = new WebClient();
            myWebClient.Headers.Add("X-API-KEY", APIKey);
            
            source.Position = 0;

            var sourceBytes = source.ToArray();

            try
            {

                string boundary = "------------------------" + DateTime.Now.Ticks.ToString("x");
                myWebClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);
                var fileData = myWebClient.Encoding.GetString(sourceBytes);
                var package = $"--{boundary}\r\nContent-Disposition: form-data; name=\"file\"; filename=\"{filename}\"\r\nContent-Type: {"multipart/form-data"}\r\n\r\n{fileData}\r\n--{boundary}--\r\n";

                var nfile = myWebClient.Encoding.GetBytes(package);

                byte[] responseArray = myWebClient.UploadData(destinationUrl, "POST", nfile);

                LastResponse.Status = HttpStatusCode.OK;
                return Encoding.ASCII.GetString(responseArray);
            }
            catch (WebException e)//thanks https://stackoverflow.com/questions/3614034/system-net-webexception-http-status-code
            {
                LastResponse.Status = HttpStatusCode.InternalServerError;
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = e.Response as HttpWebResponse;
                    if (response != null)
                        LastResponse.Status = response.StatusCode;
                }
            }
            catch (Exception e)
            {
                LastResponse.Status = HttpStatusCode.InternalServerError;
            }

            return null;
        }

        private APIResponse SetObject(Type objType, string DATA, string method)
        {
            LastResponse = new APIResponse();
            _requestedAPIURL = new Uri(apiURIBuilder(objType, false));

            HttpWebResponse webResponse = null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_requestedAPIURL);
            request.Headers.Add("X-API-KEY", APIKey);
            request.Method = method;
            request.ContentType = "application/json";

            if (DATA != null)
            {
                var utf8Array = Encoding.UTF8.GetBytes(DATA);
                request.ContentLength = utf8Array.Length;
                request.GetRequestStream().Write(utf8Array, 0, utf8Array.Length);
            }

            try
            {
                webResponse = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException we)
            {
                webResponse = (HttpWebResponse)we.Response;
            }

            LastResponse.Status = webResponse.StatusCode;
            LastResponseTotalItem = 1;

            using (Stream webStream = webResponse.GetResponseStream())
            {
                if (webStream != null)
                {
                    using (StreamReader responseReader = new StreamReader(webStream))
                    {
                        LastResponse.Content = responseReader.ReadToEnd();
                        return LastResponse;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Delete Object
        /// </summary>
        /// <param name="obj">Object to delete</param>
        /// <returns></returns>
        public APIResponse Delete(APIObject obj)
        {
            Id = obj.id;
            LastResponseTotalItem = 1;

            return SetObject(obj.GetType(), null, "DELETE");
        }

        /// <summary>
        /// Add object
        /// </summary>
        /// <param name="obj">Object to add</param>
        /// <param name="parent">Parent object if needed</param>
        /// <returns></returns>
        public APIResponse AddObject(IAPIObject obj, APIObject parent)
        {
            obj.SetParentId(parent.id);

            string json = obj.Serialize();

            if (obj.IsParentIdRequired()) ParentId = parent.id;

            LastResponseTotalItem = 1;

            return SetObject(obj.GetType(), json, "POST");

        }

        /// <summary>
        /// Update object
        /// </summary>
        /// <param name="obj">modified object</param>
        /// <param name="parent">Parent object if needed</param>
        /// <returns></returns>
        public APIResponse UpdateObject(IAPIObject obj, APIObject parent = null)
        {
            var result = new APIResponse();
            if (obj == null) return result;

            LastResponseTotalItem = 1;

            string json = obj.Serialize();

            Id = ((APIObject)obj).id;

            if (obj.IsParentIdRequired()) ParentId = parent.id;

            return SetObject(obj.GetType(), json, "PUT");

        }
        private string getCollection(Type objectType)
        {
            var objectName = objectType.Name.ToLower();
            return objectName + "s";
        }

        #endregion

        /// <summary>
        /// API response List
        /// </summary>
        /// <typeparam name="T">Requested object type</typeparam>
        public class ResponseList<T> : IAPIListInformation
        {
            [JsonProperty(PropertyName = "data")]
            public virtual IList<T> Items { get; set; }

            [JsonProperty(PropertyName = "page_size")]
            public int PageSize { get; set; }
            public int Page { get; set; }
            /// <summary>
            /// Next page URI null if there is no page
            /// </summary>
            [JsonProperty(PropertyName = "next_page")]
            public Uri NextPage { get; set; }
            /// <summary>
            /// Next page URI null if there is no page
            /// </summary>
            [JsonProperty(PropertyName = "previous_page")]
            public Uri PreviousPage { get; set; }

            [JsonProperty(PropertyName = "total")]
            public int TotalPage { get; set; }
            public HttpStatusCode Statut { get; internal set; }

            public class ReponseItem
            {
                public string Item { get; set; }
            }
        }
    }

    /// <summary>
    /// API Response
    /// </summary>
    public class APIResponse
    {
        public HttpStatusCode Status { get; set; }
        public string Content { get; set; }
    }
}
