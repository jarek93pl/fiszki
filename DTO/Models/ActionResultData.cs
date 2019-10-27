using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Models
{
    public class ActionResultData
    {
        public bool IsSuccess { get; set; }
        public static explicit operator ActionResultData(bool IsSuccess)
        {
            return new ActionResultData() { IsSuccess = IsSuccess };
        }
        public static explicit operator bool(ActionResultData data)
        {
            return data.IsSuccess;
        }
    }
}
