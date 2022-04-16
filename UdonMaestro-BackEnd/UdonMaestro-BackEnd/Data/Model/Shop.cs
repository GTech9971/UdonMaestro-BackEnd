using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdonMaestro_BackEnd.Data.Model {
    /// <summary>
    /// うどん店舗
    /// </summary>
    [Index(nameof(Name))]
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
        /// 店舗の種類ID
        /// </summary>
        [Required]
        public int ShopTypeId { get; set; }

        /// <summary>
        /// 店舗の種類
        /// </summary>
        [Required]
        public ShopType? ShopType { get; set; } = null!;

        /// <summary>
        /// コメント
        /// </summary>
        public string Comment { get; set; } = null!;

        /// <summary>
        /// 営業開始時間
        /// </summary>
        public TimeOnly StartTime { get; set; }

        /// <summary>
        /// 営業終了時間
        /// </summary>
        public TimeOnly EndTime { get; set; }

        /// <summary>
        /// 定休日
        /// </summary>
        public ICollection<ShopRegularHoliday>? RegularHolidays { get; set; } = null!;

        /// <summary>
        /// 住所ID
        /// </summary>
        [ForeignKey(nameof(Address))]
        public int AddressId { get; set; }

        /// <summary>
        /// 住所
        /// </summary>
        public Address? Address { get; set; } = null!;

        /// <summary>
        /// 電話番号
        /// </summary>
        public string Tel { get; set; } = null!;

        /// <summary>
        /// 駐車場有
        /// </summary>
        public bool ExistsParking { get; set; }

        /// <summary>
        /// コインパーキングあり
        /// </summary>
        public bool ExistsCoinParking { get; set; }

        /// <summary>
        /// テイクアウト可能
        /// </summary>
        public bool EnableTakeout { get; set; }

        /// <summary>
        /// メモ
        /// </summary>
        public string Memo { get; set; } = null!;
    }
}
