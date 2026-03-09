using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace AbpAuthDemo.Controllers;

[Area(AbpAuthDemoRemoteServiceConsts.ModuleName)]
[RemoteService(Name = AbpAuthDemoRemoteServiceConsts.RemoteServiceName)]
[Route("api/abp-auth-demo/items")]
[Authorize(AbpAuthDemoPermissions.Items.Default)]
public class ItemController : AbpControllerBase
{
    private readonly IItemAppService _itemAppService;

    public ItemController(IItemAppService itemAppService)
    {
        _itemAppService = itemAppService;
    }

    [HttpGet("{id}")]
    public virtual async Task<ItemDto> GetAsync(Guid id)
    {
        return await _itemAppService.GetAsync(id);
    }

    [HttpPost]
    [Authorize(AbpAuthDemoPermissions.Items.Create)]
    public virtual async Task<ItemDto> CreateAsync([FromBody] CreateItemDto input)
    {
        return await _itemAppService.CreateAsync(input);
    }
}
