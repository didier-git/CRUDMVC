using System.ComponentModel.DataAnnotations;

namespace CRUDMVC.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = " Debe Colocar una Descripcion")]
        [StringLength(100, ErrorMessage = "El campo no cumple con los caracteres requeridos")]
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "Debe de fijar una fecha")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Compra")]
        public DateTime FechaDeCompra { get; set; }

        [Required(ErrorMessage = "Debe introducir un monto")]
        public Decimal Monto { get; set; }

        [Required(ErrorMessage ="Debe indicar en que lugar realizó la compra")]
        [Display(Name ="Lugar de Compra")]
        public string? LugarDeCompra { get; set; }


    }
}
