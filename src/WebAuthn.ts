import DotNetInterop from "./DotNetInterop";
import WebAuthnInterop from "./WebAuthnInterop";

declare const DotNet: DotNetInterop;

declare global {
  interface Window {
    CurrieTechnologies: {
      Razor: {
        WebAuthn: WebAuthnInterop;
      };
    };
  }
}

const namespace = "CurrieTechnologies.Razor.WebAuthn";
window.CurrieTechnologies = window.CurrieTechnologies || {};
window.CurrieTechnologies.Razor = window.CurrieTechnologies.Razor || {};
window.CurrieTechnologies.Razor.WebAuthn = window.CurrieTechnologies.Razor.WebAuthn || {};

const dispatchGetCredential = async (
  requestId: string,
  credential: Credential | null
): Promise<void> => {
  await DotNet.invokeMethodAsync(namespace, "ReceiveGetResponse", requestId, credential);
};

const dispatchStoreCredential = async (
  requestId: string,
  credential: Credential
): Promise<void> => {
  await DotNet.invokeMethodAsync(namespace, "ReceiveStoreResponse", requestId, credential);
};

const dispatchCreateCredential = async (
  requestId: string,
  credential: Credential | null
): Promise<void> => {
  await DotNet.invokeMethodAsync(namespace, "ReceiveCreateResponse", requestId, credential);
};

const dispatchPreventSilentAccess = async (requestId: string): Promise<void> => {
  await DotNet.invokeMethodAsync(namespace, "ReceivePreventSilentAccessResponse", requestId);
};

const currieWebAuthn = window.CurrieTechnologies.Razor.WebAuthn;

currieWebAuthn.Get = async (
  requestId: string,
  options?: CredentialRequestOptions
): Promise<void> => {
  const credential = await window.navigator.credentials.get(options);
  await dispatchGetCredential(requestId, credential);
};

currieWebAuthn.Store = async (requestId: string, credential: Credential): Promise<void> => {
  const storedCredential = await window.navigator.credentials.store(credential);
  await dispatchStoreCredential(requestId, storedCredential);
};

currieWebAuthn.Create = async (
  requestId: string,
  options?: CredentialCreationOptions
): Promise<void> => {
  const credential = await window.navigator.credentials.create(options);
  await dispatchCreateCredential(requestId, credential);
};

currieWebAuthn.PreventSilentAccess = async (requestId: string): Promise<void> => {
  await window.navigator.credentials.preventSilentAccess();
  await dispatchPreventSilentAccess(requestId);
};
