import * as React from 'react';
import './Login.scss';
import Button from './Button';
import { usePasskeysContext } from './context/PasskeysContext';
import { PasskeyModal } from './modals/PasskeyModal';
import Input from './Input';

export const Login: React.FC<{}> = ({}) => {
  const [username, setUsername] = React.useState<string>();
  const { setShowPasskeyPrompt, signWithPasskey, currentPasskeyInfo } = usePasskeysContext();
  console.log(currentPasskeyInfo);
  return <>
    <PasskeyModal/>
    <div className="login-pane">
      <div/>
      <div className="sign-in">
        <div/>
        <div className="sign-in-title">
          <img src="/howler.png" alt="Howler"/> Howler
        </div>
        {!currentPasskeyInfo &&
          <div className="sign-in-passkeys">
            <Input onChange={(e) => setUsername(e.target.value)}/>
            <Button
              type="primary"
              disabled={!username}
              onClick={() => {
                if (username) {
                  setShowPasskeyPrompt({
                    address: "", fid: 0,
                    username: username, value: true,
                    message: '',
                    signature: '',
                    pfpUrl: undefined
                  });
                }
              }}>
                Create Account
            </Button>
          </div>
        }
        {currentPasskeyInfo && <div className="sign-in-passkeys">
            <Button
              type="primary"
              onClick={async () => {
                const result = await signWithPasskey(currentPasskeyInfo.address, "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff");
                console.log(result);
              }}>
                Sign In With Passkey
            </Button>
          </div>}
        <div/>

      </div>
      <div/>
  </div>
  </>;
};