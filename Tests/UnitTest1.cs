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
            using (SetFiszka set = new SetFiszka())
            {
                set.AddSetFiszka("dana");
            }
            
        }
    }
}
