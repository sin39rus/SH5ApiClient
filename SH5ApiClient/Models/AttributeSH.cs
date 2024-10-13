using SH5ApiClient.Models.Enums;
using System;

namespace SH5ApiClient.Models
{
    /// <summary>
    /// Используемый атрибут
    /// </summary>
    public class AttributeSH
    {
        /// <param name="identity">Идентификатор</param>
        /// <param name="type">Тип данных</param>
        /// <param name="caption">Заголовок(наименование)</param>
        /// <param name="procName">Имя процедуры</param>
        /// <param name="headName">Заголовок таблицы</param>
        /// <exception cref="ArgumentNullException"></exception>
        public AttributeSH(string identity, string path, SHAttributeType type, string caption, string procName, string bob, string headName)
        {
            Type = type;
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
            Path = path ?? throw new ArgumentNullException(nameof(path)); ;
            Caption = caption ?? throw new ArgumentNullException(nameof(caption));
            ProcName = procName ?? throw new ArgumentNullException(nameof(procName));
            Bob = bob ?? throw new ArgumentNullException(nameof(bob));
            HeadName = headName ?? throw new ArgumentNullException(nameof(headName));
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Identity { get; private set; }
        /// <summary>
        /// Путь
        /// </summary>
        public string Path { get; private set; }
        /// <summary>
        /// Тип данных
        /// </summary>
        public SHAttributeType Type { get; private set; }
        /// <summary>
        /// Заголовок(наименование)
        /// </summary>
        public string Caption { get; private set; }
        /// <summary>
        /// Имя процедуры
        /// </summary>
        public string ProcName { get; private set; }
        /// <summary>
        /// Заголовок таблицы
        /// </summary>
        public string HeadName { get; private set; }
        /// <summary>
        /// Идентификатор контейнера (SdbMan: Обслуживание - Статистика - Поля бин.объектов)
        /// </summary>
        public string Bob { get; set; }
        public bool IsChecked { set; get; }
    }
}
