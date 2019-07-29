using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public class AuthenticatorAttestationResponse : AuthenticatorResponse
    {
#pragma warning disable CA1819 // Properties should not return arrays
        public byte[] AttestationObject { get; internal set; }
#pragma warning restore CA1819 // Properties should not return arrays

        private IJSRuntime JSRuntime { get; set; }
        private Guid ClientSideId { get; set; }
        private const string jsNamespace = "CurrieTechnologies.Razor.WebAuthn";

        internal void Setup(IJSRuntime jSRuntime, Guid guid)
        {
            this.JSRuntime = jSRuntime;
            this.ClientSideId = guid;
        }

        public Task<IEnumerable<AuthenticatorTransport>> GetTransportsAsync()
        {
            return this.JSRuntime.InvokeAsync<IEnumerable<AuthenticatorTransport>>($"{jsNamespace}.GetTransports", ClientSideId);
        }
    }
}
