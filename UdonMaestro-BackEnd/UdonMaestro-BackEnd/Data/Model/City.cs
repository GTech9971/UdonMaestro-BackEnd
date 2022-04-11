using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdonMaestro_BackEnd.Data.Model {
    /// <summary>
    /// 市
    /// </summary>
    [Index(nameof(Name))]
    public class City {

        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 市名
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 県ID
        /// </summary>
        [ForeignKey(nameof(Province))]
        public int ProvinceId { get; set; }

        /// <summary>
        /// 県
        /// </summary>
        public Province? Province { get; set; } = null!;

        /// <summary>
        /// 町・群 リスト
        /// </summary>
        public ICollection<Town>? Towns { get; set; } = null!;
    }
}
