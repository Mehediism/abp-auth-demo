using Volo.Abp.Localization;

namespace AbpAuthDemo;

public class AbpAuthDemoResource
{
    public const string CultureName = "en";

    public static LocalizableString Create(string key) => LocalizableString.Create<AbpAuthDemoResource>(key);
}
