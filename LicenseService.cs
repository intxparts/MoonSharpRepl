using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MoonSharpRepl
{
    public interface ILicenseService : IService
    {
        LicenseData LicenseData { get; }
        void CheckLicense();
    }

    public enum LicenseResult
    {
        NONE = 0,
        FAIL = 1,
        SUCCESS = 2
    }

    public class LicenseData
    {
        public LicenseResult LicenseResult { get; set; }
        public List<string> Modules { get; set; }
    }

    public class LicenseService : ILicenseService
    {
        public LicenseData LicenseData { get; private set; }

        public void CheckLicense()
        {
            // connect to license service server and get the result

            LicenseData = new LicenseData()
            {
                LicenseResult = LicenseResult.SUCCESS,
                Modules = new List<string>()
            };
        }

        public void Initialize(IAppContainer container)
        {
        }
    }
}
