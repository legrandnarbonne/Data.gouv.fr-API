using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace APIODataGouv.Classes.APIObject
{
    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class Organization : ContenairObject,IAPIObject
    {
        [JsonIgnore, Browsable(false)]
        public static string Parent = "";

        //[JsonIgnore, Browsable(false)]
        //public static bool NeedParentId = false;

        [DisplayName("Logo"), ReadOnly(true), JsonIgnoreSerializationAttribute]
        public string logo { get; set; }

        [DisplayName("Miniature logo"), ReadOnly(true), JsonIgnoreSerializationAttribute]
        public string logo_thumbnail { get; set; }

        [DisplayName("Utilisateurs associés"), ReadOnly(true), JsonIgnoreSerializationAttribute]
        public List<Member> members { get; set; }

        [DisplayName("Nom"), Category("Description")]
        public string name { get; set; }
        public string slug { get; set; }

        [DisplayName("URL"), ReadOnly(true), JsonIgnoreSerializationAttribute]
        public object url { get; set; }

        public bool IsParentIdRequired()
        {
            return false;
        }

        public void SetParentId(string parentID)
        {            
        }

        public override string ToString()
        {
            return name;
        }

    }

    /// <summary>
    /// minimal Organization object use only on insert to set link to organization
    /// </summary>
    public class OrganizationId
    {
        public string id { get; set; }

    }
}
