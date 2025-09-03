using Autodesk.Navisworks.Api;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Libs.NavisworksAPI.Utils
{
    public class Vector3DConverter : JsonConverter<Vector3D>
    {
        public override Vector3D ReadJson(JsonReader reader, Type objectType, Vector3D existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JArray array = JArray.Load(reader);
            if (array.Count != 3)
                throw new JsonSerializationException("Vector3D must have exactly 3 elements");
            return new Vector3D((double)array[0], (double)array[1], (double)array[2]);
        }

        public override void WriteJson(JsonWriter writer, Vector3D value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            writer.WriteValue(value.X);
            writer.WriteValue(value.Y);
            writer.WriteValue(value.Z);
            writer.WriteEndArray();
        }
    }
}
