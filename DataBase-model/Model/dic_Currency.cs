//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataBase_model.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class dic_Currency
    {
        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string GUID { get; set; }
        public Nullable<System.DateTime> ChangeDate { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
