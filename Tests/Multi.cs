using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class Multi
    {

        [TestMethod]
        public void ListPlayer()
        {
            global::NaukaFiszek.Logic.MultiPlayer.ListPlayer<string> listplayer = new NaukaFiszek.Logic.MultiPlayer.ListPlayer<string>();
            listplayer.RegisterByString("st1");
            listplayer.RegisterByString("st2");
            listplayer.UnRegisterByString("st2");
            listplayer.RegisterByString("st3");
            var arrey = listplayer.ToArray();
            Assert.IsTrue(arrey[0] == "st1");
            Assert.IsTrue(arrey[1] == "st3");

        }
        [TestMethod]
        public void ListPlayerChangeStart0()
        {
            global::NaukaFiszek.Logic.MultiPlayer.ListPlayer<string> listplayer = new NaukaFiszek.Logic.MultiPlayer.ListPlayer<string>();
            listplayer.RegisterByString("st1");
            listplayer.RegisterByString("st2");
            listplayer.UnRegisterByString("st2");
            listplayer.RegisterByString("st3");
            var change = listplayer.ChangeLogs();
            Assert.IsTrue(change.ChangeLogs[0].Login == "st1" && change.ChangeLogs[0].Status == DTO.Enums.StatusChangedPlayerList.Register);
            Assert.IsTrue(change.ChangeLogs[1].Login == "st2" && change.ChangeLogs[1].Status == DTO.Enums.StatusChangedPlayerList.Register);
            Assert.IsTrue(change.ChangeLogs[2].Login == "st2" && change.ChangeLogs[2].Status == DTO.Enums.StatusChangedPlayerList.Leave);
            Assert.IsTrue(change.ChangeLogs[3].Login == "st3" && change.ChangeLogs[3].Status == DTO.Enums.StatusChangedPlayerList.Register);

        }
        [TestMethod]
        public void ListPlayerChangeStart1()
        {
            global::NaukaFiszek.Logic.MultiPlayer.ListPlayer<string> listplayer = new NaukaFiszek.Logic.MultiPlayer.ListPlayer<string>();
            listplayer.RegisterByString("st2");
            listplayer.UnRegisterByString("st2");
            listplayer.RegisterByString("st3");
            var change = listplayer.ChangeLogs();
            Assert.IsTrue(change.ChangeLogs[0].Login == "st2" && change.ChangeLogs[0].Status == DTO.Enums.StatusChangedPlayerList.Register);
            Assert.IsTrue(change.ChangeLogs[1].Login == "st2" && change.ChangeLogs[1].Status == DTO.Enums.StatusChangedPlayerList.Leave);
            Assert.IsTrue(change.ChangeLogs[2].Login == "st3" && change.ChangeLogs[2].Status == DTO.Enums.StatusChangedPlayerList.Register);

        }
    }
}
