using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace AbpAuthDemo;

public class AbpAuthDemoDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources.Add<AbpAuthDemoResource>("en")
                .AddVirtualJson("/Localization/AbpAuthDemo");
        });
    }
}
