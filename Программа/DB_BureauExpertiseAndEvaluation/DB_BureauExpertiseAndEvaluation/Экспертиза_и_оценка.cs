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
    
    public partial class Экспертиза_и_оценка
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Экспертиза_и_оценка()
        {
            this.Квитанция = new HashSet<Квитанция>();
        }
    
        public int Ид_диагностики { get; set; }
        public int Ид_объекта { get; set; }
        public string Вид_диагностики { get; set; }
        public int Количество_дней { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Квитанция> Квитанция { get; set; }
        public virtual Объекты_экспертизы Объекты_экспертизы { get; set; }
    }
}
