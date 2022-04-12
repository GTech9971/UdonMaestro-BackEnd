using System.Text.Json;
using System.Text.Json.Serialization;

namespace UdonMaestro_BackEnd.Service {
    /// <summary>
    /// 緯度経度サービス
    /// </summary>
    public class GeoService {

        private const string API_URL = "https://geoapi.heartrails.com/api/json?method=searchByPostal&postal=";

        private readonly HttpClient _client;

        public GeoService() {
            this._client = new HttpClient();
        }

        /// <summary>
        /// 郵便番号から緯度経度を取得する
        /// 取得に失敗した場合、nullを返す。
        /// </summary>
        /// <param name="postCode">郵便番号 TTTMMMM</param>
        /// <returns>Item1:緯度, Item2:経度</returns>
        public async Task<Tuple<decimal, decimal>> GetGeoByPostCodeAsync(string postCode) {           
            string requestUrl = $"{API_URL}{postCode}";
            var response = await this._client.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode) {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Root geoResponse =  JsonSerializer.Deserialize<Root>(jsonStr);
                var location = geoResponse?.response?.location?.FirstOrDefault();

                if(location != null) {
                    decimal lat = decimal.Parse(location.x);
                    decimal lon = decimal.Parse(location.y);
                    return new Tuple<decimal, decimal>(lat, lon);
                }
            }

            return null;
        }


        public class Location {
            public string city { get; set; }
            public string city_kana { get; set; }
            public string town { get; set; }
            public string town_kana { get; set; }
            public string x { get; set; }
            public string y { get; set; }
            public string prefecture { get; set; }
            public string postal { get; set; }
        }

        public class Response {
            public List<Location> location { get; set; }
        }

        public class Root {
            public Response response { get; set; }
        }

    }
}
