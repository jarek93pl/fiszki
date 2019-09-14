using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaukaFiszek.Models
{
    public class FileData
    {
        public byte[] DataFile { get; set; }
        public string DataFileValue
        {
            get
            {
                return Convert.ToBase64String(DataFile ?? new byte[0]);
            }
            set
            {
                DataFile = Convert.FromBase64String(value);
            }
        }

        public FileType Type { get; set; }
        public int TypeValue
        {
            get
            {
                return (int)Type;
            }
            set
            {
                Type = (FileType)value;
            }
        }
        public string Extension { get; set; }
    }
}