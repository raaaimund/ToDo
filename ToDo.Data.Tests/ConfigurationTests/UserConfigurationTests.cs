using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ToDo.Dto;
using Xunit;

namespace ToDo.Data.Tests.ConfigurationTests
{
    public class UserConfigurationTests : TestWithSqlite
    {
        [Fact]
        public void TableShouldGetCreated()
        {
            Assert.False(DbContext.User.Any());
        }

        [Fact]
        public void UsernameIsRequired()
        {
            var newUser = new User() {PasswordHash = "passwordhash"};
            DbContext.User.Add(newUser);

            Assert.Throws<DbUpdateException>(() => DbContext.SaveChanges());
        }

        [Fact]
        public void PasswordIsRequired()
        {
            var newUser = new User() {Username = "username"};
            DbContext.User.Add(newUser);

            Assert.Throws<DbUpdateException>(() => DbContext.SaveChanges());
        }

        [Fact]
        public void AddedUserShouldGetGeneratedId()
        {
            var newUser = new User() {Username = "username", PasswordHash = "passwordhash"};
            DbContext.User.Add(newUser);
            DbContext.SaveChanges();

            Assert.NotEqual(Guid.Empty, newUser.Id);
        }

        [Fact]
        public void AddedUserShouldGetPersisted()
        {
            var newUser = new User() {Username = "username", PasswordHash = "passwordhash"};
            DbContext.User.Add(newUser);
            DbContext.SaveChanges();

            Assert.Equal(newUser, DbContext.User.Find(newUser.Id));
            Assert.Equal(1, DbContext.User.Count());
        }
    }
}