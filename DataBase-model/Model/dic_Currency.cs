//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
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