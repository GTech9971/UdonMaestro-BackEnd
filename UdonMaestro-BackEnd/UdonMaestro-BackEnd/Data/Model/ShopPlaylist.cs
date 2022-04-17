using System.ComponentModel.DataAnnotations;

namespace UdonMaestro_BackEnd.Data.Model {
    /// <summary>
    /// 店舗のプレイリスト
    /// </summary>
    public class ShopPlaylist {
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// プレイリストタイトル
        /// </summary>
        [Required]
        public string? Title { get; set; } = null!;

        /// <summary>
        /// サブタイトル
        /// </summary>
        public string? SubTitle { get; set; }

        /// <summary>
        /// 店舗リスト
        /// </summary>
        public ICollection<Shop>? Shops { get; set; }
    }
}
