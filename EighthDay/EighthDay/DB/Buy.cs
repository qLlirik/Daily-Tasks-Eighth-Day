//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EighthDay.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Buy
    {
        public int ID { get; set; }
        public double GoodsID { get; set; }
        public int ProviderID { get; set; }
        public bool Sign { get; set; }
        public short GoodsInvoice { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual Goods Goods { get; set; }
        public virtual Provider Provider { get; set; }
    }
}