import React, { useState } from "react";
import { completeRegistration, register } from "../../passkeys/types";
import { usePasskeysContext } from "../context/PasskeysContext";

export const PasskeyModal = () => {
  const {
    showPasskeyPrompt, setShowPasskeyPrompt,
    passkeyRegistrationComplete, setPasskeyRegistrationComplete,
    passkeyRegistrationError, setPasskeyRegistrationError,
  } = usePasskeysContext();
  const [keypair, setKeypair] = useState<string | undefined>();
  const [id, setId] = useState<string | undefined>();
  
  return <div className={"fixed text-stone dark:text-white top-0 left-0 z-[10000] backdrop-blur-md transition ease-in-out duration-600 w-full h-full bg-stone-900/40 dark:bg-stone-800/20" + (showPasskeyPrompt.value ? "" : " hidden")}>
    <div className="absolute text-center top-0 left-0 w-full md:left-1/3 md:w-1/3 border border-t-0 border-stone-300/20 bg-stone-400/30 dark:bg-stone-300/30 drop-shadow-2xl rounded-none md:rounded-b-2xl">
      <h2 className="text-center p-4 font-light text-xl">Create Passkey</h2>
      <div className={"relative z-100 w-[60px] h-[60px] inline-block text-[23pt] font-bold transition ease-in-out duration-300 mb-4 border border-stone-100/30 rounded-full p-2" + (passkeyRegistrationComplete === true ? " bg-green-600 border-green-200" : passkeyRegistrationComplete === false ? " bg-red-600 border-red-300 font-normal" : " bg-[url('/public/passkey.png')] bg-cover pulsating")}>
        {passkeyRegistrationComplete === true ? "✓" : passkeyRegistrationComplete === false ? "!" : ""}
      </div>
      <div className="mb-4 mx-4">{
        passkeyRegistrationComplete === false && passkeyRegistrationError
          ? <>
              <div>An error was encountered while attempting to register the passkey.</div>
              <div
                className="border-stone-800/20 text-xs border bg-stone-800/20 p-4 mt-4 rounded-xl"
                style={{fontFamily: 'ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, "Liberation Mono", "Courier New", monospace'}}>
                  {passkeyRegistrationError}
              </div>
              <div className="mt-4">If your browser told you the passkey option cannot be used with the site, you may be running an unsupported browser. If the browser provides an option to use a phone for passkeys, use this.</div>
            </>
          : passkeyRegistrationComplete === true
            ? "Your passkey has been successfully created."
            : id 
                ? "To save the account, you will need to perform one more passkey interaction. Tap continue to complete."
                : "Use Passkeys to access your account, with the security of your own device's secure element. This will require two round-trips with your authenticator to complete to fully register the account. If you have an existing Q Console passkey not associated with a Farcaster account, this will be linked if detected."
      }</div>
      <div onClick={async () => {
        if (passkeyRegistrationComplete) {
          setShowPasskeyPrompt({...showPasskeyPrompt, address: "", value: false});
        } else if (id) {
          try {
            let pair = "";
            if (keypair) {
              pair = keypair;
            } else {
              // @ts-expect-error call from go
              pair = createEd448Keypair();
              setKeypair(pair);
            }
            const p = JSON.parse(keypair!);
            await completeRegistration({credentialId: id, largeBlob: Buffer.from(p.PrivateKey).toString('hex'), publicKey: Buffer.from(p.PublicKey).toString('hex'), address: p.Address, additionalData: showPasskeyPrompt});
            setPasskeyRegistrationComplete(true);
            setKeypair(undefined);
            setId(undefined);
          // eslint-disable-next-line @typescript-eslint/no-explicit-any
          } catch (e: any) {
            setPasskeyRegistrationComplete(false);
            setPasskeyRegistrationError(e.toString());
          }
        } else {
          try {
            let pair = "";
            if (keypair) {
              pair = keypair;
            } else {
              // @ts-expect-error call from go
              pair = createEd448Keypair();
              setKeypair(pair);
            }
            const r = await register(showPasskeyPrompt.username);
            setId(r.id);
          // eslint-disable-next-line @typescript-eslint/no-explicit-any
          } catch (e: any) {
            setPasskeyRegistrationComplete(false);
            setPasskeyRegistrationError(e.toString());
          }
        }
      }} className="border-stone-300/30 bg-stone-300/30 hover:bg-stone-100/30 transition ease-in-out duration-300 cursor-pointer rounded-xl px-2 p-1 border flex-col mt-2 mb-4 mx-4">Continue</div>
      {!passkeyRegistrationComplete && <div onClick={async () => {
        setShowPasskeyPrompt({...showPasskeyPrompt, address: "", value: false});
      }} className="border-stone-300/30 bg-stone-800/30 hover:bg-stone-100/30 transition ease-in-out duration-300 cursor-pointer rounded-xl px-2 p-1 border flex-col mt-2 mb-4 mx-4">Cancel</div>}
    </div>
  </div>;
}