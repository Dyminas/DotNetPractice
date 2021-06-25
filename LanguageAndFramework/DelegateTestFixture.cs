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
            StringAction sa2 = new StringAction(ActionOnObject); //�������(in)

            ObjectAction oa1 = new ObjectAction(ActionOnObject);
            //ObjectAction oa2 = new ObjectAction(ActionOnString); //�������

            ObjectRetriever or = new ObjectRetriever(RetrieveString); //����ֵЭ��(out)
            //StringRetriever sr = new StringRetriever(RetrieveObject); //�������
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