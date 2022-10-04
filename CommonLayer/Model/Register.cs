using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Model
{
    public class Register
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}
