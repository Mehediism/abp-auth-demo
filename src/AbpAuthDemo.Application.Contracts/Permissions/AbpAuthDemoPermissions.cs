namespace AbpAuthDemo.Permissions;

public static class AbpAuthDemoPermissions
{
    public const string GroupName = "AbpAuthDemo";

    public static class Items
    {
        public const string Default = GroupName + ".Items";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
}
