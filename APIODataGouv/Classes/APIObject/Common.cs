using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace APIODataGouv.Classes.APIObject
{

    public enum FrequencyTypes { unknown, punctual, continuous, hourly, fourTimesADay, threeTimesADay, semidaily, daily, fourTimesAWeek, threeTimesAWeek, semiweekly, weekly, biweekly, threeTimesAMonth, semimonthly, monthly, bimonthly, quarterly, threeTimesAYear, semiannual, annual, biennial, triennial, quinquennial, irregular }

    public enum CheckSumTypes { sha1, sha2, sha256, md5, crc };

    public enum GeomTypes { Point, LineString, Polygon, MultiPoint, MultiLineString, MultiPolygon };

    public enum MembersRoleTypes { admin, editor };

    public enum FileTypes { file, remote };

    public enum ResourceTypes { main, documentation, update, api, code, other };

    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class Badge
    {
        public string kind { get; set; }
    }

    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class Checksum
    {
        [JsonConverter(typeof(NotNullStringEnumConverter))]
        public CheckSumTypes type { get; set; }
        public string value { get; set; }

        public override string ToString()
        {
            return $"{value} ({type})";
        }
    }

    /// <summary>
    /// Additionnal properties
    /// </summary>
    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class Extras
    {
        public string datagouv_ckan_id { get; set; }
        public DateTime datagouv_ckan_last_sync { get; set; }
    }

    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class Metrics
    {
        public int dataset_views { get; set; }
        public int datasets { get; set; }
        public int followers { get; set; }
        public int members { get; set; }
        public int nb_hits { get; set; }
        public int nb_uniq_visitors { get; set; }
        public int nb_visits { get; set; }
        public int permitted_reuses { get; set; }
        public int resource_downloads { get; set; }
        public int reuse_views { get; set; }
        public int reuses { get; set; }
        public int views { get; set; }

        public override string ToString()
        {
            return $"{views} vue(s)";
        }
    }

    public class APIObject
    {

        public static Dictionary<Type, APIObjectProperties> APIObjectPropertiesDictionary = new Dictionary<Type, APIObjectProperties> {
            { typeof(Organization),new APIObjectProperties() },
            { typeof(DataSet),new APIObjectProperties { ParentName="organizations" } },
            { typeof(Resource),new APIObjectProperties{ ParentName="datasets",ParentIdNeeded=true } }
        };

        [DisplayName("ID"), Category("Description"), ReadOnly(true), JsonIgnoreSerializationAttribute]
        public string id { get; set; }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                ContractResolver = new JsonPropertiesResolver()//avoid serialization of properties tagged JsonIgnoreSerializationAttribute
               // ,DefaultValueHandling = DefaultValueHandling.Ignore// avoid null parent serialization
            });
        }

    }

    [TypeConverter(typeof(SerializableExpandableObjectConverter)), DefaultPropertyAttribute("title")]
    public class ContenairObject : APIObject
    {

        [JsonIgnoreSerializationAttribute]
        public List<Badge> badges { get; set; }

        public string acronym { get; set; }

        [DisplayName("Url de la page"), ReadOnly(true), JsonIgnoreSerializationAttribute]
        public string page { get; set; }


        [DisplayName("Url (API)"), ReadOnly(true), JsonIgnoreSerializationAttribute, IsRequired]
        public string uri { get; set; }

        [DisplayName("Créé le"), ReadOnly(true), JsonIgnoreSerializationAttribute]
        public DateTime created_at { get; set; }

        [DisplayName("Effacé"), ReadOnly(true)]
        public DateTime? deleted { get; set; }

        [DisplayName("Description"), Category("Description"), IsRequired]
        public string description { get; set; }

        [DisplayName("Dernière modification"), ReadOnly(true), JsonIgnoreSerializationAttribute]
        public DateTime last_modified { get; set; }

        [DisplayName("Statistiques"), ReadOnly(true), JsonIgnoreSerializationAttribute]
        public Metrics metrics { get; set; }

        public override string ToString()
        {
            return "";
        }
    }

    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class User
    {
        public string avatar { get; set; }
        public string avatar_thumbnail { get; set; }
        public string @class { get; set; }
        public string first_name { get; set; }
        public string id { get; set; }
        public string last_name { get; set; }
        public string page { get; set; }
        public string slug { get; set; }
        public string uri { get; set; }

        public override string ToString()
        {
            return $"{last_name} {first_name}";
        }
    }

    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class Member
    {
        [JsonConverter(typeof(NotNullStringEnumConverter))]
        public MembersRoleTypes role { get; set; }
        public User user { get; set; }
        public override string ToString()
        {
            return user.ToString();
        }
    }

    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class Owner
    {
        public string @class { get; set; }
        public string id { get; set; }
        public string avatar { get; set; }
        public string avatar_thumbnail { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string page { get; set; }
        public string slug { get; set; }
        public string uri { get; set; }

        public override string ToString()
        {
            return $"{last_name} {first_name}";
        }
    }

    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class CommunityResource
    {
        public Checksum checksum { get; set; }
        public DateTime created_at { get; set; }
        public string description { get; set; }
        public Extras extras { get; set; }
        public int filesize { get; set; }

        [JsonConverter(typeof(NotNullStringEnumConverter))]
        public FileTypes filetype { get; set; }
        public string format { get; set; }
        public string id { get; set; }
        public DateTime last_modified { get; set; }
        public string latest { get; set; }
        public Metrics metrics { get; set; }
        public string mime { get; set; }
        public string preview_url { get; set; }
        public DateTime published { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string url { get; set; }
        public DataSet dataset { get; set; }
        public Organization organization { get; set; }
        public Owner owner { get; set; }

        public override string ToString()
        {
            return title;
        }
    }

    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class Quality
    {
    }

    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class Checksum2
    {
        public string type { get; set; }
        public string value { get; set; }

        public override string ToString()
        {
            return $"{value}({type})";
        }
    }

    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class Coordinate
    {
    }

    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class Geom
    {
        public List<Coordinate> coordinates { get; set; }

        [JsonConverter(typeof(NotNullStringEnumConverter))]
        public GeomTypes type { get; set; }
    }

    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class Spatial
    {
        public Geom geom { get; set; }
        public string granularity { get; set; }
        public List<string> zones { get; set; }
    }

    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class Metrics2
    {
        public int nb_hits { get; set; }
        public int nb_uniq_visitors { get; set; }
        public int nb_visits { get; set; }
        public int views { get; set; }
    }

    [TypeConverter(typeof(SerializableExpandableObjectConverter))]
    public class TemporalCoverage
    {
        public DateTime end { get; set; }
        public DateTime start { get; set; }

        public override string ToString()
        {
            return $"{start}-{end}";
        }
    }

    /// <summary>
    /// implement object hierarchy
    /// </summary>
    public class APIObjectProperties
    {
        public string ParentName { get; set; }
        public bool ParentIdNeeded { get; set; }
    }

    public interface IAPIObject
    {
        void SetParentId(string parentID);

        bool IsParentIdRequired();

        string Serialize();
    }


}
