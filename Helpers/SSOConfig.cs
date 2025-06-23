using Amazon.Runtime.CredentialManagement;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace KARE.E2E.AUTOMATION.Helpers
{
    internal class SSOConfig
    {
        public static AWSCredentials LoadSsoCredentials(string profileName)
        {
            var chain = new CredentialProfileStoreChain();
            if (!chain.TryGetAWSCredentials(profileName, out var credentials))
                throw new Exception($"Failed to find the {profileName} profile");

            var ssoCredentials = credentials as SSOAWSCredentials;

            ssoCredentials.Options.ClientName = "local-session-csharp";
            ssoCredentials.Options.SsoVerificationCallback = args =>
            {
                // Launch a browser window that prompts the SSO user to complete an SSO sign-in.
                // This method is only invoked if the session doesn't already have a valid SSO token.
                // NOTE: Process.Start might not support launching a browser on macOS or Linux. If not,
                //       use an appropriate mechanism on those systems instead.
                Process.Start(new ProcessStartInfo
                {
                    FileName = args.VerificationUriComplete,
                    UseShellExecute = true
                });
            };

            return ssoCredentials;
        }
    }
}
