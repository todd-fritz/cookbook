using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp.Deserializers;
using Newtonsoft.Json;

namespace inBloom_c_sharp_hello_world.Recipes
{
    public class inBloomDeserializer : IDeserializer
    {

        public string DateFormat { get; set; }

        public string Namespace { get; set; }
        
        public string RootElement  { get; set; }


        public T Deserialize<T>(RestSharp.IRestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}