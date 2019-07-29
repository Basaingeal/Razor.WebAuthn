declare type AttestationConveyancePreferenceEnum = AttestationConveyancePreference | 0 | 1 | 2;
declare type AuthenticatorAttachmentEnum = AuthenticatorAttachment | 0 | 1;
declare type AuthenticatorTransportEnum = AuthenticatorTransport | 0 | 1 | 2 | 3;
declare type CredentialMediationRequirementEnum = CredentialMediationRequirement | 0 | 1 | 2;
declare type PublicKeyCredentialTypeEnum = PublicKeyCredentialType | 0;
declare type UserVerificationRequirementEnum = UserVerificationRequirement | 0 | 1 | 2;

declare type ArrayBufferString = ArrayBuffer | string;

/*eslint-disable @typescript-eslint/no-explicit-any */
const removeEmpty = (obj: any): void =>
  // Object.fromEntries(
  //   Object.entries(obj)
  //     .filter(([, v]): boolean => v != null)
  //     .map(([k, v]): [string, unknown] => (typeof v === "object" ? [k, removeEmpty(v)] : [k, v]))
  // );
  Object.keys(obj).forEach((key): void => {
    if (obj[key] && typeof obj[key] === "object") removeEmpty(obj[key]);
    else if (obj[key] == null) delete obj[key];
  });
/*eslint-enable @typescript-eslint/no-explicit-any */

const arrayBufferFromString = (input: string): ArrayBuffer => {
  return Uint8Array.from(atob(input), (c): number => c.charCodeAt(0));
};

const authenticatorTransportIntToString = (at: AuthenticatorTransport): AuthenticatorTransport => {
  switch (at as AuthenticatorTransportEnum) {
    case 0: {
      return "usb";
    }
    case 1: {
      return "nfc";
    }
    case 2: {
      return "ble";
    }
    case 3: {
      return "internal";
    }
  }

  return "usb";
};

const authenticatorAttachmentIntToString = (
  aa: AuthenticatorAttachment
): AuthenticatorAttachment => {
  switch (aa as AuthenticatorAttachmentEnum) {
    case 0: {
      return "platform";
    }
    case 1: {
      return "cross-platform";
    }
  }

  return "platform";
};

const userVerificationRequirementIntToString = (
  uvr: UserVerificationRequirement
): UserVerificationRequirement => {
  const uvEnum = uvr as UserVerificationRequirementEnum;
  switch (uvEnum) {
    case 0: {
      return "required";
    }
    case 1: {
      return "preferred";
    }
    case 2: {
      return "discouraged";
    }
  }

  return "preferred";
};

const attestationConveyancePreferenceIntToString = (
  acp: AttestationConveyancePreference
): AttestationConveyancePreference => {
  switch (acp as AttestationConveyancePreferenceEnum) {
    case 0: {
      return "none";
    }
    case 1: {
      return "indirect";
    }
    case 2: {
      return "direct";
    }
  }
  return "none";
};

const credentialMediationRequirementIntToString = (
  cmr: CredentialMediationRequirement
): CredentialMediationRequirement => {
  switch (cmr as CredentialMediationRequirementEnum) {
    case 0: {
      return "silent";
    }
    case 1: {
      return "optional";
    }
    case 2: {
      return "required";
    }
  }

  return "optional";
};

const publicKeyCredentialTypeIntToString = (
  pkct: PublicKeyCredentialType
): PublicKeyCredentialType => {
  switch (pkct as PublicKeyCredentialTypeEnum) {
    case 0: {
      return "public-key";
    }
  }

  return "public-key";
};

const fixPublicKeyCredentialDescriptor = (pkcd: PublicKeyCredentialDescriptor): void => {
  pkcd.id = arrayBufferFromString((pkcd.id as ArrayBufferString) as string);

  if (pkcd.transports) {
    pkcd.transports.forEach((at, index): void => {
      if (pkcd.transports) {
        pkcd.transports[index] = authenticatorTransportIntToString(at);
      }
    });
  }

  pkcd.type = publicKeyCredentialTypeIntToString(pkcd.type);
};

export const fixPublicKeyCredentialCreationOptions = (
  options: PublicKeyCredentialCreationOptions
): PublicKeyCredentialCreationOptions => {
  removeEmpty(options);
  if (options.authenticatorSelection) {
    if (options.authenticatorSelection.authenticatorAttachment != null) {
      options.authenticatorSelection.authenticatorAttachment = authenticatorAttachmentIntToString(
        options.authenticatorSelection.authenticatorAttachment
      );
    }

    if (options.authenticatorSelection.userVerification != null) {
      options.authenticatorSelection.userVerification = userVerificationRequirementIntToString(
        options.authenticatorSelection.userVerification
      );
    }
  }

  if (options.attestation != null) {
    options.attestation = attestationConveyancePreferenceIntToString(options.attestation);
  }

  if (options.excludeCredentials) {
    options.excludeCredentials.forEach(fixPublicKeyCredentialDescriptor);
  }

  options.challenge = arrayBufferFromString((options.challenge as ArrayBufferString) as string);

  options.pubKeyCredParams.forEach((pkcp): void => {
    pkcp.type = publicKeyCredentialTypeIntToString(pkcp.type);
  });

  options.user.id = arrayBufferFromString((options.user.id as ArrayBufferString) as string);

  if (options.extensions && options.extensions.authnSel) {
    options.extensions.authnSel = options.extensions.authnSel.map(
      (authSel): ArrayBuffer => arrayBufferFromString((authSel as ArrayBufferString) as string)
    );
  }

  return options;
};

export const fixPublicKeyCredentialRequestOptions = (
  options: PublicKeyCredentialRequestOptions
): PublicKeyCredentialRequestOptions => {
  removeEmpty(options);

  if (options.allowCredentials) {
    options.allowCredentials.forEach(fixPublicKeyCredentialDescriptor);
  }

  if (options.userVerification != null) {
    options.userVerification = userVerificationRequirementIntToString(options.userVerification);
  }

  return options;
};

export const fixCredentialRequestOptions = (
  options: CredentialRequestOptions
): CredentialRequestOptions => {
  if (options.publicKey) {
    fixPublicKeyCredentialRequestOptions(options.publicKey);
  }

  if (options.mediation != null) {
    options.mediation = credentialMediationRequirementIntToString(options.mediation);
  }

  return options;
};
