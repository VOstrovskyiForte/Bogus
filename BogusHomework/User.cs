using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BogusHomework
{
    public class User
    {
        public string id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string company { get; set; }
        public string password { get; set; }

        //MakePasswordValid is the method from MethodsFramework.cs that is needed to
        // make sure that password contains all necessary characters
        public static User GenerateUser()
        {
            return new Faker<User>()
                .StrictMode(false)
                .RuleFor(o => o.name, f => f.Name.FirstName())
                .RuleFor(o => o.surname, f => f.Name.LastName())
                .RuleFor(o => o.email, (f, o) => f.Internet.Email(o.name, o.surname))
                .RuleFor(o => o.company, f => f.Company.CompanyName())
                .RuleFor(o => o.password, f => f.Internet.Password().MakePasswordValid())
                .Generate();

            //return this;
        }

    }
}
