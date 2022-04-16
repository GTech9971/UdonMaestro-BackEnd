using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdonMaestro_BackEnd.Data;
using UdonMaestro_BackEnd.Data.Model;

namespace UdonMaestro_BackEnd.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShopsController : ControllerBase {

        private ApplicationDbContext _context;
        private IWebHostEnvironment _env;

        public ShopsController(ApplicationDbContext context, IWebHostEnvironment env) {
            _context = context;
            env = _env;
        }

        [HttpGet]
        public async Task<ActionResult<Shop>> GetShop(int id) {

        }

        [HttpGet]
        public async Task<PaginationResult<Shop>> GetShops(int pageIndex = 0, int pageSize = 10) {
            return await PaginationResult<Shop>.CreateAsync(
                _context.Shops.AsNoTracking(),
                pageIndex,
                pageSize);
        }


    }
}
