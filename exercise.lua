print(AppContainer.LicenseService.LicenseData == nil) -- true
AppContainer.LicenseService.CheckLicense()
print(AppContainer.LicenseService.LicenseData.LicenseResult == LicenseResult.SUCCESS)
