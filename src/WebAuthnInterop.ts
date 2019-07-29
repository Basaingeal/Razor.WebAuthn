export default interface WebAuthnInterop {
  Get: (requestId: string, options?: CredentialRequestOptions) => Promise<void>;
  Store: (requestId: string, credential: Credential) => Promise<void>;
  Create: (requestId: string, options?: CredentialCreationOptions) => Promise<void>;
  PreventSilentAccess: (requestId: string) => Promise<void>;
  IsUserVerifyingPlatformAuthenticatorAvailable: (requestId: string) => Promise<void>;
  GetClientExtensionResults: (clientSideId: string) => AuthenticationExtensionsClientOutputs;
  GetTransports: (clientSideId: string) => AuthenticatorTransport[];
}
