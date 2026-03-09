using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;

namespace AbpAuthDemo.Items;

[Authorize(AbpAuthDemoPermissions.Items.Default)]
public class ItemAppService : ApplicationService, IItemAppService
{
    public virtual async Task<ItemDto> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await AuthorizationService.CheckAsync(AbpAuthDemoPermissions.Items.Default);
        return await Task.FromResult(new ItemDto { Id = id, Name = "Sample" });
    }

    [Authorize(AbpAuthDemoPermissions.Items.Create)]
    public virtual async Task<ItemDto> CreateAsync(CreateItemDto input, CancellationToken cancellationToken = default)
    {
        var id = GuidGenerator.Create();
        return await Task.FromResult(new ItemDto { Id = id, Name = input.Name });
    }
}
