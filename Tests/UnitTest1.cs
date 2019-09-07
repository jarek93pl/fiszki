using System;
using Conector;
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
                set.AddSetFiche("dana",1);
            }
            
        }
    }
}
