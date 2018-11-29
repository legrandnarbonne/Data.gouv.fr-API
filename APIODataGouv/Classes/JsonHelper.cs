
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace APIODataGouv.Classes
{

    public static class JsonHelper
    {
        public static void SetDefaultValues(object obj)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj))
            {
                // Set default value if DefaultValueAttribute is present
                var attr = prop.Attributes[typeof(DefaultValueAttribute)]
                    as DefaultValueAttribute;
                if (attr != null)
                    prop.SetValue(obj, attr.Value);
            }
        }
    }

    //thanks to https://stackoverflow.com/questions/17560085/problems-using-json-net-with-expandableobjectconverter/17663930#17663930
    public class SerializableExpandableObjectConverter : ExpandableObjectConverter
    {

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType.Name == "String")
                return false;
            else
                return base.CanConvertTo(context, destinationType);
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType.Name == "String")
                return false;
            else
                return base.CanConvertFrom(context, sourceType);


        }
    }

    //thanks to https://stackoverflow.com/questions/11564091/making-a-property-deserialize-but-not-serialize-with-json-net
    public class JsonIgnoreSerializationAttribute : Attribute { }
    public class JsonIgnoreDeSerializationAttribute : Attribute { }

    public class IsRequired : Attribute { }
    class JsonPropertiesResolver : DefaultContractResolver
    {
        // ignore properties with JsonIgnoreSerializationAttribute on serialization
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            //Return properties that do NOT have the JsonIgnoreSerializationAttribute
            return objectType.GetProperties()
                             .Where(pi => !Attribute.IsDefined(pi, typeof(JsonIgnoreSerializationAttribute)))
                             .ToList<MemberInfo>();
        }

    }


    class JsonDeserializePropertiesResolver : DefaultContractResolver
    {
        // ignore properties with JsonIgnoreDeSerializationAttribute on deserialization
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            //Return properties that do NOT have the JsonIgnoreSerializationAttribute
            return objectType.GetProperties()
                             .Where(pi => !Attribute.IsDefined(pi, typeof(JsonIgnoreDeSerializationAttribute)))
                             .ToList<MemberInfo>();
        }
    }

    //default newtonsoft converter not serialize first value
    public class NotNullStringEnumConverter : Newtonsoft.Json.Converters.StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            base.WriteJson(writer, value, serializer);
        }
    }


}
