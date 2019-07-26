/* eslint-disable @typescript-eslint/no-explicit-any */
export default interface DotNetInterop {
  invokeMethodAsync: (method: string, ...params: any[]) => any;
}

/* eslint-enable @typescript-eslint/no-explicit-any */
