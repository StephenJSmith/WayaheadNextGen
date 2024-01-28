namespace Domain.Entities;

public class TunLabelBarcodeScanner
{
  public const int ParentBatchNumberLength = 5;
  public const int BarcodeLength = 31;
  public const int BarcodeDateIndex = 18;
  public const int BarcodeParentBatchIndex = 26;
  public const string DateFormat = "yyMMdd";
  public string Barcode { get; } = string.Empty;
  public string BatchNumber { get; } = string.Empty;
  public DateTime DispensedDate { get; }
  public int ShelfLifeDays { get; } = 0;

  public bool IsValidBarcodeLength => Barcode.Length == BarcodeLength;

  public string BarcodeUseByDate
  {
    get 
    {
      if (!IsValidBarcodeLength) return string.Empty;

      return Barcode.Substring(BarcodeDateIndex, DateFormat.Length);
    }
  }

  public string BarcodeParentBatchNumber
  {
    get
    {
      if (!IsValidBarcodeLength) return string.Empty;

      return Barcode.Substring(BarcodeParentBatchIndex, ParentBatchNumberLength);
    }
  }

  public string FormattedUseByDate
  {
    get{
      if (!IsValidBarcodeLength) return string.Empty;

      var formatted = DispensedDate
        .AddDays(ShelfLifeDays)
        .ToString(DateFormat);

      return formatted;
    }
  }

  public string ParentBatchNumber 
  {
    get
    {
      var length = Math.Min(ParentBatchNumberLength, BatchNumber.Length);

      return BatchNumber[..length];
    }
  }
  public bool IsValidUseByDate => 
    IsValidBarcodeLength && BarcodeUseByDate == FormattedUseByDate;

  public bool IsValidParentBatchNumber => 
    IsValidBarcodeLength && BarcodeParentBatchNumber == ParentBatchNumber;

  private TunLabelBarcodeScanner(
    string barcode, 
    string batchNumber, 
    DateTime dispensedDate, 
    int shelfLifeDays)
  {
    Barcode = barcode;
    BatchNumber = batchNumber;
    DispensedDate = dispensedDate;
    ShelfLifeDays = shelfLifeDays;
  }

  public static TunLabelBarcodeScanner Scan(
    string barcode, 
    string batchNumber, 
    DateTime dispensedDate, 
    int shelfLifeDays)
  {
    return new TunLabelBarcodeScanner(
      barcode, 
      batchNumber, 
      dispensedDate, 
      shelfLifeDays);
  }

  public static string GetCompressionBatchNumber(string coatingBatchNumber) {
    return coatingBatchNumber.Replace("-2", "-3");
  }
}