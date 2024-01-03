using Domain.Entities;
using FluentAssertions;

namespace Tests;

public class TunLabelBarcodeTests
{
  private const string TestBatchNumber = "A1234-2_1";
  private const string TestParentBatchNumber = "A1234";
  private const string TestExpiryDateString = "260430";
  private DateTime TestCurrentDate = new DateTime(2024, 01, 03);

  private string GetBarcode(string expiryDate, string parentBatchNumber)
  {
    return $"010930080735468117{expiryDate}10{parentBatchNumber}";
  }

  [Fact]
  public void Constructor_BatchNumberAndCurrentDate()
  {
    var expectedBatchNumber = TestBatchNumber;
    var expectedCurrentDate = TestCurrentDate;

    var actual = new TunLabelBarcode(TestBatchNumber, TestCurrentDate);

    actual.BatchNumber.Should().Be(expectedBatchNumber);
    actual.CurrentDate.Should().Be(expectedCurrentDate);
  }

  [Theory]
  [InlineData("A1234-2_1", "A1234")]
  [InlineData("1234-1_2", "1234-")]
  [InlineData("A123", "A123")]
  public void ParentBatchNumber_FirstFiveCharsOfBatchNumber(string testBatchNumber, string expected)
  {
    var actual = new TunLabelBarcode(testBatchNumber, TestCurrentDate);

    actual.ParentBatchNumber.Should().Be(expected);
  }

  [Fact]
  public void ScanBarcode_Null_ThrowsException()
  {
    var testBarcode = null as string;
    var sut = new TunLabelBarcode(TestBatchNumber, TestCurrentDate);

    Assert.Throws<InvalidDataException>(() => sut.ScanBarcode(testBarcode));
  }

  [Fact]
  public void ScanBarcode_NOTCorrectLength_ThrowsException()
  {
    var testBarcode = "123456789012345678901234567890";
    var sut = new TunLabelBarcode(TestBatchNumber, TestCurrentDate);

    Assert.Throws<InvalidDataException>(() => sut.ScanBarcode(testBarcode));
  }

  [Fact]
  public void ScanBarcode_CorrectLength()
  {
    var testBarcode = "1234567890123456789012345678901";
    var sut = new TunLabelBarcode(TestBatchNumber, TestCurrentDate);
    var expected = testBarcode;

    sut.ScanBarcode(testBarcode);

    sut.Barcode.Should().Be(expected);
  }

  [Fact]
  public void ScanBarcode_CorrectLengthTrimmed()
  {
    var testBarcode = " 1234567890123456789012345678901  ";
    var sut = new TunLabelBarcode(TestBatchNumber, TestCurrentDate);
    var expected = testBarcode.Trim();

    sut.ScanBarcode(testBarcode);

    sut.Barcode.Should().Be(expected);
  }

  [Theory]
  [InlineData(null, false)]
  [InlineData("", false)]
  [InlineData("1234567890", false)]
  [InlineData("1234567890123456789012345678901", true)]
  [InlineData("12345678901234567890123456789012", false)]
  public void IsBarcodeScanned(string testBarcode, bool expected)
  {
    var sut = new TunLabelBarcode(TestBatchNumber, TestCurrentDate);
    sut.IsBarcodeScanned.Should().BeFalse();

    try
    {
      sut.ScanBarcode(testBarcode);
    }
    catch (Exception)
    { }

    var actual = sut.IsBarcodeScanned;
    actual.Should().Be(expected);
  }

  [Fact]
  public void BarcodeExpiryDate_WhenNoBarcode_EmptyString()
  {
    var sut = new TunLabelBarcode(TestBatchNumber, TestCurrentDate);
    var expected = string.Empty;

    var actual = sut.BarcodeExpiryDateString;

    actual.Should().Be(expected);
  }

  [Fact]
  public void BarcodeExpiryDateString_ValidBarcode_ExtractYYMMDDdate()
  {
    var testExpiryDate = "260430";
    var testParentBatchNumber = "A1234";
    var testBarcode = GetBarcode(testExpiryDate, testParentBatchNumber);
    var sut = new TunLabelBarcode(TestBatchNumber, TestCurrentDate);
    sut.ScanBarcode(testBarcode);
    var expected = testExpiryDate;

    var actual = sut.BarcodeExpiryDateString;

    actual.Should().Be(expected);
  }

  [Fact]
  public void BarcodeExpiryDateString_NoScannedBarcode_EmptyString()
  {
    var sut = new TunLabelBarcode(TestBatchNumber, TestCurrentDate);
    var expected = string.Empty;

    var actual = sut.BarcodeExpiryDateString;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData("260430", 2026, 4, 30)]
  public void BarcodeExpiryDate(string testDateString, int expectedYear, int expectedMonth, int expectedDay)
{
  var testBarcode = GetBarcode(testDateString, TestParentBatchNumber);
  var sut = new TunLabelBarcode(TestBatchNumber, TestCurrentDate);
  sut.ScanBarcode(testBarcode);
  var expectedDateTime = new DateTime(expectedYear, expectedMonth, expectedDay);

  var actual = sut.BarcodeExpiryDate;

  actual.Should().Be(expectedDateTime);
}  

  [Fact]
  public void BarcodeParentBatchNumber_ValidBarcode_ExtractParentBatchNumber()
  {
    var testExpiryDate = "260430";
    var testParentBatchNumber = "A1234";
    var testBarcode = GetBarcode(testExpiryDate, testParentBatchNumber);
    var sut = new TunLabelBarcode(TestBatchNumber, TestCurrentDate);
    sut.ScanBarcode(testBarcode);
    var expected = testExpiryDate;

    var actual = sut.BarcodeExpiryDateString;

    actual.Should().Be(expected);
  }

  [Fact]
  public void BarcodeParentBatchNumber_NotScannedBarcode_EmptyString()
  {
    var sut = new TunLabelBarcode(TestBatchNumber, TestCurrentDate);
    var expected = string.Empty;

    var actual = sut.BarcodeExpiryDateString;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData("260430", 2024, 1, 3, true)]
  [InlineData("240103", 2024, 1, 3, true)]
  [InlineData("240102", 2024, 1, 3, false)]
  public void IsMaterialCurrent(string testExpiryDate, int testDateYear, int testDateMonth, int testDateDay, bool expected)
  {
    var testCurrentDate = new DateTime(testDateYear, testDateMonth, testDateDay);
    var testBarcode = GetBarcode(testExpiryDate, TestParentBatchNumber);
    var sut = new TunLabelBarcode(TestBatchNumber, testCurrentDate);
    sut.ScanBarcode(testBarcode);

    var actual = sut.IsMaterialCurrent;

    actual.Should().Be(expected);
  }

  [Theory]
  [InlineData("A1234-2_1", "A1234", true)]
  public void IsMatchingBatchNumber(string testBatchNumber, string testParentBatchNumber, bool expected)
  {
    var testBarcode = GetBarcode(TestExpiryDateString, testParentBatchNumber);
    var sut = new TunLabelBarcode(testBatchNumber, TestCurrentDate);
    sut.ScanBarcode(testBarcode);

    var actual = sut.IsMatchingBatchNumber;

    actual.Should().Be(expected);
  }
}