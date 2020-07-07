using System;
using System.Collections.Generic;
using System.Text;

namespace MoonSharpRepl
{
    public interface IAppContainer
    {
        ILicenseService LicenseService { get; }
        IAuthorizationService AuthorizationService { get; }

        IScriptingService ScriptingService { get; }
    }

    public sealed class AppContainer : IAppContainer
    {
        public AppContainer()
        {
            _licenseService = new Lazy<ILicenseService>(() =>
            {
                var licenseService = new LicenseService();
                licenseService.Initialize(this);
                return licenseService;
            }, isThreadSafe: true);
            _authorizationService = new Lazy<IAuthorizationService>(() =>
            {
                var authService = new AuthorizationService();
                authService.Initialize(this);
                return authService;
            }, isThreadSafe: true);
            _scriptingService = new Lazy<IScriptingService>(() =>
            {
                var scriptingService = new ScriptingService();
                scriptingService.Initialize(this);
                return scriptingService;
            }, isThreadSafe: true);
        }

        private readonly Lazy<ILicenseService> _licenseService;

        public ILicenseService LicenseService => _licenseService.Value;

        private readonly Lazy<IAuthorizationService> _authorizationService;
        public IAuthorizationService AuthorizationService => _authorizationService.Value;

        private readonly Lazy<IScriptingService> _scriptingService;
        public IScriptingService ScriptingService => _scriptingService.Value;
    }
}
