using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UdonMaestro_BackEnd.Data.Model {
    /// <summary>
    /// 県
    /// </summary>
    [Index(nameof(Name))]
    public class Province {

        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 県名
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 市のリスト
        /// </summary>
        public ICollection<City>? Cities { get; set; } = null!;

    }
}
