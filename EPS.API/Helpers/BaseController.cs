using AutoMapper;
using EPS.Data.Entities;
using EPS.Data.SolrEntities;
using EPS.Service;
using EPS.Service.Helpers;
using EPS.Utils.Common;
using EPS.Utils.Repository.Audit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SolrNet;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace EPS.API.Helpers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        private Lazier<SolrServices<SolrLogs>> _solrLogServices;
        protected SolrServices<SolrLogs> _SolrLogServices
        {
            get
            {
                if (_solrLogServices == null) _solrLogServices = new Lazier<SolrServices<SolrLogs>>(HttpContext.RequestServices);

                return _solrLogServices.Value;
            }
        }
        private Lazier<EPSBaseService> _baseService;
        protected EPSBaseService BaseService
        {
            get
            {
                if (_baseService == null) _baseService = new Lazier<EPSBaseService>(HttpContext.RequestServices);

                return _baseService.Value;
            }
        }

        private Lazier<IUserIdentity<int>> _userIdentity;
        protected IUserIdentity<int> UserIdentity
        {
            get
            {
                if (_userIdentity == null) _userIdentity = new Lazier<IUserIdentity<int>>(HttpContext.RequestServices);

                return _userIdentity.Value;
            }
        }

        private Lazier<IMapper> _mapper;
        protected IMapper Mapper
        {
            get
            {
                if (_mapper == null) _mapper = new Lazier<IMapper>(HttpContext.RequestServices);

                return _mapper.Value;
            }
        }

        private Lazier<ILogger<BaseController>> _logger;
        protected ILogger<BaseController> Logger
        {
            get
            {
                if (_logger == null) _logger = new Lazier<ILogger<BaseController>>(HttpContext.RequestServices);

                return _logger.Value;
            }
        }
        //protected async Task AddLogAsync(string _noidung, DOITUONG _object, int _thaotac, int _status)
        //{
        //    await BaseService.CreateAsync<Log, LogCreateDto>(new LogCreateDto(UserIdentity.FullName, _noidung, _object, _thaotac, _status));
        //}
        
        protected async Task AddLogAsync(string Content, DOITUONG Object, int Action, int Status, object objectID = null)
        {
            //string CreatedBy = !string.IsNullOrEmpty(UserIdentity.FullName) ? UserIdentity.FullName + " (" + UserIdentity.Username + ")" : "Người dùng ẩn danh";
            //int CreatedByID = !string.IsNullOrEmpty(UserIdentity.FullName) ? UserIdentity.UserId : 0;
            //string RemoteIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            //await _SolrLogServices.AddItemAsync(new SolrLogs(Content, Object, RemoteIpAddress, Action, Status, CreatedBy, CreatedByID, objectID));
        }
        protected async Task<IActionResult> ReturnExport(objExportResult exportResult, string type, string fileName = "",string contentType= "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            if (!string.IsNullOrEmpty(fileName))
                fileName = $"DanhSach_{type}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
            if (!exportResult.error)
                return File(exportResult.data, contentType, fileName);
            else
                return BadRequest(exportResult.message);
        }

        #region notification mobile

        //1
        //protected void AddTokenMobiles(List<string> obj, object notification, object obj_ctdt)
        //{
        //    try
        //    {
        //        string applicationID = "AAAAAeYd9RI:APA91bEwXRBEjosLpzyfZMq2TgQ4IXfaVjqg7-QZ0DB2GaUupF_Bb4tNEif-A8_3d1aGfZKqvMxy-lg81PA4wdeyqjcw6MyiyRpJUWGyv0YOVQ6YZ9Rev9015gDU4mcrYIoXBMyyvTnQ";
        //        string senderId = "363060269669";
        //        //WebRequest tRequest = (HttpWebRequest)WebRequest.Create("https://iid.googleapis.com/iid/v1:batchAdd");
        //        //tRequest.Method = "post";
        //        //tRequest.ContentType = "application/json";
        //        var data = new
        //        {
        //            to = "/topics/notification",
        //            registration_tokens = obj
        //        };


        //        var notification1 = new
        //        {
        //            body = "The first message from the React Native and Firebase",
        //            title = "React Native Firebase",
        //            content_available = "true",
        //            priority = "high"
        //        };
        //        var data1 = new
        //        {
        //            body = "The first message from the React Native and Firebase",
        //            title = "React Native Firebase",
        //            content_available = true,
        //            priority = "high"
        //        };
        //        var data2 = new
        //        {
        //            to = "/topics/notification",
        //            notification = notification1,
        //            data = data1,
        //        };



        //        using (var content = new StringContent(JsonConvert.SerializeObject(data2), System.Text.Encoding.UTF8, "application/json"))
        //        {


        //            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
        //            HttpClient _httpClient = new HttpClient(handler);
        //            _httpClient.DefaultRequestHeaders.Accept.Clear();
        //            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            _httpClient.DefaultRequestHeaders.Add("Authorization", string.Format("key={0}",applicationID));
        //            // _httpClient.DefaultRequestHeaders.Add("Sender", senderId);
        //            //_httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("key=", applicationID);


        //            HttpResponseMessage result = _httpClient.PostAsync("https://fcm.googleapis.com/fcm/send", content).Result;
        //            if (result.StatusCode == System.Net.HttpStatusCode.Created)
        //            {
        //                int a = 0;
        //            }
        //            string returnValue = result.Content.ReadAsStringAsync().Result;
        //            throw new Exception($"Failed to POST data: ({result.StatusCode}): {returnValue}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = ex.Message;
        //    }
        //}
        ////2
        //protected void SendPushNotification(List<string> obj, object notification, object obj_ctdt)
        //{
        //    try
        //    {
        //        string applicationID = "AIzaSyAwyxZ_BKtY9rIFq5z0bh4K0MVNTvCJhHo";
        //        string senderId = "363060269669";
        //        //string deviceId = "/topics/" + topic;
        //        WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
        //        tRequest.Method = "post";
        //        tRequest.ContentType = "application/json";
        //        var data = new
        //        {
        //            to = "/topics/notification",
        //            notification = notification,
        //            data = obj_ctdt
        //        };

        //        var serializer = new JavaScriptSerializer();
        //        var json = serializer.Serialize(data);
        //        Byte[] byteArray = Encoding.UTF8.GetBytes(json);
        //        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
        //        tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
        //        tRequest.ContentLength = byteArray.Length;

        //        using (Stream dataStream = tRequest.GetRequestStream())
        //        {
        //            dataStream.Write(byteArray, 0, byteArray.Length);
        //            using (WebResponse tResponse = tRequest.GetResponse())
        //            {
        //                using (Stream dataStreamResponse = tResponse.GetResponseStream())
        //                {
        //                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
        //                    {
        //                        String sResponseFromServer = tReader.ReadToEnd();
        //                        string str = sResponseFromServer;
        //                        RemoveTokens(obj);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = ex.Message;
        //    }
        //}
        //////3
        //protected void RemoveTokens(List<string> obj)
        //{
        //    try
        //    {
        //        string applicationID = "AIzaSyAwyxZ_BKtY9rIFq5z0bh4K0MVNTvCJhHo";
        //        string senderId = "363060269669";
        //        //string deviceId = "/topics/" + topic;
        //        WebRequest tRequest = WebRequest.Create("https://iid.googleapis.com/iid/v1:batchRemove");
        //        tRequest.Method = "post";
        //        tRequest.ContentType = "application/json";
        //        var data = new
        //        {
        //            to = "/topics/notification",
        //            registration_tokens = obj
        //        };

        //        var serializer = new JavaScriptSerializer();
        //        var json = serializer.Serialize(data);
        //        Byte[] byteArray = Encoding.UTF8.GetBytes(json);
        //        tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
        //        tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
        //        tRequest.ContentLength = byteArray.Length;

        //        using (Stream dataStream = tRequest.GetRequestStream())
        //        {
        //            dataStream.Write(byteArray, 0, byteArray.Length);
        //            using (WebResponse tResponse = tRequest.GetResponse())
        //            {
        //                using (Stream dataStreamResponse = tResponse.GetResponseStream())
        //                {
        //                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
        //                    {
        //                        String sResponseFromServer = tReader.ReadToEnd();
        //                        string str = sResponseFromServer;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string str = ex.Message;
        //    }
        //}
        #endregion
        public BaseController()
        {
           
          
        }
       
        


    }
    public class Lazier<T> : Lazy<T> where T : class
    {
        public Lazier(IServiceProvider provider)
            : base(() => provider.GetRequiredService<T>())
        {
        }
    }
}
