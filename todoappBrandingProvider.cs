using Microsoft.Extensions.Localization;
using todoapp.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace todoapp;

[Dependency(ReplaceServices = true)]
public class todoappBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<todoappResource> _localizer;

    public todoappBrandingProvider(IStringLocalizer<todoappResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}