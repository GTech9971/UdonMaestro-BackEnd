using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security;
using UdonMaestro_BackEnd.Data;
using UdonMaestro_BackEnd.Data.Model;
using UdonMaestro_BackEnd.Service;

namespace UdonMaestro_BackEnd.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TownsController : ControllerBase {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly GeoService geoService;

        public TownsController(ApplicationDbContext context, IWebHostEnvironment env) {
            _context = context;
            _env = env;
            geoService = new GeoService();
        }

        /// <summary>
        /// 緯度経度が0のデータに対し再取得を行い、更新する
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SecurityException"></exception>
        [HttpGet]
        public async Task<ActionResult> RefreshLatLon() {
            if(_env.IsDevelopment() == false) {
                throw new SecurityException("Not allowed");
            }

            int numberOfUpdateTown = 0;
            var towns = await _context.Towns.ToListAsync();
            foreach(var town in towns) {
                if(town.Lat == 0 || town.Lon == 0) {
                    var info = await geoService.GetGeoByPostCodeAsync(town.PostCode);
                    Thread.Sleep(100);
                    if(info != null) {
                        town.Lat = info.Item1;
                        town.Lon = info.Item2;
                        _context.Towns.Update(town);
                        numberOfUpdateTown++;
                    }
                }
            }

            if(numberOfUpdateTown > 0) {
                await _context.SaveChangesAsync();
            }

            return new JsonResult(new {
                Towns=numberOfUpdateTown
            });
        }

        /// <summary>
        /// 町・群を取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Town?>> GetTown(int id) {
            return await _context.Towns.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// 町・群のリストを取得する
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<PaginationResult<Town>>> GetTowns(int pageIndex = 0, int pageSize = 10) {
            return await PaginationResult<Town>.CreateAsync(
                _context.Towns.AsNoTracking(),
                pageIndex,
                pageSize);
        }

    }
}
