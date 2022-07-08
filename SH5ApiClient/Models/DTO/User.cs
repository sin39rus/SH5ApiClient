namespace SH5ApiClient.Models.DTO
{
    /// <summary>Пользователь</summary>
    [OriginalName("109")]
    public class User
    {    
        /// <summary>Rid</summary>
        [OriginalName("1")]
        public uint? Rid { set; get; }

        /// <summary>Name</summary>
        [OriginalName("3")]
        public string? Name { set; get; }

        /// <summary>Date</summary>
        [OriginalName("7")]
        public DateTime? Date { set; get; }

        /// <summary>Rid</summary>
        [OriginalName("8")]
        public uint? Time { set; get; } //ToDo переделать в TimeSpan

        public static User? Parse(Dictionary<string, string> value)
        {
            if (!value.Any())
                return null;
            return new User
            {
                Rid = uint.TryParse(value.GetValueOrDefault("1"), out uint rid) ? rid : null,
                Time = uint.TryParse(value.GetValueOrDefault("8"), out uint time) ? time : null,
                Name = value.GetValueOrDefault("3"),
                Date = DateTime.TryParse(value.GetValueOrDefault("7"), out DateTime date) ? date : null
            };
        }
    }
}
