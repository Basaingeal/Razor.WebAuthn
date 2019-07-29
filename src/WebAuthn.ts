import DotNetInterop from "./DotNetInterop";
import WebAuthnInterop from "./WebAuthnInterop";
import { fixPublicKeyCredentialCreationOptions, fixCredentialRequestOptions } from "./ObjectFixes";

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

declare interface AuthenticatorAttestationResponse extends AuthenticatorResponse {
  getTransports: () => AuthenticatorTransport[];
}

const namespace = "CurrieTechnologies.Razor.WebAuthn";
window.CurrieTechnologies = window.CurrieTechnologies || {};
window.CurrieTechnologies.Razor = window.CurrieTechnologies.Razor || {};
window.CurrieTechnologies.Razor.WebAuthn = window.CurrieTechnologies.Razor.WebAuthn || {};

const credentialStore: Map<string, Credential> = new Map<string, Credential>();

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

const dispatchIsUserVerifyingPlatformAuthenticatorAvailable = async (
  requestId: string,
  result: boolean
): Promise<void> => {
  await DotNet.invokeMethodAsync(
    namespace,
    "ReceiveIsUserVerifyingPlatformAuthenticatorAvailableResponse",
    requestId,
    result
  );
};

const currieWebAuthn = window.CurrieTechnologies.Razor.WebAuthn;

currieWebAuthn.Get = async (
  requestId: string,
  options?: CredentialRequestOptions
): Promise<void> => {
  if (options) {
    fixCredentialRequestOptions(options);
  }

  console.log(options);

  const credential = await window.navigator.credentials.get(options);

  if (credential) {
    credentialStore.set(requestId, credential);
  }

  await dispatchGetCredential(requestId, credential);
};

currieWebAuthn.Store = async (requestId: string, credential: Credential): Promise<void> => {
  const storedCredential = await window.navigator.credentials.store(credential);

  credentialStore.set(requestId, storedCredential);

  await dispatchStoreCredential(requestId, storedCredential);
};

currieWebAuthn.Create = async (
  requestId: string,
  options?: CredentialCreationOptions
): Promise<void> => {
  if (options && options.publicKey) {
    fixPublicKeyCredentialCreationOptions(options.publicKey);
  }

  console.log(options);

  const credential = await window.navigator.credentials.create(options);

  if (credential) {
    credentialStore.set(requestId, credential);
  }

  await dispatchCreateCredential(requestId, credential);
};

currieWebAuthn.PreventSilentAccess = async (requestId: string): Promise<void> => {
  await window.navigator.credentials.preventSilentAccess();
  await dispatchPreventSilentAccess(requestId);
};

currieWebAuthn.IsUserVerifyingPlatformAuthenticatorAvailable = async (
  requestId: string
): Promise<void> => {
  const result = await PublicKeyCredential.isUserVerifyingPlatformAuthenticatorAvailable();
  await dispatchIsUserVerifyingPlatformAuthenticatorAvailable(requestId, result);
};

currieWebAuthn.GetClientExtensionResults = (
  clientSideId: string
): AuthenticationExtensionsClientOutputs => {
  const credential = credentialStore.get(clientSideId);
  if (credential) {
    const pkCredential = credential as PublicKeyCredential;
    return pkCredential.getClientExtensionResults();
  }
  return {};
};

currieWebAuthn.GetTransports = (clientSideId: string): AuthenticatorTransport[] => {
  const credential = credentialStore.get(clientSideId);
  if (credential) {
    const pkCredential = credential as PublicKeyCredential;
    const authenticatorAttestationResponse = pkCredential.response as AuthenticatorAttestationResponse;
    return authenticatorAttestationResponse.getTransports();
  }

  return [];
};
