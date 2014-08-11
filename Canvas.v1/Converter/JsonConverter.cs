using Newtonsoft.Json;

namespace Canvas.v1.Converter
{
    public class JsonConverter : IJsonConverter
    {
        JsonSerializerSettings _settings;

        /// <summary>
        /// Instantiates a new JsonConverter that converts JSON
        /// </summary>
        public JsonConverter()
        {
            _settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatString = "yyyy-MM-ddTHH:mm:sszzz"
            };
        }

        /// <summary>
        /// Parses a JSON string into the provided type T
        /// </summary>
        /// <typeparam name="T">The type that the content should be parsed into</typeparam>
        /// <param name="content">The JSON string</param>
        /// <returns>The box representation of the JSON</returns>
        public T Parse<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        /// <summary>
        /// Serializes the Box type into JSON
        /// </summary>
        /// <typeparam name="T">The type of the entity to serialize</typeparam>
        /// <param name="entity">The entity to serialize</param>
        /// <returns>JSON string</returns>
        public string Serialize<T>(T entity)
        {
            return JsonConvert.SerializeObject(entity, _settings);
        }
    }
}
