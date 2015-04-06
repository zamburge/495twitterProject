using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterFind;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace TwitterFind
{

    class events
    {

        public static void download_serialized_json_data()
        {
            var url = "http://enter77.ius.edu/~cjkimmer/test.json";
            using (var w = new WebClient())
            {

                var json_data = string.Empty;
                // attempt to download JSON data as a string
                
                    json_data = w.DownloadString(url);
                }
               
        }

        public static void parse_json()
        {
            JsonObject jsonObject = new JsonObject;
        }
    }
}
