using System;
using System.Collections.Generic;
using System.Text;

namespace MoonSharpRepl
{
    public interface IAuthorizationService : IService
    {
        bool IsAuthorizedToAccessFeature(LicenseControlledFeatures feature);
    }

    public enum LicenseControlledFeatures
    {
        FEATURE_1 = 0,
        FEATURE_2 = 1
    }

    public class AuthorizationService : IAuthorizationService
    {
        private HashSet<LicenseControlledFeatures> _licenseControlledFeatures;

        public AuthorizationService()
        {
            _licenseControlledFeatures = new HashSet<LicenseControlledFeatures>();
        }

        private ILicenseService _licenseService;

        public void Initialize(IAppContainer container)
        {
            _licenseService = container.LicenseService;
            SetAccessToFeatures();
        }

        public bool IsAuthorizedToAccessFeature(LicenseControlledFeatures feature)
        {
            return _licenseControlledFeatures.Contains(feature);
        }

        private void AddAccessToFeature(LicenseControlledFeatures feature)
        {
            _licenseControlledFeatures.Add(feature);
        }

        private void SetAccessToFeatures()
        {
            // register features to different licenses
        }
    }
}
