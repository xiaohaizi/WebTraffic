//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebTraffic.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Task
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public Nullable<bool> Vip { get; set; }
        public Nullable<int> StartNum { get; set; }
        public Nullable<int> EndNum { get; set; }
        public Nullable<int> Speed { get; set; }
        public Nullable<int> Praise { get; set; }
        public string PraiseUnit { get; set; }
        public Nullable<int> UserWecat { get; set; }
        public Nullable<int> TransmitWecat { get; set; }
        public Nullable<int> FriendWecat { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public Nullable<int> TaskStatus { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public System.DateTime CreateTime { get; set; }
        public Nullable<int> ReadNum { get; set; }
        public Nullable<int> OrderNum { get; set; }
        public Nullable<int> StartReadNum { get; set; }
    }
}
