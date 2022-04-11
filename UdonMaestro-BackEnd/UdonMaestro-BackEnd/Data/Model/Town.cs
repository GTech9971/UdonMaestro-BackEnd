using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdonMaestro_BackEnd.Data.Model {
    /// <summary>
    /// 町・群
    /// </summary>
    [Index(nameof(Name))]
    [Index(nameof(Lat))]
    [Index(nameof(Lon))]
    public class Town {

        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 町・群名
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// 緯度
        /// </summary>
        [Column(TypeName ="decimal(7,4)")]
        public decimal Lat { get; set; }

        /// <summary>
        /// 経度
        /// </summary>
        [Column(TypeName ="decimal(7,4)")]
        public decimal Lon { get; set; }

        /// <summary>
        /// 市ID
        /// </summary>
        [ForeignKey(nameof(City))]
        public int CityId { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public City? City { get; set; } = null!;
    }
}
