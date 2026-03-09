using System.ComponentModel.DataAnnotations;

namespace AbpAuthDemo.Items;

public class CreateItemDto
{
    [Required]
    [StringLength(128)]
    public string Name { get; set; } = string.Empty;
}
