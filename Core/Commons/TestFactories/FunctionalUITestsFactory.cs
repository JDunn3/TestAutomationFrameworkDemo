using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomationFrameworkDemo.Core.Commons.TestObjects;

namespace TestAutomationFrameworkDemo.Core.Commons.TestFactories
{
    public class FunctionalUITestsFactory : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new object[] { new FunctionalUITest() };
        }
    }
}
