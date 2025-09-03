using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using Autodesk.Navisworks.Api;

namespace Libs.NavisworksAPI.Utils
{
    public class Point3DConverter : JsonConverter<Point3D>
    {
        public override Point3D ReadJson(JsonReader reader, Type objectType, Point3D existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JArray array = JArray.Load(reader);
            if (array.Count != 3)
                throw new JsonSerializationException("Point3D must have exactly 3 elements");
            return new Point3D((double)array[0], (double)array[1], (double)array[2]);
        }

        public override void WriteJson(JsonWriter writer, Point3D value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            writer.WriteValue(value.X);
            writer.WriteValue(value.Y);
            writer.WriteValue(value.Z);
            writer.WriteEndArray();
        }
    }
}
