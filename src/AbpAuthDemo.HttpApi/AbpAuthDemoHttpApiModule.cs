using Volo.Abp.Modularity;

namespace AbpAuthDemo;

[DependsOn(typeof(AbpAuthDemoApplicationContractsModule))]
public class AbpAuthDemoHttpApiModule : AbpModule
{
}
