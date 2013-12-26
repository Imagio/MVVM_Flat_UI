using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Ru.Imagio.Model
{
    [Table("SendAccounts")]
    public class SendAccount : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ReceiveDate")]
        public DateTime? ReceiveDate { get; set; }

        [Column("AccountId")]
        public int AccountId { get; set; }

        [Column("SendDate")]
        public DateTime SendDate { get; set; }

        [Column("DocumentId")]
        public int DocumentId { get; set; }

        public virtual Document Document { get; set; }
    }
}
