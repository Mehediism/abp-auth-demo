using Volo.Abp.Modularity;

namespace AbpAuthDemo;

[DependsOn(typeof(AbpAuthDemoDomainSharedModule))]
public class AbpAuthDemoDomainModule : AbpModule
{
}
