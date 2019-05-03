using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Web.Models.ToDo
{
    public class UpdateViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name{ get; set; }
    }
}
