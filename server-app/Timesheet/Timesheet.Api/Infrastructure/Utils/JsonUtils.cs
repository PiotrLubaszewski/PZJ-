namespace Timesheet.Api.Infrastructure.Utils
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class JsonUtils
    {
        public static string SerializeObjectWithCamelCasePropertyNames(object value)
        {
            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return JsonConvert.SerializeObject(value, Formatting.Indented, settings);
        }
    }
}
