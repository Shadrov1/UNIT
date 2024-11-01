using Moq;

namespace UNITtests1
{
    public class UnitTest1
    {
        private readonly Mock<ILoggingService> _mockLoggingService;
        private readonly Mock<ICurrencyConverter> _mockCurrencyConverter;
        private readonly Calculator _calculator;

        public UnitTest1()
        {
            _mockLoggingService = new Mock<ILoggingService>();
            _mockCurrencyConverter = new Mock<ICurrencyConverter>();
            _calculator = new Calculator(_mockLoggingService.Object, _mockCurrencyConverter.Object);
        }

        // 1. ���� ���������
        [Fact]
        public void TestMultiply_ValidInputs_ShouldReturnCorrectResult()
        {
            double value1 = 5;
            double value2 = 4;
            double expected = 20;

            double result = _calculator.Multiply(value1, value2);

            Assert.Equal(expected, result);
            _mockLoggingService.Verify(x => x.Log(It.IsAny<string>()), Times.Once);
        }

        // 2. ���� ��������� � ������� �������� �������
        [Theory]
        [InlineData(3.75, 4, 15)]
        [InlineData(7.5, 0.2, 1.5)]
        [InlineData(10, 0, 0)]
        [InlineData(-2, 5, -10)]
        public void TestMultiply_MultipleInputs_ShouldReturnCorrectResult(double value1, double value2, double expected)
        {
            double result = _calculator.Multiply(value1, value2);

            Assert.Equal(expected, result);
            _mockLoggingService.Verify(x => x.Log(It.IsAny<string>()), Times.Once);
        }

        // 3. ���� ��������
        [Fact]
        public void TestAdd_ValidInputs_ShouldReturnCorrectResult()
        {
            double value1 = 15;
            double value2 = 10;
            double expected = 25;

            double result = _calculator.Add(value1, value2);

            Assert.Equal(expected, result);
            _mockLoggingService.Verify(x => x.Log(It.IsAny<string>()), Times.Once);
        }

        // 4. ���� �������� � ������� �������� �������
        [Theory]
        [InlineData(10, 5, 15)]
        [InlineData(-5, 5, 0)]
        [InlineData(0, 0, 0)]
        public void TestAdd_MultipleInputs_ShouldReturnCorrectResult(double value1, double value2, double expected)
        {
            double result = _calculator.Add(value1, value2);

            Assert.Equal(expected, result);
            _mockLoggingService.Verify(x => x.Log(It.IsAny<string>()), Times.Once);
        }

        // 5. ���� ���������� ���������� �������
        [Fact]
        public void TestRoundOff_ResultRequiresRounding_ShouldReturnRoundedResult()
        {
            double value1 = 10.255;
            double value2 = 3;
            double expected = 3.42;
            double result = _calculator.Divide(value1, value2);

            Assert.Equal(expected, Math.Round(result, 2));
            _mockLoggingService.Verify(x => x.Log(It.IsAny<string>()), Times.Once);
        }

        // 6. ���� ����������� � ���������
        [Fact]
        public void TestConvertAndMultiply_ValidInputs_ShouldReturnCorrectResult()
        {
            double amount = 50;
            string fromCurrency = "USD";
            string toCurrency = "GBP";
            double convertedAmount = 40;
            double multiplier = 2;
            double expected = 80;

            _mockCurrencyConverter.Setup(x => x.Convert(amount, fromCurrency, toCurrency)).Returns(convertedAmount);

            double result = _calculator.ConvertAndMultiply(amount, fromCurrency, toCurrency, multiplier);

            Assert.Equal(expected, result);
            _mockCurrencyConverter.Verify(x => x.Convert(amount, fromCurrency, toCurrency), Times.Once);
            _mockLoggingService.Verify(x => x.Log(It.IsAny<string>()), Times.Once);
        }
    }
}