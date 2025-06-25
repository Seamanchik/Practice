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
                var relevantBlanks = blanks.Where(b => relevantBoxes.Any(box => box.Id == b.BoxId)).ToList();

                if (relevantBlanks.Count == 0) continue;

                using var workbook = new XLWorkbook(filePath);
                var worksheet = workbook.Worksheet("Расшифровка карточки");

                if (worksheet == null)
                {
                    MessageBox.Show($"Лист 'Расшифровка карточки' не найден в файле {filePath}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                var exportedKeys = new HashSet<string>();
                int lastRow = worksheet.LastRowUsed()?.RowNumber() ?? 1;

                int ppNumber = 1;
                for (int row = 2; row <= lastRow; row++)
                {
                    string? series = worksheet.Cell(row, 4).GetString();
                    string? number = worksheet.Cell(row, 5).GetString();

                    if (!string.IsNullOrWhiteSpace(series) && !string.IsNullOrWhiteSpace(number))
                    {
                        exportedKeys.Add($"{series}_{number}");

                        var cellValue = worksheet.Cell(row, 1).GetValue<string>();
                        if (int.TryParse(cellValue, out int existingPp))
                        {
                            ppNumber = Math.Max(ppNumber, existingPp + 1);
                        }
                    }
                }

                int currentRow = lastRow + 1;

                foreach (var blank in relevantBlanks)
                {
                    if (string.IsNullOrWhiteSpace(blank.ProductName) || blank.Date == null || blank.RecipientId == null)
                        continue;

                    var box = boxes.FirstOrDefault(b => b.Id == blank.BoxId);
                    if (box == null) continue;

                    var recipient = recipients.FirstOrDefault(r => r.Id == blank.RecipientId);
                    if (recipient == null) continue;

                    string key = $"{box.Series}_{blank.BlankNumber}";
                    if (exportedKeys.Contains(key)) continue;

                    worksheet.Cell(currentRow, 1).Value = ppNumber++; // № п.п.
                    worksheet.Cell(currentRow, 2).Value = documentName;
                    worksheet.Cell(currentRow, 3).Value = blank.Date.Value.ToString("dd.MM.yyyy");
                    worksheet.Cell(currentRow, 4).Value = box.Series;
                    worksheet.Cell(currentRow, 5).Value = blank.BlankNumber;
                    worksheet.Cell(currentRow, 6).Value = recipient.Name;
                    worksheet.Cell(currentRow, 7).Value = blank.ProductName;

                    currentRow++;
                }

                workbook.Save();
            }
        }
    }
}