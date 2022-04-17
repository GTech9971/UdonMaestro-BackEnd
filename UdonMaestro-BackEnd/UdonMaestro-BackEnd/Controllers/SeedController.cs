using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security;
using System.Text;
using UdonMaestro_BackEnd.Data;
using UdonMaestro_BackEnd.Data.Model;
using UdonMaestro_BackEnd.Service;

namespace UdonMaestro_BackEnd.Controllers {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SeedController : ControllerBase {

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SeedController(ApplicationDbContext context, IWebHostEnvironment env) {
            this._context = context;
            this._env = env;
        }

        /// <summary>
        /// 香川県のデータを設定する
        /// </summary>
        /// <returns></returns>
        /// <exception cref="SecurityException"></exception>
        [HttpGet]
        public async Task<ActionResult> Import() {
            if (_env.IsDevelopment() == false) {
                throw new SecurityException("Not allowed");
            }

            Encoding ENC = Encoding.GetEncoding("UTF-8");

            var path = Path.Combine(_env.ContentRootPath, "Data/Source/37kagawa.csv");
            using (var stream = new StreamReader(path, ENC)) {
                const char SPLIT = ',';

                //県
                await stream.ReadLineAsync();
                int numberOfProvinceAdd = 0;
                string line = null;
                while ((line = await stream.ReadLineAsync()) != null) {
                    line = line.Replace("\"", "");
                    string[] contexts = line.Split(SPLIT);

                    var provincdId = int.Parse(contexts[1]);
                    var provinceName = contexts[7];


                    var province = new Province() {
                        Id = provincdId,
                        Name = provinceName
                    };
                    if (_context.Provinces.ToList().Exists(x => x.Id == provincdId) == false) {
                        await _context.Provinces.AddAsync(province);
                        numberOfProvinceAdd++;
                    }
                    break;
                }

                if (numberOfProvinceAdd > 0) {
                    await this._context.SaveChangesAsync();
                }

                //市
                int numbeOfCityAdd = 0;
                var kagawaProvince = await _context.Provinces.FirstAsync();
                line = null;
                stream.BaseStream.Position = 0;
                await stream.ReadLineAsync();
                var citiesByName = _context.Cities.AsNoTracking().ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);
                while ((line = await stream.ReadLineAsync()) != null) {
                    line = line.Replace("\"", "");
                    string[] contexts = line.Split(SPLIT);

                    var cityId = contexts[2];
                    var cityName = contexts[9];
                    var city = new City() {
                        Id = int.Parse(cityId),
                        Name = cityName,
                        ProvinceId = kagawaProvince.Id,
                        Province = kagawaProvince,
                    };

                    if (citiesByName.ContainsKey(city.Name)) {
                        continue;
                    }

                    await _context.Cities.AddAsync(city);
                    citiesByName.Add(cityName, city);
                    numbeOfCityAdd++;
                }

                if (numbeOfCityAdd > 0) {
                    await _context.SaveChangesAsync();
                }


                //町・群
                int numberOfTownAdd = 0;
                var townsByName = _context.Towns.AsNoTracking().ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);
                line = null;
                GeoService geoService = new GeoService();
                stream.BaseStream.Position = 0;
                await stream.ReadLineAsync();
                while ((line = await stream.ReadLineAsync()) != null) {
                    line = line.Replace("\"", "");
                    string[] contexts = line.Split(SPLIT);

                    var cityId = int.Parse(contexts[2]);
                    var townId = int.Parse(contexts[3]);
                    var townName = contexts[11];
                    var postCode = contexts[4];

                    if (string.IsNullOrWhiteSpace(townName)) {
                        continue;
                    }

                    var city = await _context.Cities.Where(x => x.Id == cityId).FirstOrDefaultAsync();
                    if (city == null) {
                        continue;
                    }
                    var town = new Town() {
                        Id = townId,
                        Name = townName,
                        CityId = cityId,
                        City = city,
                        PostCode = postCode
                    };

                    if (townsByName.ContainsKey(townName)) {
                        continue;
                    }

                    var geoInfo = await geoService.GetGeoByPostCodeAsync(town.PostCode);
                    if (geoInfo != null) {
                        town.Lat = geoInfo.Item1;
                        town.Lon = geoInfo.Item2;
                    }


                    await _context.Towns.AddAsync(town);
                    townsByName.Add(townName, town);
                    numberOfTownAdd++;
                }

                if (numberOfTownAdd > 0) {
                    await _context.SaveChangesAsync();
                }


                return new JsonResult(new {
                    Provinces = numberOfProvinceAdd,
                    Cities = numbeOfCityAdd,
                    Towns = numberOfTownAdd
                });
            }
        }

        /// <summary>
        /// アプリのコードマスタデータを設定する
        /// 店のタイプ、定休日
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ImportMaster() {
            if (_env.IsDevelopment() == false) {
                throw new SecurityException("Not allowed");
            }

            //店舗の種類を追加
            List<ShopType> shopTypes = new List<ShopType>();
            shopTypes.Add(new ShopType() { Id = 1, Name = "一般店" });
            shopTypes.Add(new ShopType() { Id = 2, Name = "セルフ" });
            shopTypes.Add(new ShopType() { Id = 3, Name = "製麺所" });

            int numberOfAddShopType = 0;
            foreach (var type in shopTypes) {
                if (await _context.ShopTypes.ContainsAsync(type) == false) {
                    await _context.ShopTypes.AddAsync(type);
                    numberOfAddShopType++;
                }
            }

            if (numberOfAddShopType > 0) {
                await _context.SaveChangesAsync();
            }

            //定休日
            int numberOfAddRegularHoliday = 0;
            List<RegularHoliday> regularHolidays = new List<RegularHoliday>();
            regularHolidays.Add(new RegularHoliday() { Id = 1, Name = "日曜日" });
            regularHolidays.Add(new RegularHoliday() { Id = 2, Name = "月曜日" });
            regularHolidays.Add(new RegularHoliday() { Id = 3, Name = "火曜日" });
            regularHolidays.Add(new RegularHoliday() { Id = 4, Name = "水曜日" });
            regularHolidays.Add(new RegularHoliday() { Id = 5, Name = "木曜日" });
            regularHolidays.Add(new RegularHoliday() { Id = 6, Name = "金曜日" });
            regularHolidays.Add(new RegularHoliday() { Id = 7, Name = "土曜日" });
            regularHolidays.Add(new RegularHoliday() { Id = 8, Name = "祝日" });

            foreach (var holiday in regularHolidays) {
                if (await _context.RegularHolidays.ContainsAsync(holiday) == false) {
                    await _context.RegularHolidays.AddAsync(holiday);
                    numberOfAddRegularHoliday++;
                }
            }

            if (numberOfAddRegularHoliday > 0) {
                await _context.SaveChangesAsync();
            }

            return new JsonResult(new {
                ShopType = numberOfAddShopType,
                RegularHoliday = numberOfAddRegularHoliday
            });
        }


        [HttpGet]
        public async Task<ActionResult> ImportShops() {
            if (_env.IsDevelopment() == false) {
                throw new SecurityException("Not allowed");
            }

            List<Shop> shops = new List<Shop>();
            shops.Add(new Shop() {
                Id = 100,
                Name = "根っこ",
                ShopTypeId = 1,
                Comment = "test",
                StartTime = TimeOnly.Parse("09:00:00"),
                EndTime = TimeOnly.Parse("15:00:00"),
            });


            int numberOfShop = 0;
            foreach (var shop in shops) {
                if (await _context.Shops.ContainsAsync(shop)) {
                    continue;
                }

                await _context.Shops.AddAsync(shop);
                numberOfShop++;
            }

            if (numberOfShop > 0) {
                await _context.SaveChangesAsync();
            }

            return new JsonResult(new {
                Shops = numberOfShop
            });
        }
    }
}
