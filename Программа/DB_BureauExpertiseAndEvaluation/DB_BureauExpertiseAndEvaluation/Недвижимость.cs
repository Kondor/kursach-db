//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DB_BureauExpertiseAndEvaluation
{
    using System;
    using System.Collections.Generic;
    
    public partial class Недвижимость
    {
        public string Адрес { get; set; }
        public string Район { get; set; }
        public int Ид_объекта { get; set; }
        public short Количество_комнат { get; set; }
        public float Площадь { get; set; }
        public string Тип_постройки { get; set; }
    
        public virtual Объекты_экспертизы Объекты_экспертизы { get; set; }
    }
}
