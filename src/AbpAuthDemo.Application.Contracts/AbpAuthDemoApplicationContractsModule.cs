using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace AbpAuthDemo;

[DependsOn(
    typeof(AbpAuthorizationModule),
    typeof(AbpAuthDemoDomainSharedModule)
)]
public class AbpAuthDemoApplicationContractsModule : AbpModule
{
}
