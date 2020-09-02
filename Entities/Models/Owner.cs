using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Entities.Models
{
    [Table("owner")]
    public class Owner
    {
        [Column("owner_id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("date_of_birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        [Column("address")]
        public string Address { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}
