using System.ComponentModel.DataAnnotations;

namespace Bookify.DTOs.Auth
{
    public class RegisterDto
    {
        [Required] public required string FullName { get; set; } 
        [Required, EmailAddress] public required string Email { get; set; } 
        [Required] public required string Password { get; set; } 

        public string? Address { get; set; } 

        [Required] public required string NationalId { get; set; } 
        [Required] public required string Nationality { get; set; } 
        [Required, DataType(DataType.Date)] public required DateTime BirthDate { get; set; } 
    }
}