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
    
    public partial class Объекты_экспертизы
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Объекты_экспертизы()
        {
            this.Накладная = new HashSet<Накладная>();
            this.Экспертиза_и_оценка = new HashSet<Экспертиза_и_оценка>();
        }
    
        public int Ид_объекта { get; set; }
        public string Наименование { get; set; }
        public string Вид { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Накладная> Накладная { get; set; }
        public virtual Недвижимость Недвижимость { get; set; }
        public virtual Техника Техника { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Экспертиза_и_оценка> Экспертиза_и_оценка { get; set; }
    }
}
