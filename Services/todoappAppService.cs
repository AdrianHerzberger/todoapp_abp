using Volo.Abp.Application.Services;
using todoapp.Localization;

namespace todoapp.Services;

/* Inherit your application services from this class. */
public abstract class todoappAppService : ApplicationService
{
    protected todoappAppService()
    {
        LocalizationResource = typeof(todoappResource);
    }
}