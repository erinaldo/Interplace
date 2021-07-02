using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interRegraNegocio.MercadoLivre
{
    public class MercadoLivreMensagens
    {
        public Paging paging { get; set; }
        public Conversation_Status conversation_status { get; set; }
        public Message[] messages { get; set; }
    }

    public class Conversation_Status
    {
        public string path { get; set; }
        public string status { get; set; }
        public object substatus { get; set; }
        public DateTime? status_date { get; set; }
        public bool status_update_allowed { get; set; }
        public object claim_ids { get; set; }
        public object shipping_id { get; set; }
    }

    public class Message
    {
        public string id { get; set; }
        public string site_id { get; set; }
        public long? client_id { get; set; }
        public From from { get; set; }
        public To to { get; set; }
        public string status { get; set; }
        public object subject { get; set; }
        public string text { get; set; }
        public Message_Date message_date { get; set; }
        public Message_Moderation message_moderation { get; set; }
        public Message_Attachments[] message_attachments { get; set; }
        public Message_Resources[] message_resources { get; set; }
        public bool conversation_first_message { get; set; }
    }

    public class From
    {
        public string user_id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
    }

    public class To
    {
        public string user_id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
    }

    public class Message_Date
    {
        public DateTime? received { get; set; }
        public DateTime? available { get; set; }
        public DateTime? notified { get; set; }
        public DateTime? created { get; set; }
        public DateTime? read { get; set; }
    }

    public class Message_Moderation
    {
        public string status { get; set; }
        public object reason { get; set; }
        public string source { get; set; }
        public DateTime? moderation_date { get; set; }
    }

    public class Message_Attachments
    {
        public string filename { get; set; }
        public string original_filename { get; set; }
        public string type { get; set; }
        public int? size { get; set; }
        public bool potential_security_threat { get; set; }
        public DateTime? creation_date { get; set; }
    }

    public class Message_Resources
    {
        public string id { get; set; }
        public string name { get; set; }
    }


}
