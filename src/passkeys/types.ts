export const getStoredPasskeys = async () => {
  const passkeysList = window.localStorage.getItem('passkeys-list');
  let passkeys: StoredPasskey[] = [];
  if (passkeysList) {
    passkeys = JSON.parse(passkeysList);
  }

  return passkeys;
}

export const updateStoredPasskey = async (
  credentialId: string,
  storedPasskey: StoredPasskey,
) => {
  const passkeysList = window.localStorage.getItem('passkeys-list');
  let passkeys: StoredPasskey[] = [];
  if (passkeysList) {
    passkeys = JSON.parse(passkeysList);
  }

  if (passkeys.filter((p) => p.credentialId === credentialId)) {
    passkeys = passkeys.filter((p) => p.credentialId !== credentialId);
  }

  passkeys.push(storedPasskey);

  window.localStorage.setItem(
    'passkeys-list',
    JSON.stringify(passkeys),
  );

  return true;
}

export const register = async (account: string) => {
  const challenge = new Uint8Array(32);
  crypto.getRandomValues(challenge);
  const credential = await navigator.credentials.create({
    publicKey: {
      challenge: challenge,
      rp: {
        name: 'Quilibrium',
        // id: request.rp.id,
      },
      user: {
        id: Buffer.from(account),
        name: account,
        displayName: account,
      },
      pubKeyCredParams: [{ alg: -7, type: 'public-key' }],
      authenticatorSelection: {
        userVerification: 'required',
        residentKey: 'required',
      },
      extensions: {
        // @ts-expect-error passkeys
        largeBlob: {
          support: 'required',
        },
      },
    },
  });
  if (!credential) {
    throw new Error('could not register passkey');
  }

  return {
    // @ts-expect-error passkeys
    id: Buffer.from(credential.rawId).toString('base64'),
    // @ts-expect-error passkeys
    rawId: Buffer.from(credential.rawId).toString('base64'),
  };
}

export const completeRegistration = async (
  request: PasskeyAuthenticationRequestLargeBlob,
) => {
  const challenge = new Uint8Array(32);
  crypto.getRandomValues(challenge);
  const write = await navigator.credentials.get({
    publicKey: {
      challenge: challenge,
      allowCredentials: [
        {
          id: Buffer.from(request.credentialId, 'base64'),
          type: 'public-key',
        },
      ],
      extensions: {
        // @ts-expect-error passkeys
        largeBlob: {
          write: Buffer.from(request.largeBlob, 'utf-8'),
        },
      },
    },
  });

  // @ts-expect-error passkeys
  if (write?.getClientExtensionResults().largeBlob.written) {
    updateStoredPasskey(request.credentialId, {
      credentialId: request.credentialId,
      address: request.address,
      publicKey: request.publicKey,
      additionalData: request.additionalData,
    })
    return {
      id: request.credentialId,
      rawId: request.credentialId,
      response: {
        authenticatorData: '',
        clientDataJSON: '',
        signature: '',
        userHandle: '',
      },
    };
  } else {
    throw new Error('could not add key to credential');
  }
}

export const authenticate = async (
  request: PasskeyAuthenticationRequest,
) => {
  const passkeysList = window.localStorage.getItem('passkeys-list');
  let passkeys: StoredPasskey[] = [];
  if (passkeysList) {
    passkeys = JSON.parse(passkeysList);
  }

  const passkey = passkeys.filter((p) => p.address === request.credentialId)[0];
  
  const challenge = new Uint8Array(32);
  crypto.getRandomValues(challenge);
  const credential = await navigator.credentials.get({
    publicKey: {
      challenge: challenge,
      allowCredentials: [
        {
          id: Buffer.from(passkey.credentialId, 'base64'),
          type: 'public-key',
        },
      ],
      extensions: {
        // @ts-expect-error passkeys
        largeBlob: {
          read: true,
        },
      },
    },
  });

  if (credential) {
    if (
      // @ts-expect-error passkeys
      typeof credential.getClientExtensionResults().largeBlob === 'undefined'
    ) {
      throw new Error('invalid authenticator');
    }
    const key = Buffer.from(
      // @ts-expect-error passkeys
      credential.getClientExtensionResults().largeBlob.blob,
    ).toString('utf-8');
    return {
      id: request.credentialId,
      rawId: request.credentialId,
      response: {
        authenticatorData: '',
        clientDataJSON: '',
        signature: '',
        userHandle: '',
      },
      largeBlob: key,
    };
  } else {
    throw new Error('could not authenticate');
  }
}

export const isPasskeysSupported = async () => {
  const matches =
    navigator.userAgent.match(
      /(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i,
    ) || [];
  if (/trident/i.test(matches[1])) {
    return false;
  }

  if (matches[1] === 'Chrome') {
    if (navigator.userAgent.match(/\b(OPR)\/(\d+)/) !== null) {
      return parseInt(matches[2], 10) >= 99;
    }
    return parseInt(matches[2], 10) >= 113;
  }

  if (
    matches[1] === 'Safari' &&
    navigator.userAgent.match(/version\/(\d+)/i) !== null
  ) {
    const versionMatch = navigator.userAgent.match(/version\/(\d+)/i);
    return versionMatch !== null && parseInt(versionMatch[1], 10) >= 17;
  }

  return false;
}

export interface PasskeyRegistrationResult {
  id: string;
}

export interface PasskeyAuthenticationRequestLargeBlob {
  credentialId: string;
  address: string;
  publicKey: string;
  largeBlob: string;
  additionalData?: {fid: number, username: string, message: string, signature: string, pfpUrl: string | undefined}
}

export interface PasskeyAuthenticationRequest {
  credentialId: string;
}

export interface PasskeyAuthenticationResult {
  id: string;
  rawId: string;
  type?: string;
  response: {
    authenticatorData: string;
    clientDataJSON: string;
    signature: string;
    userHandle: string;
  };
  largeBlob?: string;
}

export interface StoredPasskey {
  credentialId: string;
  address: string;
  publicKey: string;
  simulated?: boolean;
  privateKey?: string;
  additionalData?: {fid: number, username: string, message: string, signature: string, pfpUrl: string | undefined}
}