using System.Text.Json;

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
                    decimal lat = decimal.Parse(location.y);
                    decimal lon = decimal.Parse(location.x);
                    return new Tuple<decimal, decimal>(lat, lon);
                }
            }

            return null;
        }

        /// <summary>
        /// 2点間の緯度経度から距離を取得する
        /// Vincenty 法らしい
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        public double CalcDistcance(decimal lat1, decimal lon1, decimal lat2, decimal lon2) {
            double x1 = ((double)lat1 * Math.PI) / 180;
            double y1 = ((double)lon1 * Math.PI) / 180;
            double x2 = ((double)lat2 * Math.PI) / 180;
            double y2 = ((double)lon2 * Math.PI) / 180;

            //地球の半径
            const double RADIUS = 6378.137;

            //差の絶対値
            double diffY = Math.Abs((y1 - y2));

            double calc1 = Math.Cos(x2) * Math.Sin(diffY);
            double calc2 = Math.Cos(x1) * Math.Sin(x2) - Math.Sin(x1) * Math.Cos(x2) * Math.Cos(diffY);


            //分子
            double numerator = Math.Sqrt(Math.Pow(calc1, 2) + Math.Pow(calc2, 2));

            //分母
            double denominator = Math.Sin(x1) * Math.Sin(x2) + Math.Cos(x1) * Math.Cos(x2) * Math.Cos(diffY);

            double degree = Math.Atan2(numerator, denominator);

            return degree * RADIUS;
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
