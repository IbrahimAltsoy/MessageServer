using Application.Services.Repositories;
using Application.Services.SmsService;
using Domain.AdapterModels.SmsModels;
using Domain.Entities;
using System.Text;
using System.Xml.Serialization;

namespace Infrastructure.Adapter.MasSms;

public class MasSmsAdapter : ISmsService
{
    private readonly static string _username = "5321561202";
    private readonly static string _password = "5201385eee8be320642739620660fa42";
    private readonly static string _header = "E.HALIYKAMA";


    private readonly IHttpClientFactory _httpClientFactory;

    public MasSmsAdapter(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
      
    }

    public async Task<SmsResponseModel> TitleInquiry()
    {
        string endpoint = $"originator/v1?username={_username}&password={_password}";
        var client = _httpClientFactory.CreateClient("MasSms");
        var response = await client.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        string resultString = await response.Content.ReadAsStringAsync();
        var resultData = await MasSmsHelper.ResultPretier(resultString);
        return resultData;
    }

    public async Task<SmsResponseModel> CreditInquiry()
    {
        string endpoint = $"credit/v1?username={_username}&password={_password}";
        var client = _httpClientFactory.CreateClient("MasSms");
        var response = await client.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        string resultString = await response.Content.ReadAsStringAsync();
        var resultData = await MasSmsHelper.ResultPretier(resultString);
        return resultData;
    }

    public async Task<object> SendSms(string phone, string message, DateTime sendDate = default)
    {
        string endpoint = $"smspost/v1";
        var client = _httpClientFactory.CreateClient("MasSms");

        if (sendDate == default(DateTime))
        {
            sendDate = DateTime.UtcNow;
        }

        var smsRequest = new SmsSendRequest
        {
            username = _username,
            password = _password,
            header = _header,
            validity = 2880,
            sendDateTime = sendDate.ToString("yyyy.M.d.H.m.s"),
            messages = new Messages
            {
                mb = new MessageBody
                {
                    no = phone,
                    msg = message
                }
            }
        };

        var xmlContent = SerializeToXml(smsRequest);
        var content = new StringContent(xmlContent, Encoding.UTF8, "application/xml");

        //Console.WriteLine(xmlContent);

        var response = await client.PostAsync(endpoint, content);

        if (response.IsSuccessStatusCode)
        {
            //Sms sms = new Sms()
            //{
            //    Title = _header,
            //    Message = message,

            //};
            ////SmsTemplate smsTemplate = new SmsTemplate();
            //await _smsRepository.AddAsync(sms);

            var responseContent = await response.Content.ReadAsStringAsync();
            //Console.WriteLine("Response XML: " + responseContent);  
            string resultString = await response.Content.ReadAsStringAsync();
            var resultData = await MasSmsHelper.ResultPretier(resultString);            
            return resultData;
        }
        else
        {
            throw new Exception("SMS gönderimi başarısız oldu: " + response.ReasonPhrase);
        }
    }

    private string SerializeToXml<T>(T obj)
    {
        using (var stringWriter = new StringWriter())
        {
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stringWriter, obj);
            return stringWriter.ToString();
        }
    }

    //private T DeserializeFromXml<T>(string xml)
    //{
    //    using (var stringReader = new StringReader(xml))
    //    {
    //        var serializer = new XmlSerializer(typeof(T));
    //        return (T)serializer.Deserialize(stringReader);
    //    }
    //}
}
