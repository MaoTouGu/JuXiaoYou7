namespace MaoTouGu.Foundation.Tests
{
    public class ClassStaticInstance
    {
        public string DumpString() => nameof(DumpString);
    }
    
    [TestClass]
    public sealed class ClassStaticUnitTest
    {
        [TestMethod]
        public void Should_CreateInstance_Generic()
        {
            var i = ClassStatic.CreateInstance<ClassStaticInstance>();
            var s = i.DumpString();

            Assert.AreEqual(nameof(ClassStaticInstance.DumpString), s);
        }
    }
}