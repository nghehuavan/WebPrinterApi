using DeviceId;

namespace KikanPrinter.Helper
{
    public static class DeviceHelper
    {
        public static string GetUniqueDeviceId()
        {
            return new DeviceIdBuilder()
                .AddMachineName()
                .AddOsVersion()
                .AddMacAddress()
                .ToString();
        }

    }
}
