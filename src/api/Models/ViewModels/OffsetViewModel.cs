using System.ComponentModel.DataAnnotations;

namespace api.Models.ViewModels;

public class OffsetViewModel
{
    public int Index { get; set; }

    [Range(1, 50)]
    public int? Length { get; set; }
}
