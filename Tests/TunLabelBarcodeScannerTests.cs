using Domain.Entities;
using FluentAssertions;

namespace Tests;

public class TunLabelBarcodeScannerTests
{
  private const string TestBatchNumber = "A1234-2_1";
  private const string TestParentBatchNumber = "A1234";
  private const string TestFormattedUseByDate = "250517";
  private const string TestBarcode = "01093008073546811725051710A1234";
  private DateTime TestDispensedDate = new DateTime(2024, 01, 03);
  private const int TestShelfLifeDays = 500;
  private const string DateFormat = "yyMMdd";
  

  [Fact]
  public void Scan_ReturnsTunLabelBarcodeScannerObjectWithEnteredProperties()
  {
    var actual = TunLabelBarcodeScanner.Scan(TestBarcode, TestBatchNumber, TestDispensedDate, TestShelfLifeDays);

    actual.Should().NotBeNull();
    actual.Barcode.Should().Be(TestBarcode);
    actual.BatchNumber.Should().Be(TestBatchNumber);
    actual.DispensedDate.Should().Be(TestDispensedDate);
    actual.ShelfLifeDays.Should().Be(TestShelfLifeDays);
  }

  [Theory]
  [InlineData("", false)]
  [InlineData("123456789012345678901234567890", false)]
  [InlineData("1234567890123456789012345678901", true)]
  [InlineData("12345678901234567890123456789012", false)]
  public void IsValidBarcodeLength(string testBarcode, bool expected)
  {
    var sut = TunLabelBarcodeScanner.Scan(testBarcode, TestBatchNumber, TestDispensedDate, TestShelfLifeDays);

    var actual = sut.IsValidBarcodeLength;

    actual.Should().Be(expected);
  }

  [Fact]
  public void BarcodeUseByDate_ExtractFromBarcode()
  {
    var expected = TestDispensedDate
      .AddDays(TestShelfLifeDays)
      .ToString(DateFormat);
    var sut = TunLabelBarcodeScanner.Scan(TestBarcode, TestBatchNumber, TestDispensedDate, TestShelfLifeDays);

    var actual = sut.BarcodeUseByDate;

    actual.Should().Be(expected);
  }

  [Fact]
  public void BardcodeParentBatchNumber_ExtractFromBarcode()
  {
    var expected = TestParentBatchNumber;
    var sut = TunLabelBarcodeScanner.Scan(TestBarcode, TestBatchNumber, TestDispensedDate, TestShelfLifeDays);

    var actual = sut.BarcodeParentBatchNumber;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData(2024, 1, 3, 500, "250517")]
  [InlineData(2023, 12, 25, 600, "250816")]
  public void FormattedUseByDate(int testYear, int testMonth, int testDay, int testShelfLifeDays, string expected)
  {
    var testDispensedDate = new DateTime(testYear, testMonth, testDay);
    var testBarcode = GetBarcode(expected, TestParentBatchNumber);
    var sut = TunLabelBarcodeScanner.Scan(testBarcode, TestBatchNumber, testDispensedDate, testShelfLifeDays);

    var actual = sut.FormattedUseByDate;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData("A1234-2_1", "A1234")]
  [InlineData("K434-2_1", "K434-")]
  public void ParentBatchNumber(string testBatchNumber, string expected)
  {
    var testBarcode = GetBarcode(TestFormattedUseByDate, expected);
    var sut = TunLabelBarcodeScanner.Scan(testBarcode, testBatchNumber, TestDispensedDate, TestShelfLifeDays);
    
    var actual = sut.ParentBatchNumber;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData("01093008073546811725051710A1234", true)]
  [InlineData("01093008073546811725051810A1234", false)]
  public void IsValidUseByDate(string testBarcode, bool expected)
  {
    var sut = TunLabelBarcodeScanner.Scan(testBarcode, TestBatchNumber, TestDispensedDate, TestShelfLifeDays);

    var actual = sut.IsValidUseByDate;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData("01093008073546811725051710A1234", true)]
  [InlineData("01093008073546811725051710A1233", false)]
  public void IsValidParentBatchNumber(string testBarcode, bool expected)
  {
    var sut = TunLabelBarcodeScanner.Scan(testBarcode, TestBatchNumber, TestDispensedDate, TestShelfLifeDays);

    var actual = sut.IsValidUseByDate;

    actual.Should().Be(expected);
  }

  private string GetBarcode(string formattedDate, string parentBatchNumber)
  {
    return $"010930080735468117{formattedDate}10{parentBatchNumber}";
  }
}