﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APISample.Models.Entities
{
	public class PointOfIntrest
	{
        public PointOfIntrest(string name)
        {
            this.Name = name;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
		public string Name { get; set; }
        public string? Description { get; set; }


        [ForeignKey("CityId")]
        public City? City { get; set; }
        public int CityId { get; set; }

    }
}
