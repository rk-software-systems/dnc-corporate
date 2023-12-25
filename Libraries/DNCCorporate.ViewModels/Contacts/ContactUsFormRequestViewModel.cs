using System.ComponentModel.DataAnnotations;

namespace DNCCorporate.ViewModels;

public record class ContactUsFormRequestViewModel(
    [Required][MaxLength(100)]  string? FullName,
    [Required][EmailAddress][MaxLength(100)]  string? EmailAddress,
    [MaxLength(100)]  string? Subject,
    [Required][MaxLength(5000)]  string? Message
    );
