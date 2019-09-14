using System;
using System.IO;
using Conector;
using DTO.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NaukaFiszek.Controllers;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (SetFiche set = new SetFiche())
            {
                set.AddSetFiche("dana", 1);
            }

        }


        [TestMethod]
        public void SaveFileTest()
        {
            ComonController con = new ComonController();
            var result = con.SaveFile(new NaukaFiszek.Models.FileData() { Type = FileType.PromptContent, DataFile = File.ReadAllBytes("tx.txt"), Extension = "txt" });

        }
    }
}
