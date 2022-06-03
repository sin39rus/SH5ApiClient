using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH5ApiClient.Core.Answears
{
    public sealed class SHEnumAnswear : SHAnswearBase
    {
        [JsonProperty("Version")]
        public string? Version { get; set; }

        [JsonProperty("UserName")]
        public string? UserName { get; set; }

        [JsonProperty("actionType")]
        public string? ActionType { get; set; }

        [JsonProperty("head")]
        public string? Head { get; set; }

        [JsonProperty("path")]
        public string? Path { get; set; }

        [JsonProperty("idents")]
        public List<int> Idents { get; set; } = new();

        [JsonProperty("values")]
        public List<string> Values { get; set; } = new();

        /// <summary>
        /// Разобрать ответ SH
        /// </summary>
        /// <param name="jsonText">Содержимое ответа (json)</param>
        /// <returns>Ответ SH</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public static SHEnumAnswear Parse(string jsonText)
        {
            if (string.IsNullOrWhiteSpace(jsonText))
                throw new ArgumentException($"\"{nameof(jsonText)}\" не может быть пустым или содержать только пробел.", nameof(jsonText));
            SHEnumAnswear? answear = JsonConvert.DeserializeObject<SHEnumAnswear>(jsonText);
            if (answear == null)
                throw new ArgumentException("Ошибка разбора ответа SH.");
            answear.CheckError();
            return answear;
        }

        public Dictionary<int, string> GetValues()
        {
            Dictionary<int, string> values = new();
            for (int x = 0; x < Values.Count; x++)
                values.Add(Idents[x], Values[x]);
            return values;
        }
        public Dictionary<string, int> GetBankAccounts(char splitChar)
        {
            Dictionary<string, int> accaunts = new();
            for (int x = 0; x < Values.Count; x++)
            {
                if (Values[x].Split(splitChar).Length == 2)
                {
                    string bankAccaunt = Values[x].Split(splitChar)[1];
                    if (!string.IsNullOrWhiteSpace(bankAccaunt))
                        accaunts.Add(Values[x].Split(splitChar)[1], Idents[x]);
                }
            }
            return accaunts;
        }
    }
}
