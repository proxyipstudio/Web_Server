//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class NodeInfo
    {
        public int Id { get; set; }
        public Nullable<int> HostId { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public Nullable<System.DateTime> startTime { get; set; }
        public Nullable<System.DateTime> Endtime { get; set; }
        public string ScanRate { get; set; }
        public Nullable<int> ScanCount { get; set; }
        public Nullable<int> ScanSumCount { get; set; }
        public Nullable<int> IsOpen { get; set; }
        public Nullable<int> IsDelete { get; set; }
        public Nullable<System.DateTime> DeleteTime { get; set; }
        public Nullable<int> DeleteID { get; set; }
        public Nullable<int> NodeType { get; set; }
    }
}
