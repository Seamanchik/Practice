namespace Practice.Helper
{
    public class DataGridHelper
    {
        public static Dictionary<string, string> BlankHeaders => new()
    {
            { "BlankNumber", "Номер" },
            {"DocumentName", "Тип документа" },
            {"Series", "Серия" },
            { "Date", "Дата" },
            { "RecipientName", "Получатель" },
            { "ProductName", "Наименование товара" }
    };

        public static Dictionary<string, string> BoxHeaders => new()
    {
        { "StartNumber", "Начальный номер" },
        { "EndNumber", "Конечный номер" },
        { "Series", "Серия" },
        { "DocumentName", "Тип документа" }
    };

        public static Dictionary<string, string> ItemHeaders => new()
        {
            {"Id","Номер" },
            {"Name","Наименование" }
        };

        public static void SetColumnHeaders(DataGridView dgv, Dictionary<string, string> headerMap)
        {
            foreach (var entry in headerMap)
                if (dgv.Columns.Contains(entry.Key))
                    dgv.Columns[entry.Key].HeaderText = entry.Value;
        }
    }
}