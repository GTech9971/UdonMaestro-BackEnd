using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UdonMaestro_BackEnd.Data;
using UdonMaestro_BackEnd.Data.Model;
using UdonMaestro_BackEnd.Service;

namespace UdonMaestro_BackEnd.Controllers {
    /// <summary>
    /// 緯度経度を取得するコントローラー
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GeoController : ControllerBase {

        private readonly ApplicationDbContext _context;
        private readonly GeoService geoService;

        public GeoController(ApplicationDbContext context) {
            _context = context;
            geoService = new GeoService();
        }

        /// <summary>
        /// 郵便番号から緯度経度を取得する
        /// </summary>
        /// <param name="postcode"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Tuple<decimal,decimal>>> GetGeoByPostCode(string postCode) {
            var formatPostCode = postCode;
            if (postCode.Contains("-")) {
                formatPostCode = formatPostCode.Replace("-", "");
            }
            Tuple<decimal, decimal> geo = await geoService.GetGeoByPostCodeAsync(formatPostCode);
            if(geo == null) {
                return BadRequest($"郵便番号:{postCode} から緯度経度が取得できませんでした。");
            }

            return geo;
        }

        /// <summary>
        /// 2点間の緯度経度から距離を取得する
        /// 何も設定しなければ高松、坂出の距離を取得する
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        [HttpGet]
        public double GetDistance(decimal lat1 = (decimal)34.34028, decimal lon1 = (decimal)134.04333, decimal lat2 = (decimal)34.314202, decimal lon2 = (decimal)133.882861) {
            return geoService.CalcDistcance(lat1, lon1, lat2, lon2);
        }


        /// <summary>
        /// 入力された緯度経度付近10kmの町・群のリストを返す
        /// </summary>
        /// <param name="lat">緯度</param>
        /// <param name="lon">経度</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Town>>> GetTownsAround10Km(decimal lat, decimal lon) {
            if (lat == 0 || lon == 0) {
                return BadRequest($"有効な緯度経度を入力してください。 緯度:{lat} 経度:{lon}");
            }

            List<Town> list = new List<Town>();
            var towns = await _context.Towns.ToListAsync();
            towns.ForEach(town => {
                if (town.Lat != 0 && town.Lon != 0) {
                    double distance = geoService.CalcDistcance(lat, lon, town.Lat, town.Lon);
                    Debug.WriteLine(distance);
                    if (distance < 10) {
                        list.Add(town);
                    }
                }
            });

            return list;
        }

    }
}
