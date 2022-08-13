using System.ComponentModel.DataAnnotations;

namespace CRUDMVC.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Debe colocar su usuario")]
        public string UsuarioName { get; set; } = "";
        [Required(ErrorMessage ="Debe introducir su clave")]
        public string password { get; set; } = "";
    }
}
