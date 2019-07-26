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
            return await tcs.Task.ConfigureAwait(false);
        }

        [JSInvokable]
        public static Task ReceiveGetResponse(string requestId, Credential? credential)
        {
            var requestGuid = Guid.Parse(requestId);
            pendingGetRequests.TryGetValue(requestGuid, out TaskCompletionSource<Credential?> tcs);
            tcs.SetResult(credential);
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
            return await tcs.Task.ConfigureAwait(false);
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
            return await tcs.Task.ConfigureAwait(false);
        }

        [JSInvokable]
        public static Task ReceiveCreateResponse(string requestId, Credential? credential)
        {
            var requestGuid = Guid.Parse(requestId);
            pendingCreateRequests.TryGetValue(requestGuid, out TaskCompletionSource<Credential?> tcs);
            tcs.SetResult(credential);
            pendingCreateRequests.Remove(requestGuid);
            return Task.CompletedTask;
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
    }
}
