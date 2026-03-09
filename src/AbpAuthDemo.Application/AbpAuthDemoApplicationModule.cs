using Volo.Abp.Modularity;

namespace AbpAuthDemo;

[DependsOn(
    typeof(AbpAuthDemoApplicationContractsModule),
    typeof(AbpAuthDemoDomainModule)
)]
public class AbpAuthDemoApplicationModule : AbpModule
{
}
