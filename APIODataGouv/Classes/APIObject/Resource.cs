
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel;

namespace APIODataGouv.Classes.APIObject
{
    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class Resource : ContenairObject,IAPIObject
    {
        [JsonIgnore, Browsable(false)]
        public static string Parent = "datasets";

        [JsonIgnore, Browsable(false)]
        public static bool NeedParentId = true;

        [DisplayName("Checksum"), ReadOnly(true), JsonIgnoreSerializationAttribute]
        public Checksum2 checksum { get; set; }


        [DisplayName("Titre"), Category("Description (obligatoire)"), IsRequired]
        public string title { get; set; }

        [ReadOnly(true), JsonIgnoreSerializationAttribute]
        public Extras extras { get; set; }

        [DisplayName("Taille du fichier"), ReadOnly(true), JsonIgnoreSerializationAttribute]
        public int? filesize { get; set; }

        [DisplayName("Type de fichier"),JsonConverter(typeof(NotNullStringEnumConverter))]
        public FileTypes filetype { get; set; }

        [DisplayName("Format")]
        public string format { get; set; }

        [ReadOnly(true), JsonIgnoreSerializationAttribute]
        public string latest { get; set; }

        [ReadOnly(true), JsonIgnoreSerializationAttribute]
        public string mime { get; set; }

        [ReadOnly(true), JsonIgnoreSerializationAttribute]
        public string preview_url { get; set; }

        [ReadOnly(true), JsonIgnoreSerializationAttribute]
        public DateTime published { get; set; }

        [ JsonConverter(typeof(NotNullStringEnumConverter))]
        public ResourceTypes type { get; set; }

       
        public Resource()
        {
            url = "https://www.data.gouv.fr";
        }

        public string url { get; set; }

        public override string ToString()
        {
            return title;
        }

        /// <summary>
        /// Set parent id , not needed for resource
        /// </summary>
        /// <param name="parentId"></param>
        public void SetParentId(string parentId)
        {
        }

        public bool IsParentIdRequired()
        {
            return true;
        }
    }
}
