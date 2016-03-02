using LinkCrawler.Utils.Extensions;
using NUnit.Framework;

namespace LinkCrawler.Tests.UtilsTests.ExtensionsTests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        //MethodName_StateUnderTest_ExpectedBehaviour
        [Test]
        public void ToBool_StringValueIsTrue_BooleanValueIsTrue()
        {
            var stringValue = "true";
            var booleanValue = stringValue.ToBool();
            Assert.AreEqual(true, booleanValue);
        }

        [Test]
        public void ToBool_StringValueIsFalse_BooleanValueIsFalse()
        {
            var stringValue = "false";
            var booleanValue = stringValue.ToBool();
            Assert.AreEqual(false, booleanValue);
        }

        [Test]
        public void ToBool_StringValueIsFoobar_BooleanValueIsFalse()
        {
            var stringValue = "Foobar";
            var booleanValue = stringValue.ToBool();
            Assert.AreEqual(false, booleanValue);
        }

        [Test]
        public void ToBool_StringValueIsEmpty_BooleanValueIsFalse()
        {
            var stringValue = "";
            var booleanValue = stringValue.ToBool();
            Assert.AreEqual(false, booleanValue);
        }

        [Test]
        public void ToBool_StringValueIsNull_BooleanValueIsFalse()
        {
            string stringValue = null;
            var booleanValue = stringValue.ToBool();
            Assert.AreEqual(false, booleanValue);
        }

        [Test]
        public void StartsWithIgnoreCase_SameLetterAndSameCase_True()
        {
            var word = "Foobar";
            var letter = "F";
            var result = word.StartsWithIgnoreCase(letter);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void StartsWithIgnoreCase_SameLetterAndDifferentCase_True()
        {
            var word = "Foobar";
            var letter = "f";
            var result = word.StartsWithIgnoreCase(letter);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void StartsWithIgnoreCase_EmptyWord_False()
        {
            var word = "";
            var letter = "A";
            var result = word.StartsWithIgnoreCase(letter);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void StartsWithIgnoreCase_LetterIsBlankSpace_False()
        {
            var word = "Foobar";
            var letter = " ";
            var result = word.StartsWithIgnoreCase(letter);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void StartsWithIgnoreCase_LetterIsNull_False()
        {
            var word = "Foobar";
            string letter = null;
            var result = word.StartsWithIgnoreCase(letter);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void StartsWithIgnoreCase_WordIsNull_False()
        {
            string word = null;
            var letter = "F";
            var result = word.StartsWithIgnoreCase(letter);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void StartsWithIgnoreCase_DifferentLetterAndDifferentCase_True()
        {
            var word = "Foobar";
            var letter = "a";
            var result = word.StartsWithIgnoreCase(letter);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void StartsWithIgnoreCase_DifferentLetterAndSameCase_True()
        {
            var word = "Foobar";
            var letter = "A";
            var result = word.StartsWithIgnoreCase(letter);
            Assert.AreEqual(false, result);
        }
    }
}
