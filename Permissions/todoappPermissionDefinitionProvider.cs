using todoapp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace todoapp.Permissions;

public class todoappPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(todoappPermissions.GroupName);


        //Define your own permissions here. Example:
        //myGroup.AddPermission(todoappPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<todoappResource>(name);
    }
}
