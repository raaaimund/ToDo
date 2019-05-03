using System;

namespace ToDo.Dto
{
    public class User : IDto<Guid>
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
