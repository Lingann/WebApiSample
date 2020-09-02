using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("account")]
    public class Account
    {
        [Column("account_id")]
        public int Id { get; set; }

        [Column("date_created")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [Column("account_type")]
        public  string AccountType { get; set; }
    
        [Column("owner_id")]
        [ForeignKey(nameof(Owner))] // 外键属性，用于声明一个账户仅与一个所有者相关
        public int OwnerId { get; set; }

        public Owner owner { get; set; }
    }
}
