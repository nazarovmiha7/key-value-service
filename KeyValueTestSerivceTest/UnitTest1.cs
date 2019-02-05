using System;
using Xunit;

using KeyValueService;

namespace KeyValueTestSerivceTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string value = @"текст для проверки. ЕЩЕ одно предложение.";

            var formattingValue = value.SentencesFormatting();

            Assert.Equal("Текст для проверки. Еще одно предложение.", formattingValue);
        }

        [Fact]
        public void Test2()
        {
            string value = @"текст для проверки.ЕЩЕ одно предложение.ЕЩЕ ОДНО";

            var formattingValue = value.SentencesFormatting();

            Assert.Equal("Текст для проверки. Еще одно предложение. Еще одно.", formattingValue);
        }

        [Fact]
        public void Test3()
        {
            string value = @"  текст для проверки";

            var formattingValue = value.SentencesFormatting();

            Assert.Equal("Текст для проверки.", formattingValue);
        }

        [Fact]
        public void Test4()
        {
            string value = @"  ";

            var formattingValue = value.SentencesFormatting();

            Assert.Equal("", formattingValue);
        }

        [Fact]
        public void Test5()
        {
            string value = null;

            var formattingValue = value.SentencesFormatting();

            Assert.Null(formattingValue);
        }
    }
}
