namespace SH5ApiClient.Models.Enums
{
    /// <summary>
    /// Доступные операции сервера
    /// </summary>
    public enum ServerOperationType
    {
        /// <summary>
        /// Запросить настройки сервера и информацию о БД:
        /// </summary>
        sh5info,
        /// <summary>
        /// Запросить наличие прав на выполнение процедуры: 
        /// </summary>
        sh5able,
        /// <summary>
        /// Изменить свой пароль: 
        /// </summary>
        sh5pass,
        /// <summary>
        /// Информация о лицензии:
        /// </summary>
        sh5lic,
        /// <summary>
        /// Запросить список датасетов процедуры: 
        /// </summary>
        sh5struct,
        /// <summary>
        /// Выполнить процедуру: 
        /// </summary>
        sh5exec,
        /// <summary>
        /// Cоздать атрибутное поле: 
        /// </summary>
        sh5CreateAttr,
        /// <summary>
        /// Запросить значения перечислимого атрибута: 
        /// </summary>
        sh5enum,
        /// <summary>
        /// Работа с пользователями: 
        /// </summary>
        sh5user,
        /// <summary>
        /// Работа с ролями: 
        /// </summary>
        sh5role
    }
}
