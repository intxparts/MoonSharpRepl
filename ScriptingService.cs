using MoonSharp.Interpreter;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoonSharpRepl
{
    public interface IScriptingService : IService
    {
        DynValue RunCommand(string command);

        event EventHandler<string> DebugPrint;
    }

    public sealed class ScriptingService : IScriptingService
    {
        private Script _script;

        public ScriptingService()
        {
        }

        public void Initialize(IAppContainer container)
        {
            _script = new Script(CoreModules.Preset_HardSandbox);
            _script.Options.DebugPrint = OnDebugPrint;

            // register classes to Script
            UserData.RegisterType<IAppContainer>();
            UserData.RegisterType<IScriptingService>();
            UserData.RegisterType<ILicenseService>();
            UserData.RegisterType<IAuthorizationService>();
            UserData.RegisterType<LicenseData>();
            UserData.RegisterType<LicenseResult>();
            UserData.RegisterType<LicenseControlledFeatures>();

            //Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<LicenseResult>((s, l) => {
            //    return DynValue.NewNumber((int)l);
            //});
            //Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<LicenseControlledFeatures>((s, l) => DynValue.NewNumber((int)l));
            
            DynValue c = UserData.Create(container);
            DynValue lr = UserData.CreateStatic<LicenseResult>();
            DynValue lcf = UserData.CreateStatic<LicenseControlledFeatures>();

            _script.Globals.Set(nameof(AppContainer), c);
            _script.Globals.Set(nameof(LicenseResult), lr);
            _script.Globals.Set(nameof(LicenseControlledFeatures), lcf);
        }

        private void OnDebugPrint(string s)
        {
            DebugPrint?.Invoke(this, s);
        }

        public DynValue RunCommand(string command)
        {
            return _script.DoString(command);
        }

        public event EventHandler<string> DebugPrint;
    }
}
