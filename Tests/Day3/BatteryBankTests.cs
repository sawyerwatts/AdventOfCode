using AdventOfCode2025.Day3;

namespace AdventOfCode2025.Tests.Day3;

public class BatteryBankTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("0")]
    [InlineData("02")]
    [InlineData("20")]
    [InlineData("2!1")]
    public void BadInput(string? input)
    {
        Assert.Throws<ArgumentException>(() => new BatteryBank(input!));
    }

    [Fact]
    public void GoodInput()
    {
        var bank = new BatteryBank("123456789");
        Assert.Equal("123456789", bank.ToString());
    }

    [Theory]
    [InlineData("54", 54)]
    [InlineData("154", 54)]
    [InlineData("541", 54)]
    [InlineData("1289", 89)]
    [InlineData("12398", 98)]
    [InlineData("12787", 87)]
    [InlineData("98242", 98)]
    [InlineData("73689", 89)]
    [InlineData("73629", 79)]
    [InlineData("93627", 97)]
    [InlineData("93629", 99)]
    [InlineData("93921", 99)]
    [InlineData("13929", 99)]
    [InlineData("11111", 11)]
    public void TestJoltageOfTwoBatteries(string input, int expected)
    {
        var bank = new BatteryBank(input);
        var maxJoltage = bank.MaxJoltage(2);
        Assert.Equal(expected, maxJoltage);
    }

    [Theory]
    [InlineData("938", 938)]
    [InlineData("9381", 981)]
    [InlineData("93821", 982)]
    [InlineData("13829", 389)]
    [InlineData("83828", 888)]
    [InlineData("31427596879", 989)]
    public void TestJoltageOfThreeBatteries(string input, int expected)
    {
        var bank = new BatteryBank(input);
        var maxJoltage = bank.MaxJoltageAlt(3);
        Assert.Equal(expected, maxJoltage);
    }
}