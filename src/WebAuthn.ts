import DotNetInterop from "./DotNetInterop";

declare const DotNet: DotNetInterop;

declare global {
  interface Window {
    CurrieTechnologies: {
      Razor: {
        WebAuthn: any;
      };
    };
  }
}

const namespace = "CurrieTechnologies.Razor.WebAuthn";
window.CurrieTechnologies = window.CurrieTechnologies || {};
window.CurrieTechnologies.Razor = window.CurrieTechnologies.Razor || {};
window.CurrieTechnologies.Razor.WebAuthn = window.CurrieTechnologies.Razor.WebAuthn || {};

const currieWebAuthn = window.CurrieTechnologies.Razor.WebAuthn;
