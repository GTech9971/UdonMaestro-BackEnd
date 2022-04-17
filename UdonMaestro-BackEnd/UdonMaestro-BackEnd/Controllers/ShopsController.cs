using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdonMaestro_BackEnd.Data;
using UdonMaestro_BackEnd.Data.Model;
using UdonMaestro_BackEnd.Service;

namespace UdonMaestro_BackEnd.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShopsController : ControllerBase {

        private readonly ApplicationDbContext _context;
        private readonly GeoService geoService;

        public ShopsController(ApplicationDbContext context) {
            _context = context;
            geoService = new GeoService();
        }

        /// <summary>
        /// 店舗を取得する
        /// </summary>
        /// <param name="id">店舗ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Shop?>> GetShop(int id) {
            return await _context.Shops.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// 店舗リストを取得する
        /// </summary>
        /// <param name="pageIndex">ページインデックス</param>
        /// <param name="pageSize">ページサイズ</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PaginationResult<Shop>> GetShops(int pageIndex = 0, int pageSize = 10) {
            return await PaginationResult<Shop>.CreateAsync(
                _context.Shops.AsNoTracking(),
                pageIndex,
                pageSize);
        }

        /// <summary>
        /// 10km圏内にある店舗を取得する
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Shop>> GetShopsAround10km(decimal lat, decimal lon) {
            var shops = await _context.Shops.AsNoTracking().Include(s => s.Town).ToListAsync();
            List<Shop> list = new List<Shop>();
            foreach (var shop in shops) {
                if (shop.Lat == 0 || shop.Lon == 0) {
                    continue;
                }
                if (geoService.CalcDistcance(lat, lon, shop.Lat, shop.Lon) < 10) {
                    list.Add(shop);
                }
            }
            return list;
        }

        /// <summary>
        /// 町・群にある店舗リストを取得する
        /// </summary>
        /// <param name="townId">町・群ID</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PaginationResult<Shop>> GetShopsInTown(int townId, int pageIndex = 0, int pageSize = 10) {
            return await PaginationResult<Shop>.CreateAsync(
                _context.Shops.AsNoTracking().Where(x => x.TownId == townId).Include(s => s.Town),
                pageIndex,
                pageSize);
        }

        /// <summary>
        /// 入力された店舗情報が問題ないか調べる
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> IsValidShopAsync(Shop shop) {
            if (shop == null) { return false; }
            if (string.IsNullOrWhiteSpace(shop.Name)) { return false; }
            bool existsName = await _context.Shops.AnyAsync(x => x.Name == shop.Name);
            if (existsName) { return false; }

            return true;
        }

        [HttpGet]
        public async Task<bool> IsExistsShopAsync(int id) {
            return await _context.Shops.AnyAsync(x => x.Id == id);
        }

        /// <summary>
        /// 店舗を登録する
        /// </summary>
        /// <param name="shop"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostShop(Shop shop) {
            if (await IsValidShopAsync(shop) == false) {
                return BadRequest();
            }

            await _context.Shops.AddAsync(shop);
            int ret = await _context.SaveChangesAsync();
            if (ret > 0) {
                return Ok();
            } else {
                return BadRequest();
            }
        }

        /// <summary>
        /// 店舗を更新する
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shop"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> PutShop(int id, Shop shop) {
            if (await IsExistsShopAsync(id) == false) {
                return NotFound();
            }

            _context.Shops.Update(shop);
            int ret = await _context.SaveChangesAsync();
            if (ret > 0) {
                return Ok();
            } else {
                return BadRequest();
            }
        }


    }
}
