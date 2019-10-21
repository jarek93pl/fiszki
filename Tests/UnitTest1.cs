using System;
using System.IO;
using Conector;
using DTO.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            NaukaFiszek.Controllers.ComonController con = new NaukaFiszek.Controllers.ComonController();
            //con.SaveFile(new NaukaFiszek.Models.FileData() { Type = FileType.PromptContent, DataFile = File.ReadAllBytes("tx.txt"), Extension = "txt" });

        }
        [TestMethod]
        public void LoadExtenison()
        {
            NaukaFiszek.Controllers.ComonController con = new NaukaFiszek.Controllers.ComonController();
            var file = con.LoadFile(3);

        }
        [TestMethod]
        public void LoadNextFiche()
        {
            using (Conector.Game g = new Conector.Game())
            {
                var data = g.NextFiche(15);
            }

        }

        [TestMethod]
        public void SendAnswer()
        {
            using (Game set = new Game())
            {
                set.SendAnswer(15, 1005, true);
            }

        }

    }
}

