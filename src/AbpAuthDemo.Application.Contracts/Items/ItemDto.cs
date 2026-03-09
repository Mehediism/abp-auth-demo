using Volo.Abp.Application.Dtos;

namespace AbpAuthDemo.Items;

public class ItemDto : EntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
}
