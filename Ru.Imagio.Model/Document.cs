using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Ru.Imagio.Model
{
    [Table("Documents")]
    public class Document : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("DocumentNumber")]
        public String DocumentNumber { get; set; }

        [Column("Name")]
        public String Name { get; set; }

        [Column("Date")]
        public DateTime? Date { get; set; }

        [Column("IsDeleted")]
        public Boolean IsDeleted { get; set; }

        [Column("EmployeeId")]
        public Int32? EmployeeId { get; set; }
    }
}
