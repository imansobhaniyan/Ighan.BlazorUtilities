using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ighan.BlazorUtilities.Utilities
{
    public class QueryHelper
    {
        private readonly NavigationManager navigationManager;

        private List<Query> queries;

        public QueryHelper(NavigationManager navigationManager)
        {
            this.navigationManager = navigationManager;
            navigationManager.LocationChanged += NavigationManager_LocationChanged;
            NavigationManager_LocationChanged(this, null);
        }

        private void NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            if (navigationManager.Uri.Contains('?'))
                queries = navigationManager.Uri.Split('?')[1].Split('&').Select(query => new Query(query)).ToList();
            else
                queries = new List<Query>();
        }

        public string GetValue(string key)
        {
            return queries.Where(f => f.Key == key).Select(f => f.Value).FirstOrDefault();
        }

        public string this[string key]
        {
            get { return GetValue(key); }
        }

        private class Query
        {
            public Query(string query)
            {
                Key = query.Split('=')[0];
                Value = System.Net.WebUtility.UrlDecode(query.Split('=')[1]);
                
                if (bool.TryParse(Value, out bool boolResult))
                    ValueAsBool = boolResult;
                if (int.TryParse(Value, out int intResult))
                    ValueAsInt = intResult;
            }

            public string Key { get; private set; }

            public string Value { get; private set; }

            public int? ValueAsInt { get; private set; }

            public bool? ValueAsBool { get; private set; }
        }
    }
}
