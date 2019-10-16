using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DTO.Models
{
    public class AuthorizationDetails
    {
        public string Login { get; set; }

        [DisplayName("Hasło")]
        public string Password { get; set; }
    }
}