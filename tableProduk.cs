//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Stock
{
    using System;
    using System.Collections.Generic;
    
    public partial class tableProduk
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tableProduk()
        {
            this.tableStockKeluars = new HashSet<tableStockKeluar>();
            this.tableStockMasuks = new HashSet<tableStockMasuk>();
        }
    
        public int idProduk { get; set; }
        public int idMerek { get; set; }
        public string namaProduk { get; set; }
        public Nullable<int> RAM { get; set; }
        public string mInternal { get; set; }
        public byte[] imgPrd { get; set; }
        public Nullable<int> hargaPrd { get; set; }
    
        public virtual tableMerek tableMerek { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tableStockKeluar> tableStockKeluars { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tableStockMasuk> tableStockMasuks { get; set; }
    }
}
