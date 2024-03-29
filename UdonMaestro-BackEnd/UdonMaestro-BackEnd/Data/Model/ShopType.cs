﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UdonMaestro_BackEnd.Data.Model {
    /// <summary>
    /// 店のタイプ
    /// </summary>
    [Index(nameof(Name))]
    public class ShopType {
        
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// タイプ名
        /// </summary>
        [Required]
        public string Name { get; set; } = null!;
    }
}
