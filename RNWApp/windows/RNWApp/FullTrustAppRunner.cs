using System;
using Windows.ApplicationModel;
using Windows.Foundation.Metadata;
using Microsoft.ReactNative.Managed;

namespace RNWApp
{
    [ReactModule]
    public class FullTrustAppRunner
    {
        [ReactMethod]
        public async void Execute()
        {
            if (ApiInformation.IsApiContractPresent("Windows.ApplicationModel.FullTrustAppContract", 1, 0))
            {
                await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
            }
        }
    }
}
