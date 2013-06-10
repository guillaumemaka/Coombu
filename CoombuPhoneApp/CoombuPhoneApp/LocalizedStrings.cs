using CoombuPhoneApp.Resources;

namespace CoombuPhoneApp
{
    /// <summary>
    /// Permet d'accéder aux ressources de chaîne.
    /// </summary>
    public class LocalizedStrings
    {
        private static AppResources _localizedResources = new AppResources();
        public AppResources LocalizedResources { get { return _localizedResources; } }

        private static RegistrationResources _localizedRegistrationResources = new RegistrationResources();
        public RegistrationResources LocalizedRegistrationResources { get { return _localizedRegistrationResources; } }
    }
}