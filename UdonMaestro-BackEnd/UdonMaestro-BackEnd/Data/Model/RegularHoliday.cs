using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UdonMaestro_BackEnd.Data.Model {
    /// <summary>
    /// 定休日
    /// </summary>
    [Index(nameof(Name))]
    public class RegularHoliday {

        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 曜日名
        /// </summary>
        public string Name { get; set; } = null!;
    }
}
