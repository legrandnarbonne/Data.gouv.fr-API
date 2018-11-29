using System;
using System.Net;

namespace APIODataGouv
{
    public interface IAPIListInformation
    {
        Uri NextPage { get; set; }
        int Page { get; set; }
        int PageSize { get; set; }
        Uri PreviousPage { get; set; }
        HttpStatusCode Statut { get; }
        int TotalPage { get; set; }
    }
}