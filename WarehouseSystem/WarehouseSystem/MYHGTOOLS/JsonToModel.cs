using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace MYHGTOOLS
{
    public class JsonToModel<T> where T : new()
    {
        public static T jsonToOrder(string jsons)
        {
            T t = JsonConvert.DeserializeObject<T>(jsons);
            return t;
        }

        /// <summary>
        /// 将实体转换为json
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static string modelToJson(T Model)
        {
            return JsonConvert.SerializeObject(Model);
        }
    }
}
