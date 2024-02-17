using System.ComponentModel.DataAnnotations;

namespace HRLeaveManagement.BlazorUI.Models.LeaveTypes;

public class LeaveTypeVM
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Default Number Of Days")]
    public int DefaultDayss { get; set; }
}
