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
    
    public partial class RealEstateFilterSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RealEstateFilterSet()
        {
            this.z3_DemandSet = new HashSet<DemandSet>();
        }
    
        public int Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DemandSet> z3_DemandSet { get; set; }
        public virtual RealEstateFilterSet_ApartmentFilter z3_RealEstateFilterSet_ApartmentFilter { get; set; }
        public virtual RealEstateFilterSet_HouseFilter z3_RealEstateFilterSet_HouseFilter { get; set; }
        public virtual RealEstateFilterSet_LandFilter z3_RealEstateFilterSet_LandFilter { get; set; }
    }
}
