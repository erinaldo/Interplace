using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interRegraNegocio.MercadoLivre
{

    public class MercadoLivreEntrega
    {
        public long? id { get; set; }
        public string mode { get; set; }
        public string created_by { get; set; }
        public long? order_id { get; set; }
        public object order_cost { get; set; }
        public object base_cost { get; set; }
        public string site_id { get; set; }
        public string status { get; set; }
        public object substatus { get; set; }
        public Status_History status_history { get; set; }
        public DateTime? date_created { get; set; }
        public DateTime? last_updated { get; set; }
        public object tracking_number { get; set; }
        public object tracking_method { get; set; }
        public object service_id { get; set; }
        public object carrier_info { get; set; }
        public int? sender_id { get; set; }
        public Sender_Address sender_address { get; set; }
        public int? receiver_id { get; set; }
        public Receiver_Address receiver_address { get; set; }
        public Shipping_Items[] shipping_items { get; set; }
        public Shipping_Option shipping_option { get; set; }
        public object comments { get; set; }
        public object date_first_printed { get; set; }
        public string market_place { get; set; }
        public object return_details { get; set; }
        public object[] tags { get; set; }
        public object return_tracking_number { get; set; }
        public object carrier_id { get; set; }
        public Cost_Components cost_components { get; set; }
    }

    public class Status_History
    {
        public object date_shipped { get; set; }
        public object date_delivered { get; set; }
    }

    public class Sender_Address
    {
        public int? id { get; set; }
        public string address_line { get; set; }
        public string street_name { get; set; }
        public string street_number { get; set; }
        public string comment { get; set; }
        public string zip_code { get; set; }
        public City city { get; set; }
        public State state { get; set; }
        public Country country { get; set; }
        public Neighborhood neighborhood { get; set; }
        public Municipality municipality { get; set; }
        public object agency { get; set; }
        public string[] types { get; set; }
        public object latitude { get; set; }
        public object longitude { get; set; }
        public object geolocation_type { get; set; }
        public object geolocation_last_updated { get; set; }
        public object geolocation_source { get; set; }
    }





    public class Neighborhood
    {
        public object id { get; set; }
        public string name { get; set; }
    }

    public class Municipality
    {
        public object id { get; set; }
        public object name { get; set; }
    }



    public class City1
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class State1
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Country1
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Neighborhood1
    {
        public object id { get; set; }
        public string name { get; set; }
    }

    public class Municipality1
    {
        public object id { get; set; }
        public object name { get; set; }
    }


    public class Cost_Components
    {
        public int? special_discount { get; set; }
    }


}
