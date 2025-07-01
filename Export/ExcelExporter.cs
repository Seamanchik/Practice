using ClosedXML.Excel;
using Practice.Models;

namespace Practice.Export
{
    public static class ExcelExporter
    {
        public static void ExportFilledBlanksByDocumentType(
     Dictionary<string, string> documentFileMap,
     List<Models.Blank> blanks,
     List<Box> boxes,
     List<Document> documents,
     List<Recipient> recipients)
        {
            foreach (var docEntry in documentFileMap)
            {
                string documentName = docEntry.Key;
                string filePath = docEntry.Value;

                var document = documents.FirstOrDefault(d => d.Name == documentName);
                if (document == null) continue;

                var relevantBoxes = boxes.Where(b => b.DocumentId == document.Id).ToList();
                var relevantBlanks = blanks
                    .Where(b => relevantBoxes.Any(box => box.Id == b.BoxId))
                    .Where(b => b.Date != null && !string.IsNullOrWhiteSpace(b.ProductName) && b.RecipientId != null)
                    .OrderBy(b => b.Date)
                    .ThenBy(b => b.BlankNumber)
                    .ToList();

                if (!relevantBlanks.Any()) continue;

                using var workbook = new XLWorkbook(filePath);
                var decodeSheet = workbook.Worksheet("Расшифровка карточки");
                var summarySheet = workbook.Worksheet("Карточка справка");

                if (decodeSheet == null || summarySheet == null)
                {
                    MessageBox.Show($"Не найден один из листов в файле {filePath}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                var titleCell = decodeSheet.CellsUsed()
                    .FirstOrDefault(c => c.GetValue<string>().Trim() == "Учет бланков строгой отчетности");
                if (titleCell != null)
                {
                    titleCell.Value = $"Учет бланков строгой отчетности {documentName}";
                    titleCell.Style.Font.FontName = "Arial Cyr";
                    titleCell.Style.Font.FontSize = 14;
                }

                var exportedKeys = new HashSet<string>();
                int lastRow = decodeSheet.LastRowUsed()?.RowNumber() ?? 1;
                int ppNumber = 1;

                for (int row = 2; row <= lastRow; row++)
                {
                    string? series = decodeSheet.Cell(row, 4).GetString();
                    string? number = decodeSheet.Cell(row, 5).GetString();

                    if (!string.IsNullOrWhiteSpace(series) && !string.IsNullOrWhiteSpace(number))
                    {
                        exportedKeys.Add($"{series}_{number}");

                        var cellValue = decodeSheet.Cell(row, 1).GetValue<string>();
                        if (int.TryParse(cellValue, out int existingPp))
                            ppNumber = Math.Max(ppNumber, existingPp + 1);
                    }
                }

                int currentRow = lastRow + 1;
                var usedBlanks = new List<Models.Blank>();

                foreach (var blank in relevantBlanks)
                {
                    var box = boxes.FirstOrDefault(b => b.Id == blank.BoxId);
                    var recipient = recipients.FirstOrDefault(r => r.Id == blank.RecipientId);
                    if (box == null || recipient == null) continue;

                    string key = $"{box.Series}_{blank.BlankNumber}";
                    if (exportedKeys.Contains(key)) continue;

                    decodeSheet.Cell(currentRow, 1).Value = ppNumber++;
                    decodeSheet.Cell(currentRow, 2).Value = documentName;
                    decodeSheet.Cell(currentRow, 3).Value = blank.Date.Value.ToString("dd.MM.yyyy");
                    decodeSheet.Cell(currentRow, 4).Value = box.Series;
                    decodeSheet.Cell(currentRow, 5).Value = blank.BlankNumber;
                    decodeSheet.Cell(currentRow, 6).Value = recipient.Name;
                    decodeSheet.Cell(currentRow, 7).Value = blank.ProductName;

                    currentRow++;
                    usedBlanks.Add(blank);
                }

                int summaryStartRow = 11;
                int boxCount = relevantBoxes.Count;
                if (boxCount == 0) continue;

                if (boxCount > 1)
                    summarySheet.Row(summaryStartRow).InsertRowsBelow(boxCount - 1);

                int currentSummaryRow = summaryStartRow;

                var boxUsedBlankMap = usedBlanks.GroupBy(b => b.BoxId).ToDictionary(g => g.Key, g => g.ToList());

                foreach (var box in relevantBoxes)
                {
                    var boxUsedBlanks = boxUsedBlankMap.ContainsKey(box.Id) ? boxUsedBlankMap[box.Id] : new List<Models.Blank>();
                    if (boxUsedBlanks.Count == 0) continue;

                    int totalInBox = box.EndNumber - box.StartNumber + 1;
                    int usedCount = boxUsedBlanks.Count;
                    int remainingCount = totalInBox - usedCount;

                    int usedMin = boxUsedBlanks.Min(b => b.BlankNumber);
                    int usedMax = boxUsedBlanks.Max(b => b.BlankNumber);

                    var allNumbers = Enumerable.Range(box.StartNumber, totalInBox).ToHashSet();
                    var usedNumbers = boxUsedBlanks.Select(b => b.BlankNumber).ToHashSet();
                    var remainingNumbers = allNumbers.Except(usedNumbers).OrderBy(n => n).ToList();

                    int remainingMin = remainingNumbers.Count > 0 ? remainingNumbers.First() : 0;
                    int remainingMax = remainingNumbers.Count > 0 ? remainingNumbers.Last() : 0;

                    var row = summarySheet.Row(currentSummaryRow);
                    row.Style.Font.FontName = "Arial Cyr";
                    row.Style.Font.FontSize = 9;
                    row.Style.Font.Bold = false;

                    row.Cell(2).Value = $"Коробка #{box.Id}";
                    row.Cell(3).Value = totalInBox;
                    row.Cell(4).Value = box.Series;
                    row.Cell(5).Value = box.StartNumber;
                    row.Cell(6).Value = box.EndNumber;

                    row.Cell(7).Value = usedCount;
                    row.Cell(8).Value = box.Series;
                    row.Cell(9).Value = usedMin;
                    row.Cell(10).Value = usedMax;

                    row.Cell(11).Value = remainingCount;
                    row.Cell(12).Value = box.Series;
                    row.Cell(13).Value = remainingMin;
                    row.Cell(14).Value = remainingMax;

                    currentSummaryRow++;
                }

                int afterSummaryRow = summaryStartRow + boxCount;
                var summaryLastRowUsed = summarySheet.LastRowUsed()?.RowNumber() ?? afterSummaryRow;
                int currentRowDatewise = summaryLastRowUsed + 2;

                int templateRowIndex = currentRowDatewise;

                var blanksGroupedByYear = usedBlanks
                    .Where(b => b.Date.HasValue)
                    .GroupBy(b => b.Date.Value.Year)
                    .OrderBy(g => g.Key)
                    .ToList();

                var nextRemainingNumberMap = relevantBoxes.ToDictionary(b => b.Id, b => b.StartNumber);

                foreach (var yearGroup in blanksGroupedByYear)
                {
                    int year = yearGroup.Key;

                    summarySheet.Row(templateRowIndex).CopyTo(summarySheet.Row(currentRowDatewise));

                    var yearRange = summarySheet.Range(currentRowDatewise, 1, currentRowDatewise, 14);
                    yearRange.Merge();
                    yearRange.Value = year.ToString();
                    yearRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    yearRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    yearRange.Style.Font.Bold = true;
                    yearRange.Style.Font.FontSize = 11;

                    currentRowDatewise++;


                    var groupedByDateAndBox = yearGroup
                        .GroupBy(b => new { b.Date, b.BoxId })
                        .OrderBy(g => g.Key.Date)
                        .ThenBy(g => relevantBoxes.FindIndex(b => b.Id == g.Key.BoxId));

                    foreach (var group in groupedByDateAndBox)
                    {
                        var date = group.Key.Date;
                        var boxId = group.Key.BoxId;
                        var box = boxes.First(b => b.Id == boxId);

                        var used = group.OrderBy(b => b.BlankNumber).ToList();
                        int usedCount = used.Count;
                        int usedMin = used.Min(b => b.BlankNumber);
                        int usedMax = used.Max(b => b.BlankNumber);

                        int totalInBox = box.EndNumber - box.StartNumber + 1;

                        if (!nextRemainingNumberMap.ContainsKey(box.Id))
                            nextRemainingNumberMap[box.Id] = box.StartNumber;

                        int currentStart = nextRemainingNumberMap[box.Id];
                        int remainingTotal = totalInBox - (currentStart - box.StartNumber) - usedCount;
                        int nextStart = currentStart + usedCount;

                        var row = summarySheet.Row(currentRowDatewise++);
                        row.Style.Font.FontName = "Arial Cyr";
                        row.Style.Font.FontSize = 9;
                        row.Style.Font.Bold = false;

                        row.Cell(1).Value = date?.ToString("dd.MM.yyyy") ?? "";
                        row.Cell(7).Value = usedCount;
                        row.Cell(8).Value = box.Series;
                        row.Cell(9).Value = usedMin;
                        row.Cell(10).Value = usedMax;

                        row.Cell(11).Value = remainingTotal;
                        row.Cell(12).Value = box.Series;
                        row.Cell(13).Value = nextStart;
                        row.Cell(14).Value = box.EndNumber;

                        nextRemainingNumberMap[box.Id] = nextStart;
                    }
                }

                workbook.Save();
            }
        }
    }
}