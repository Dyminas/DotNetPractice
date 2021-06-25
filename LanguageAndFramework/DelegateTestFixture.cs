using NUnit.Framework;
using System;

namespace LanguageAndFramework
{
    [TestFixture]
    public class DelegateTestFixture
    {
        [Test]
        public void TestDelegate()
        {
            StringAction sa1 = new StringAction(ActionOnString);
            StringAction sa2 = new StringAction(ActionOnObject); //参数逆变(in)

            ObjectAction oa1 = new ObjectAction(ActionOnObject);
            //ObjectAction oa2 = new ObjectAction(ActionOnString); //编译错误

            ObjectRetriever or = new ObjectRetriever(RetrieveString); //返回值协变(out)
            //StringRetriever sr = new StringRetriever(RetrieveObject); //编译错误
        }

        delegate void StringAction(string s);
        delegate void ObjectAction(object o);
        void ActionOnString(string s) => Console.WriteLine(s);
        void ActionOnObject(object o) => Console.WriteLine(o);

        delegate object ObjectRetriever();
        delegate string StringRetriever();
        object RetrieveObject() => "Object";
        string RetrieveString() => "String";
    }
}