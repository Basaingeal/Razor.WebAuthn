using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public abstract class PublicKeyCredential : Credential
    {
#pragma warning disable CA1819 // Properties should not return arrays
        public byte[] RawId { get; internal set; }
#pragma warning restore CA1819 // Properties should not return arrays

        public AuthenticatorResponse Response { get; internal set; }

        private IJSRuntime JSRuntime { get; set; }
        private Guid ClientSideId { get; set; }
        private const string jsNamespace = "CurrieTechnologies.Razor.WebAuthn";

        internal void Setup(IJSRuntime jSRuntime, Guid guid)
        {
            this.JSRuntime = jSRuntime;
            this.ClientSideId = guid;
            switch(Response)
            {
                case AuthenticatorAttestationResponse aar: {
                        aar.Setup(jSRuntime, guid);
                        break;
                    }
            }
        }


        public Task<AuthenticationExtensionsClientOutputs> GetClientExtensionResultsAsync()
        {
            return this.JSRuntime.InvokeAsync<AuthenticationExtensionsClientOutputs>($"{jsNamespace}.GetClientExtensionResults", ClientSideId).AsTask();
        }
    }
}
