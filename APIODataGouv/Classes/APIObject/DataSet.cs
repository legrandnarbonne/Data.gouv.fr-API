using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace APIODataGouv.Classes.APIObject
{
    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class DataSet : ContenairObject, IAPIObject
    {
        [JsonIgnore, Browsable(false)]
        public static string Parent = "organizations";

        [JsonIgnore, Browsable(false)]
        //public static bool NeedParentId = false;


        [DisplayName("Organisation"), ReadOnly(true), JsonIgnoreSerializationAttribute]//
        public Organization organization { get; set; }//read organization properties

        [Description("Needed only to build link to organisation when creating new Dataset"),
        DisplayName("Organisation"), JsonProperty(PropertyName = "organization"),
        DefaultValue(null), Browsable(false), JsonIgnoreDeSerialization]
        public OrganizationId organizationId { get; set; }//write organization object with id if set write on serialization, ignored on deserialization

        [JsonIgnoreSerializationAttribute, ReadOnly(true)]
        public Extras extras { get; set; }
        
        [DisplayName("Titre"), Category("Description"), IsRequired]
        public string title { get; set; }

        [JsonConverter(typeof(NotNullStringEnumConverter))]
        public FrequencyTypes frequency { get; set; }
        public string frequency_date { get; set; }

        [DisplayName("Dernière mise à jour"), ReadOnly(true), JsonIgnoreSerializationAttribute]
        public DateTime last_update { get; set; }

        [DisplayName("Licence")]
        public string license { get; set; }

        [DisplayName("Privé"), JsonProperty(PropertyName = "private")]
        public bool? publish { get; set; }

        [DisplayName("Resources associées"), ReadOnly(true), JsonIgnoreSerializationAttribute]
        public List<Resource> resources { get; set; }
        public string slug { get; set; }

        [DisplayName("Géolocalisation"), ReadOnly(true), JsonIgnoreSerializationAttribute]
        public Spatial spatial { get; set; }

        [DisplayName("Tags"), ReadOnly(true), JsonIgnoreSerializationAttribute]
        public List<string> tags { get; set; }

        [DisplayName("Couverture temporelle"), ReadOnly(true), JsonIgnoreSerializationAttribute]
        public TemporalCoverage temporal_coverage { get; set; }


        /// <summary>
        /// Set parent id for insert operation
        /// </summary>
        /// <param name="parentId"></param>
        public void SetParentId(string parentId)
        {
            organizationId = new OrganizationId() { id = parentId };
        }

        public bool IsParentIdRequired()
        {
            return false;
        }

        public override string ToString()
        {
            return title;
        }

        public bool ShouldSerializeorganizationId()
        { // don't serialize OrganizationId if null
            return organizationId != null;
           
        }
    }

}
