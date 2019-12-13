using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EclEngine.Utils
{
    public static class JsonUtil
    {
        public static DataTable DeserializeToDatatable(string json)
        {
            return (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
        }

       public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
