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
            string value = @"����� ��� ��������. ��� ���� �����������.";

            var formattingValue = value.SentencesFormatting();

            Assert.Equal("����� ��� ��������. ��� ���� �����������.", formattingValue);
        }

        [Fact]
        public void Test2()
        {
            string value = @"����� ��� ��������.��� ���� �����������.��� ����";

            var formattingValue = value.SentencesFormatting();

            Assert.Equal("����� ��� ��������. ��� ���� �����������. ��� ����.", formattingValue);
        }

        [Fact]
        public void Test3()
        {
            string value = @"  ����� ��� ��������";

            var formattingValue = value.SentencesFormatting();

            Assert.Equal("����� ��� ��������.", formattingValue);
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
