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
    
    public partial class OrderDetail
    {
        public int ID { get; set; }
        public Nullable<int> IDOrder { get; set; }
        public Nullable<int> IDProduct { get; set; }
        public Nullable<decimal> UnitPriceSale { get; set; }
        public Nullable<int> QuantitySale { get; set; }
        public string imgPro { get; set; }
        public Nullable<System.DateTime> ngayDat { get; set; }
        public Nullable<System.DateTime> ngayNhan { get; set; }
        public string namePro { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
