using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace AbpAuthDemo.Permissions;

public class AbpAuthDemoPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var group = context.AddGroup("AbpAuthDemo", L("Permission:Group"));

        var itemsPermission = group.AddPermission(AbpAuthDemoPermissions.Items.Default, L("Permission:Items"));
        itemsPermission.AddChild(AbpAuthDemoPermissions.Items.Create, L("Permission:Items.Create"));
        itemsPermission.AddChild(AbpAuthDemoPermissions.Items.Edit, L("Permission:Items.Edit"));
        itemsPermission.AddChild(AbpAuthDemoPermissions.Items.Delete, L("Permission:Items.Delete"));
    }

    private static LocalizableString L(string name) => LocalizableString.Create<AbpAuthDemoResource>(name);
}
