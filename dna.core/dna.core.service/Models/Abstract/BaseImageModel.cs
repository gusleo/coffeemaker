using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dna.core.service.Models.Abstract
{
    public class BaseImageModel : WriteHistoryBaseModel, IModelBase
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string ImagePath { get; set; }
        [Required, MaxLength(100)]
        public string ImageName { get; set; }

    }
}
