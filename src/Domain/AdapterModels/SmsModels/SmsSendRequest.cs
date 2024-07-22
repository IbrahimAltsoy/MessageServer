using System.Xml.Serialization;

namespace Domain.AdapterModels.SmsModels;

[XmlRoot("sms")]
public class SmsSendRequest
{
    public string username { get; set; }
    public string password { get; set; }
    public string header { get; set; }
    public int validity { get; set; }
    public string sendDateTime { get; set; }
    public Messages messages { get; set; }
}

public class Messages
{
    [XmlElement("mb")]
    public MessageBody mb { get; set; }
}

public class MessageBody
{
    [XmlElement("no")]
    public string no { get; set; }
    [XmlElement("msg")]
    public string msg { get; set; }
}