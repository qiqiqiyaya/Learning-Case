using System.ComponentModel.DataAnnotations;

namespace Customize_Router.Data
{
    public class Api
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Descripetion { get; set; }
    }
}
