using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;

namespace Ru.Imagio.Model
{
    public abstract class ModelBase
    {
        [Key]
        [Required]
        public abstract int Id { get; set; }
    }
}
