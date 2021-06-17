using System;
using System.ComponentModel.DataAnnotations;
using hondaerp.Validators;

namespace hondaerp.Dealerships.Dtos
{
    public class DealershipCreateDto
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [CNPJ]
        public string CNPJ { get; set; }

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

        public int StreetNumber { get; set; }


    }
}