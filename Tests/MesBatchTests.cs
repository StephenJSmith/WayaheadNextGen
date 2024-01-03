using FluentAssertions;

namespace Tests;

public class MesBatchTests
{
  private static int GetMaxChildBatchNumber(List<string> batchNumbers)
  {
    if (batchNumbers == null || !batchNumbers.Any()) return 0;

    var validBatches = batchNumbers.Where(x => x.LastIndexOf('_') > 0 
          && int.TryParse(x.Substring(x.LastIndexOf('_') + 1), out int result))
        .ToList();
    if (!validBatches.Any()) return 0;

    return validBatches
      .Select(x => Convert.ToInt32(x.Substring(x.LastIndexOf('_') + 1)))
      .Max();
  }

  private static int GetNextAvailableChildBatchSeq(List<string> batchNumbers)
  {
    return GetMaxChildBatchNumber(batchNumbers) + 1;
  }

  [Fact]
  public void GetNextAvailableChildBatchSeq_UnorderedList_CorrectNextValue()
  {
    var batches = new List<string> {
      "A1234-2_3",
      "A1234-2_2",
      "A1234-2_1",
      "A1234-2_6",
      "A1234-2_5",
      "A1234-2_4",
    };
    var expected = 7;

    var actual = GetNextAvailableChildBatchSeq(batches);

    actual.Should().Be(expected);
  }

  [Fact]
  public void GetNextAvailableChildBatchSeq_EmptyList_Returns1()
  {
    var batches = new List<string> ();
    var expected = 1;

    var actual = GetNextAvailableChildBatchSeq(batches);

    actual.Should().Be(expected);
  }

  [Fact]
  public void GetNextAvailableChildBatchSeq_NoNumericValuesinList_Returns1()
  {
    var batches = new List<string> {
      "A1234-2_A",
      "A1234-2_B",
      "A1234-2_C",
      "A1234-2_D",
      "A1234-2_E",
      "A1234-2_F",
    };
    var expected = 1;

    var actual = GetNextAvailableChildBatchSeq(batches);

    actual.Should().Be(expected);
  }

  [Fact]
  public void GetNextAvailableChildBatchSeq_UnorderedListMoreThan10_CorrectNextValue()
  {
    var batches = new List<string> {
      "A1234-2_3",
      "A1234-2_2",
      "A1234-2_1",
      "A1234-2_6",
      "A1234-2_5",
      "A1234-2_4",
      "A1234-2_11",
      "A1234-2_10",
      "A1234-2_8",
      "A1234-2_9",
      "A1234-2_7",
    };
    var expected = 12;
    
    var actual = GetNextAvailableChildBatchSeq(batches);

    actual.Should().Be(expected);
  }

  [Fact]
  public void GetNextAvailableChildBatchSeq_ListIncludeNoChildValues_CorrectNextValue()
  {
    var batches = new List<string> {
      "A1234-2_3",
      "A1234-2_2",
      "A1234-2_1",
      "A1234-2",
      "A1234-2_6",
      "A1234-2",
      "A1234-2_5",
      "A1234-2_4",
    };
    var expected = 7;
    
    var actual = GetNextAvailableChildBatchSeq(batches);

    actual.Should().Be(expected);
  }

  [Fact]
  public void GetNextAvailableChildBatchSeq_ListIncludeNonNumericValues_CorrectNextValue()
  {
    var batches = new List<string> {
      "A1234-2_3",
      "A1234-2_2",
      "A1234-2_1",
      "A1234-2_X",
      "A1234-2_6",
      "A1234-2_Y",
      "A1234-2_5",
      "A1234-2_4",
      "A1234-2_Z",
    };
    var expected = 7;
    
    var actual = GetNextAvailableChildBatchSeq(batches);

    actual.Should().Be(expected);
  }

}