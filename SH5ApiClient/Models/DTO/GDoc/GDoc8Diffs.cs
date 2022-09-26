namespace SH5ApiClient.Models.DTO
{
    /// <summary>Сличительная ведомость излишки/недостачи</summary>
    public class GDoc8Diffs
    {
        /// <summary>Содержимое накладной</summary>
        [OriginalName("112")]
        public IEnumerable<GDocItem?>? Content { get; set; }
        public static GDoc8Diffs? Parse(ExecOperation answear)
        {
            ExecOperationContent content = answear.GetAnswearContent("112");
            return new GDoc8Diffs
            {
                Content = content.GetValues().Select(t => GDocItem.Parse(t))
            };
        }
    }
}
