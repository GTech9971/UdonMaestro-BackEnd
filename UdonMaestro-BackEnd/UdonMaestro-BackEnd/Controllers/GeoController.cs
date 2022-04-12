using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


    }
}
