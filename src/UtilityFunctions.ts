/*eslint-disable @typescript-eslint/no-explicit-any */
export const removeEmpty = (obj: any): void =>
  Object.keys(obj).forEach((key): void => {
    if (obj[key] && typeof obj[key] === "object") removeEmpty(obj[key]);
    else if (obj[key] == null) delete obj[key];
  });
/*eslint-enable @typescript-eslint/no-explicit-any */

export const arrayBufferFromString = (input: string): ArrayBuffer => {
  return Uint8Array.from(atob(input), (c): number => c.charCodeAt(0));
};
