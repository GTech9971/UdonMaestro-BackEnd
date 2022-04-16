using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdonMaestro_BackEnd.Data;
using UdonMaestro_BackEnd.Data.Model;

namespace UdonMaestro_BackEnd.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProvincesController : ControllerBase {

        private readonly ApplicationDbContext _context;

        public ProvincesController(ApplicationDbContext context) {
            _context = context;
        }

        /// <summary>
        /// 県のリストを取得する
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<PaginationResult<Province>>> GetProvinces(int pageIndex = 0, int pageSize = 10) {
            return await PaginationResult<Province>.CreateAsync(
                _context.Provinces.AsNoTracking(),
                pageIndex,
                pageSize);
        }

        /// <summary>
        /// 県を取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<Province?>> GetProvince(int id) {
            return await _context.Provinces.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

    }
}
