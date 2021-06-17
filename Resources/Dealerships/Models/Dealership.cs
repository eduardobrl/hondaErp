using System;
using System.ComponentModel.DataAnnotations;
using hondaerp.Validators;

namespace hondaerp.Dealerships.Models
{
    public class Dealership
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [CNPJ]
        public string CNPJ { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime UpdatedAt { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(250)]
        public string Email { get; set; }

        [MaxLength(250)]
        public string CEP { get; set; }

        [MaxLength(250)]
        public string Street { get; set; }

        [MaxLength(10)]
        public int StreetNumber { get; set; }


    }
}