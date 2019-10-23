using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class WaitingForPlayerData
    {
        [DisplayName("Guid gry :")]
        public string GuidGame { get; set; }
        public bool UserCanStart { get; set; }
    }
}
