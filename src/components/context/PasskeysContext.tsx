import React, { FC, ReactNode, createContext, useContext, useEffect, useState } from "react";
import { authenticate, getStoredPasskeys } from "../../passkeys/types";

type PasskeysContextValue = {
  currentPasskeyInfo: {address: string, fid: number, username: string, message: string, signature: string, pfpUrl: string | undefined} | undefined;
  showPasskeyPrompt: {fid: number, username: string, message: string, signature: string, pfpUrl: string | undefined, value: boolean};
  setShowPasskeyPrompt: (state: {address: string, fid: number, username: string, message: string, signature: string, pfpUrl: string | undefined, value: boolean}) => void;
  passkeyRegistrationComplete?: boolean;
  setPasskeyRegistrationComplete: (value: boolean | undefined) => void;
  passkeyRegistrationError?: string;
  setPasskeyRegistrationError: (value: string) => void;
  signWithPasskey: (credentialId: string, payload: string) => Promise<string>;
};

type PasskeysContextProps = {
  children: ReactNode;
};

const PasskeysProvider: FC<PasskeysContextProps> = ({ children }) => {
  const [currentPasskeyInfo, setCurrentPassKeyInfo] = useState<{address: string, fid: number, username: string, message: string, signature: string, pfpUrl: string | undefined}>();
  const [showPasskeyPrompt, setShowPasskeyPrompt] = useState<{fid: number, username: string, message: string, signature: string, pfpUrl: string | undefined, value: boolean}>({fid: 0, username: "", signature: "", message: "", pfpUrl: "", value: false});
  const [passkeyRegistrationComplete, setPasskeyRegistrationComplete] = useState<boolean | undefined>();
  const [passkeyRegistrationError, setPasskeyRegistrationError] = useState<string>();
  const signWithPasskey = async (credentialId: string, payload: string) => {
    const cred = await authenticate({credentialId});
    // @ts-expect-error it's in go
    return signPayload(cred.largeBlob, payload);
  }

  useEffect(() => {
    // @ts-expect-error it's in go
    const go = new Go();
    WebAssembly.instantiateStreaming(fetch("/main.wasm"), go.importObject).then(result => {
      go.run(result.instance);
    })

    getStoredPasskeys().then(p => {
      if (p.length > 0) {
        setCurrentPassKeyInfo({...(p[0].additionalData!), address: p[0].address});
        setPasskeyRegistrationComplete(true);
      }
    })
  }, []);

  return (
    <PasskeysContext.Provider value={{
      currentPasskeyInfo,
      showPasskeyPrompt,
      setShowPasskeyPrompt,
      passkeyRegistrationComplete,
      setPasskeyRegistrationComplete,
      passkeyRegistrationError,
      setPasskeyRegistrationError,
      signWithPasskey,
    }}>
      {children}
    </PasskeysContext.Provider>
  );
};


const PasskeysContext = createContext<PasskeysContextValue>({
  currentPasskeyInfo: undefined,
  showPasskeyPrompt: undefined as never,
  setShowPasskeyPrompt: () => undefined,
  passkeyRegistrationComplete: undefined,
  setPasskeyRegistrationComplete: () => undefined,
  passkeyRegistrationError: undefined,
  setPasskeyRegistrationError: () => undefined,
  signWithPasskey: async () => undefined as unknown as Promise<string>,
});

const usePasskeysContext = () => useContext(PasskeysContext);

export { PasskeysProvider, usePasskeysContext };