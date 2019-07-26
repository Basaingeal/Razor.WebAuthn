using Microsoft.Extensions.DependencyInjection;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public static class ExtensionMethods
    {
        public static IServiceCollection AddSweetAlert2(this IServiceCollection services)
        {
            return services.AddScoped<CredentialsContainer>();
        }
    }
}
