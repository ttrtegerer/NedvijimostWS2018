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
    
    public partial class DemandSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DemandSet()
        {
            this.z3_DealSet = new HashSet<DealSet>();
        }
    
        public int Id { get; set; }
        public string Address_City { get; set; }
        public string Address_Street { get; set; }
        public string Address_House { get; set; }
        public string Address_Number { get; set; }
        public Nullable<long> MinPrice { get; set; }
        public Nullable<long> MaxPrice { get; set; }
        public Nullable<int> AgentId { get; set; }
        public int ClientId { get; set; }
        public int RealEstateFilter_Id { get; set; }
        public string Potrebnost
        {
            get
            {
                return ("|"+Id+"| "+Address_City+","+Address_Street+","+Address_House+","+Address_Number).ToString();
            }
        }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DealSet> z3_DealSet { get; set; }
        public virtual PersonSet_Agent z3_PersonSet_Agent { get; set; }
        public virtual PersonSet_Client z3_PersonSet_Client { get; set; }
        public virtual RealEstateFilterSet z3_RealEstateFilterSet { get; set; }
    }
}