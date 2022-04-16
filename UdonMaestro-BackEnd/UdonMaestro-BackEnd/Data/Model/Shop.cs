using System.ComponentModel.DataAnnotations;

namespace UdonMaestro_BackEnd.Data.Model {
    /// <summary>
    /// うどん店舗
    /// </summary>
    public class Shop {

        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// 店舗名
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;

        /// <summary>
        /// 店舗の種類
        /// </summary>
        [Required]
        public ShopType ShopType { get; set; }

    }
}
