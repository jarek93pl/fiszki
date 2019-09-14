using DTO.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaukaFiszek.Helper
{
    public static class ComonFunctionalities
    {
        public static string RenderToFile(FileType fileType, string Id)
        {
            string returned = $"< input type = \"hidden\" id = \"{Id}_value\" /> ";
            returned += $"< input type = \"file\" id = \"{Id}_file\" />";
            returned += $"< input type = \"button\" id = \"{Id}_button\" value = \"Wyślij plik\" data-theme=\"{fileType}\" onclick=\"UploadFile()\" />";
            return returned;
        }
    }
}