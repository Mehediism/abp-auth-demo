using Volo.Abp.Application.Services;

namespace AbpAuthDemo.Items;

public interface IItemAppService : IApplicationService
{
    Task<ItemDto> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ItemDto> CreateAsync(CreateItemDto input, CancellationToken cancellationToken = default);
}
