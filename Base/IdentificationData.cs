//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Desktop_Auth.Base
{
    using System;
    using System.Collections.Generic;
    
    public partial class IdentificationData
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IdentificationData()
        {
            this.InstalledEquipmentInfoes = new HashSet<InstalledEquipmentInfo>();
        }
    
        public int ID { get; set; }
        public string SerialNumber { get; set; }
        public string InventoryNumberOS { get; set; }
        public string IP { get; set; }
        public string MAC { get; set; }
        public string NumbersPhones { get; set; }
        public string InstallationDate { get; set; }
        public string OtherInfo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstalledEquipmentInfo> InstalledEquipmentInfoes { get; set; }
    }
}
