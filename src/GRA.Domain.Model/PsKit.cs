﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GRA.Domain.Model
{
    public class PsKit : Abstract.BaseDomainEntity
    {
        [MaxLength(255, ErrorMessage = "Please enter no more than {1} characters for {0}.")]
        [Required]
        public string Name { get; set; }

        [MaxLength(2000, ErrorMessage = "Please enter no more than {1} characters for {0}.")]
        [Required]
        public string Description { get; set; }

        [MaxLength(255, ErrorMessage = "Please enter no more than {1} characters for {0}.")]
        public string Website { get; set; }

        public List<PsKitImage> Images { get; set; }
        public ICollection<PsAgeGroup> AgeGroups { get; set; }

        public int SelectionsCount { get; set; }
    }
}
