using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;
using RestSharp.Serializers;
using Newtonsoft.Json;

namespace inBloom_c_sharp_hello_world.Recipes
{
    /// <summary>
	/// Serializer for inBloom. 
	/// Replaces SimpleJson with Json.Net
	/// </summary>
    public class inBloomSerializer : ISerializer
    {
        /// <summary>
		/// Default serializer
		/// </summary>
		public inBloomSerializer()
		{
			ContentType = "application/json";
		}

		/// <summary>
		/// Serialize the object as JSON
		/// </summary>
		/// <param name="obj">Object to serialize</param>
		/// <returns>JSON as String</returns>
		public string Serialize(object obj)
		{
			return JsonConvert.SerializeObject(obj);
		}

		/// <summary>
		/// Unused for JSON Serialization
		/// </summary>
		public string DateFormat { get; set; }
		/// <summary>
		/// Unused for JSON Serialization
		/// </summary>
		public string RootElement { get; set; }
		/// <summary>
		/// Unused for JSON Serialization
		/// </summary>
		public string Namespace { get; set; }
		/// <summary>
		/// Content type for serialized content
		/// </summary>
		public string ContentType { get; set; }
	}
}