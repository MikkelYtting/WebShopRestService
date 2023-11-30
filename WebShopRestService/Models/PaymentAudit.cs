using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopRestService.Models
{
    public class PaymentAudit
    {
        [Key]
        public int PaymentAuditId { get; set; }

        [ForeignKey("OrderTable")]
        [Required(ErrorMessage = "Order ID is required.")]
        // Assuming OrderId should be a positive integer
        [Range(1, int.MaxValue, ErrorMessage = "Order ID must be a positive number.")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        // Ensure the date is not in the future
        [DateNotInTheFuture(ErrorMessage = "Date cannot be in the future.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Column(TypeName = "decimal(9, 2)")]
        [Range(0.01, 1000000.00, ErrorMessage = "Amount must be between 0.01 and 1000000.00")]
        // Ensure the amount is not negative
        [NonNegativeValue(ErrorMessage = "Amount cannot be negative.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Action type is required.")]
        [StringLength(50, ErrorMessage = "Action type cannot be longer than 50 characters.")]
        // Regular expression to ensure ActionType follows a certain pattern (e.g., only letters)
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Action type must contain only letters.")]
        public string ActionType { get; set; } 

        [Required(ErrorMessage = "Action date is required.")]
        [DataType(DataType.DateTime)]
        // Ensure the action date is either today or in the past
        [DateNotInTheFuture(ErrorMessage = "Action date cannot be in the future.")]
        public DateTime ActionDate { get; set; }

        public OrderTable OrderTable { get; set; }
    }

    // Custom validation attribute to ensure the date is not in the future
    public class DateNotInTheFutureAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var date = (DateTime)value;
            return date <= DateTime.Now;
        }
    }

    // Custom validation attribute to ensure a value is non-negative
    public class NonNegativeValueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is decimal decimalValue)
            {
                return decimalValue >= 0;
            }
            return true;
        }
    }
}