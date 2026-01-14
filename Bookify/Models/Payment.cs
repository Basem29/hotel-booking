using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookify.Models
{
    public enum PaymentStatus { Pending, Completed, Failed }
    public enum PaymentTypeEnum { Cash, CreditCard, PayPal }
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public PaymentTypeEnum PaymentType { get; set; }

        [Required]
        public PaymentStatus Status { get; set; }

        [Required]
        public decimal Amount { get; set; }   

        public DateTime PaymentDate { get; set; } = DateTime.Now; 

        [Required]
        public int ReservationId { get; set; }

        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; }

    }

}
