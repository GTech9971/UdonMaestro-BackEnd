using System.ComponentModel.DataAnnotations;

namespace UdonMaestro_BackEnd.Data.Model {
    public class Shop {

        [Key]
        [Required]
        public int Id { get; set; }

    }
}
