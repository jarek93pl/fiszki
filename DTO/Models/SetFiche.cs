using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class SetFiche
    {
        [DisplayName("Nazwa zestawu")]
        public string Name { get; set; }

        [DisplayName("Nazwa urzytkownika")]
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
    }
}
