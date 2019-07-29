using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrieTechnologies.Razor.WebAuthn
{
    public class CredentialsContainer
    {
        private const string jsNamespace = "CurrieTechnologies.Razor.WebAuthn";
        private readonly IJSRuntime jSRuntime;
        private static readonly Dictionary<Guid, TaskCompletionSource<Credential?>> pendingGetRequests =
            new Dictionary<Guid, TaskCompletionSource<Credential?>>();
        private static readonly Dictionary<Guid, TaskCompletionSource<Credential>> pendingStoreRequests =
            new Dictionary<Guid, TaskCompletionSource<Credential>>();
        private static readonly Dictionary<Guid, TaskCompletionSource<Credential?>> pendingCreateRequests =
            new Dictionary<Guid, TaskCompletionSource<Credential?>>();
        private static readonly Dictionary<Guid, TaskCompletionSource<object>> pendingPreventSilentAccessRequests =
            new Dictionary<Guid, TaskCompletionSource<object>>();
        private static readonly Dictionary<Guid, TaskCompletionSource<bool>> pendingIsUserVerifyingPlatformAuthenticatorAvailableRequests =
            new Dictionary<Guid, TaskCompletionSource<bool>>();

        public CredentialsContainer(IJSRuntime jSRuntime)
        {
            this.jSRuntime = jSRuntime;
        }

        public async Task<Credential?> GetAsync(CredentialRequestOptions? options)
        {
            var tcs = new TaskCompletionSource<Credential?>();
            var requestId = Guid.NewGuid();
            pendingGetRequests.Add(requestId, tcs);
            await jSRuntime
                .InvokeAsync<object>($"{jsNamespace}.Get", requestId, options)
                .ConfigureAwait(false);
            var credential = await tcs.Task.ConfigureAwait(false);
            SetupCredential(credential, requestId);
            return credential;
        }

        [JSInvokable]
        public static Task ReceiveGetResponse(string requestId, AssertionPublicKeyCredential? credential)
        {
            var requestGuid = Guid.Parse(requestId);
            pendingGetRequests.TryGetValue(requestGuid, out TaskCompletionSource<Credential?> tcs);
            tcs.SetResult(credential as PublicKeyCredential);
            pendingGetRequests.Remove(requestGuid);
            return Task.CompletedTask;
        }

        public async Task<Credential?> StoreAsync(Credential credential)
        {
            var tcs = new TaskCompletionSource<Credential>();
            var requestId = Guid.NewGuid();
            pendingStoreRequests.Add(requestId, tcs);
            await jSRuntime
                .InvokeAsync<object>($"{jsNamespace}.Store", requestId, credential)
                .ConfigureAwait(false);
            var storedCredential = await tcs.Task.ConfigureAwait(false);
            SetupCredential(storedCredential, requestId);
            return storedCredential;
        }

        [JSInvokable]
        public static Task ReceiveStoreResponse(string requestId, Credential credential)
        {
            var requestGuid = Guid.Parse(requestId);
            pendingStoreRequests.TryGetValue(requestGuid, out TaskCompletionSource<Credential> tcs);
            tcs.SetResult(credential);
            pendingStoreRequests.Remove(requestGuid);
            return Task.CompletedTask;
        }

        public async Task<Credential?> CreateAsync(CredentialCreationOptions? options)
        {
            var tcs = new TaskCompletionSource<Credential?>();
            var requestId = Guid.NewGuid();
            pendingCreateRequests.Add(requestId, tcs);
            await jSRuntime
                .InvokeAsync<object>($"{jsNamespace}.Create", requestId, options)
                .ConfigureAwait(false);
            var credential = await tcs.Task.ConfigureAwait(false);
            SetupCredential(credential, requestId);
            return credential;
        }

        [JSInvokable]
        public static Task ReceiveCreateResponse(string requestId, AttestationPublicKeyCredential? credential)
        {
            var requestGuid = Guid.Parse(requestId);
            pendingCreateRequests.TryGetValue(requestGuid, out TaskCompletionSource<Credential?> tcs);
            tcs.SetResult(credential as PublicKeyCredential);
            pendingCreateRequests.Remove(requestGuid);
            return Task.CompletedTask;
        }

        private void SetupCredential(Credential? credential, Guid requestGuid)
        {
            switch (credential)
            {
                case PublicKeyCredential publicKeyCredential:
                    {
                        publicKeyCredential.Setup(this.jSRuntime, requestGuid);
                        break;
                    }
            }
        }

        public async Task PreventSilentAccessAsync()
        {
            var tcs = new TaskCompletionSource<object>();
            var requestId = Guid.NewGuid();
            pendingPreventSilentAccessRequests.Add(requestId, tcs);
            await jSRuntime
                .InvokeAsync<object>($"{jsNamespace}.PreventSilentAccess", requestId)
                .ConfigureAwait(false);
            await tcs.Task.ConfigureAwait(false);
        }

        [JSInvokable]
        public static Task ReceivePreventSilentAccessResponse(string requestId)
        {
            var requestGuid = Guid.Parse(requestId);
            pendingPreventSilentAccessRequests.TryGetValue(requestGuid, out TaskCompletionSource<object> tcs);
            tcs.SetResult(new { });
            pendingPreventSilentAccessRequests.Remove(requestGuid);
            return Task.CompletedTask;
        }

        public async Task<bool> IsUserVerifyingPlatformAuthenticatorAvailableAsync()
        {
            var tcs = new TaskCompletionSource<bool>();
            var requestId = Guid.NewGuid();
            pendingIsUserVerifyingPlatformAuthenticatorAvailableRequests.Add(requestId, tcs);
            await jSRuntime
                .InvokeAsync<bool>($"{jsNamespace}.IsUserVerifyingPlatformAuthenticatorAvailable", requestId)
                .ConfigureAwait(false);
            return await tcs.Task.ConfigureAwait(false);
        }

        [JSInvokable]
        public static Task ReceiveIsUserVerifyingPlatformAuthenticatorAvailableResponse(string requestId, bool response)
        {
            var requestGuid = Guid.Parse(requestId);
            pendingIsUserVerifyingPlatformAuthenticatorAvailableRequests.TryGetValue(requestGuid, out TaskCompletionSource<bool> tcs);
            tcs.SetResult(response);
            pendingIsUserVerifyingPlatformAuthenticatorAvailableRequests.Remove(requestGuid);
            return Task.CompletedTask;
        }
    }
}
