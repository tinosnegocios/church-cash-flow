using ChurchCashFlow.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace ChurchCashFlow.Data.ViewModels.Dtos.Church;
public class EditChurchDto : ModelDto
{
    [Required(ErrorMessage = "The name is required")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "The size must be between 5 and 50 characters")]
    public string Name { get; set; }
    public bool? Active { get; set; }
}
