using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class ChangeLog<T>
    {
        public int EndIndex { get; set; }
        public List<T> ChangeLogs = new List<T>();
    }
}
