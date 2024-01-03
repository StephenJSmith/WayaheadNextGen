namespace Domain.Entities;

public class TunLabelBarcode
{
  public const int ParentBatchNumberLength = 5;
  public const int BarcodeLength = 31;
  public const int BarcodeDateIndex = 18;
  public const int BarcodeParentBatchIndex = 26;
  public const string DateFormat = "yyMMdd";

  public string BatchNumber { get; private set; } = string.Empty;
  public DateTime CurrentDate { get; private set; }

  public string Barcode { get; private set; } = string.Empty;

  public bool IsBarcodeScanned => Barcode.Length == BarcodeLength;

  public string BarcodeExpiryDateString 
  {
    get {
      if (!IsBarcodeScanned) return string.Empty;

      return Barcode.Substring(BarcodeDateIndex, DateFormat.Length);
    }
  }

  public DateTime BarcodeExpiryDate {
    get {
      var yy = BarcodeExpiryDateString.Substring(0,2);
      var mm = BarcodeExpiryDateString.Substring(2, 2);
      var dd = BarcodeExpiryDateString.Substring(4, 2);
      var year = 2000 + int.Parse(yy);
      var month = int.Parse(mm);
      var day = int.Parse(dd);

      return new DateTime(year, month, day);
    }
  }

  public string BarcodeParentBatchNumber 
  {
    get {
      if (!IsBarcodeScanned) return string.Empty;

      return Barcode.Substring(BarcodeParentBatchIndex, DateFormat.Length);
    }
  }

  public string ParentBatchNumber {
    get {
      if (string.IsNullOrWhiteSpace(BatchNumber)) {
        return string.Empty;
      }

      var length = Math.Min(ParentBatchNumberLength, BatchNumber.Length);

      return BatchNumber[..length];
    }
  }

  public bool IsMaterialCurrent => IsBarcodeScanned && CurrentDate <= BarcodeExpiryDate;

  public bool IsMatchingBatchNumber => IsBarcodeScanned && ParentBatchNumber == BarcodeParentBatchNumber;

  public TunLabelBarcode(string batchNumber, DateTime currentDate)
  {
    BatchNumber = batchNumber;
    CurrentDate = currentDate;
  }

  public void ScanBarcode(string barcode) 
  {
    if (barcode == null || barcode.Trim().Length != BarcodeLength) {
      throw new InvalidDataException("Invalid barcode");
    }

    Barcode = barcode.Trim();
  }
}