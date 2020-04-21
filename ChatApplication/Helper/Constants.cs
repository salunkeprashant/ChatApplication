using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.Helper
{
    public static class Constants
    {
        public const string PolicyName = "CorsPolicy";

        public const string Secret = "RealTimeChatApplicationByPrashant";

        public static ObjectId BuildObjectId(dynamic obj) {

            JObject newObj = JsonConvert.DeserializeObject<dynamic>(Convert.ToString(obj));
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

            double ts = newObj["timestamp"].Value<double>();
            var timestamp = origin.AddSeconds(ts);
            int machine = newObj["machine"].Value<int>();
            short pid = newObj["pid"].Value<short>();
            int increment = newObj["increment"].Value<int>();;

            return new ObjectId(timestamp, machine, pid, increment);
        }
    }
}
