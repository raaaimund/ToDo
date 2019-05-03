using System.ComponentModel.DataAnnotations;

namespace ToDo.Web.Models.ToDo
{
    public class CreateViewModel
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
    }
}
