﻿using System;
using System.Web;

namespace APIODataGouv.Classes
{
    public static class HttpExtensions
    {
        public static Uri AddQuery(this Uri uri, string name, string value)
        {
            if (value == null) return uri;

            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);

            httpValueCollection.Remove(name);
            httpValueCollection.Add(name, value);

            var ub = new UriBuilder(uri);
            ub.Query = httpValueCollection.ToString();

            return ub.Uri;
        }
    }
}