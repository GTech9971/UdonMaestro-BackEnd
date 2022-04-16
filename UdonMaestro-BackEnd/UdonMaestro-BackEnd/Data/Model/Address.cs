using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdonMaestro_BackEnd.Data.Model {
    /// <summary>
    /// 住所
    /// </summary>
    [Index(nameof(Name))]
    [Index(nameof(Lat))]
    [Index(nameof(Lon))]
    public class Address {
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 番地
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 緯度
        /// </summary>
        [Column(TypeName = "decimal(7,4)")]
        public decimal Lat { get; set; }

        /// <summary>
        /// 経度
        /// </summary>
        [Column(TypeName = "decimal(7,4)")]
        public decimal Lon { get; set; }

        /// <summary>
        /// 郵便番号
        /// </summary>
        public string PostCode { get; set; } = null!;

        /// <summary>
        /// 町・群ID
        /// </summary>
        [ForeignKey(nameof(Town))]
        public int TownId { get; set; }

        /// <summary>
        /// 町・群
        /// </summary>
        public Town? Town { get; set; } = null!;
    }
}
