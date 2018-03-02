using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomationFrameworkDemo.Core.Commons.TestFactories;
using TestAutomationFrameworkDemo.Core.Commons.TestObjects;
using TopTal.Commons.TestData;
using tddb = TopTal.Commons.TestData.TestDataDatabase;

namespace TopTal.Commons.TestLibraries
{
    [TestFixture]
    class FunctionalUITests
    {
        [TestCaseSource(typeof(FunctionalUITestsFactory))]
        public void SubmitCustomerCareQuestion(FunctionalUITest uiTest)
        {
            tddb.init();
            tddb.Queries.Testing();
        }
    }
}
