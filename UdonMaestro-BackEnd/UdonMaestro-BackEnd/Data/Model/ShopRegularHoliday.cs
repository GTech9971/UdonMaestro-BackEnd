using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdonMaestro_BackEnd.Data.Model {

    /// <summary>
    /// 店舗の定休日
    /// </summary>
    [Index(nameof(ShopId))]
    [Index(nameof(RegularHolidayId))]    
    public class ShopRegularHoliday {

        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 店舗ID
        /// </summary>
        [ForeignKey(nameof(Shop))]
        public int ShopId { get; set; }
        /// <summary>
        /// 店舗
        /// </summary>
        public Shop? Shop { get; set; } 

        /// <summary>
        /// 定休日ID
        /// </summary>
        [ForeignKey(nameof(RegularHoliday))]
        public int RegularHolidayId { get; set; }

        /// <summary>
        /// 定休日
        /// </summary>
        public RegularHoliday? RegularHoliday { get; set; }

    }
}
