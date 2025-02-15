//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteBicycleStore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int IDOrder { get; set; }
        public Nullable<int> IDUser { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string Address_Cus { get; set; }
        public Nullable<double> Amount { get; set; }
        public string Descriptions { get; set; }
        public Nullable<bool> TinhTrangGiao { get; set; }
        public Nullable<bool> TinhTrangDonHang { get; set; }
        public Nullable<bool> TinhTrangThanhToan { get; set; }
        public Nullable<bool> HuyDon { get; set; }
        public Nullable<bool> TinhTrangDongGoi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual User User { get; set; }
    }
}
