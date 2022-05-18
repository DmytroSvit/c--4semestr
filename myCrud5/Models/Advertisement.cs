using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using myCrud.Validation;

namespace myCrud.Models
{
    public class Advertisement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } = default(int);

        [Required]
        public string? Title { get; set; }
        [Required]
        [UrlValidator]
        public string WebsiteUrl { get; set; }
        [Required]
        [UrlValidator]
        public string PhotoUrl { get; set; }

        [Required]
        [StartDateValidator]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [PriceValidator]
        public int Price { get; set; }

        [Required]
        [RegularExpression(
        Settings.TransactionNumberPattern,
        ErrorMessage = "Transaction number must be in form: XX-YYY-XX/YY, X - uppercase letter, Y - digit."
        )
    ]
        public string? TransactionNumber { get; set; }

        [JsonIgnore] public string? Owner { get; set; }

    }
}
