//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace esoft.entitis
{
    using System;
    using System.Collections.Generic;
    
    public partial class PersonSet_Agent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonSet_Agent()
        {
            this.z3_DemandSet = new HashSet<DemandSet>();
            this.z3_SupplySet = new HashSet<SupplySet>();
        }
        public string Name
        {
            get
            {
                return z3_PersonSet.FirstName;
            }
        }
        public string Sername
        {
            get
            {
                return z3_PersonSet.LastName;
            }
        }
        public string Otchestvo
        {
            get
            {
                return z3_PersonSet.MiddleName;
            }
        }
        public string Fio
        {
            get
            {
                return Sername + " " + Name + " " + Otchestvo;
            }
        }
        public int DealShare { get; set; }
        public int Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DemandSet> z3_DemandSet { get; set; }
        public virtual PersonSet z3_PersonSet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplySet> z3_SupplySet { get; set; }
    }
}
