using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.AddRange(
                    new Author{FirstName = "Fyodor",LastName = "Dostoyevski",DateOfBirth = new DateTime(1821, 11, 11)},
                    new Author{FirstName = "Jean Paul",LastName = "Sartre",DateOfBirth = new DateTime(1905, 06, 21)},
                    new Author{FirstName = "Albert",LastName = "Camus",DateOfBirth = new DateTime(1913, 11, 07)},
                    new Author{FirstName = "Enes",LastName = "Arat",DateOfBirth = new DateTime(1999, 07, 07)}
                );
        }
    }
}