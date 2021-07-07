// <copyright file="KMSRSAProvider.cs" company="Howler Team">
// Copyright (c) Howler Team. All rights reserved.
// Licensed under the Server Side Public License.
// See LICENSE file in the project root for full license information.
// </copyright>
// <author>Cassandra A. Heart</author>

namespace Howler.AuthGateway.CryptographyProviders
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using Amazon.KeyManagementService;
    using Amazon.KeyManagementService.Model;

    /// <summary>
    /// Provides RSA from KMS.
    /// </summary>
    public class KMSRSAProvider : IRSAProvider
    {
        private byte[] oid =
        {
            0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86,
            0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00,
        };

        private IAmazonKeyManagementService _kms;

        /// <summary>
        /// Initializes a new instance of the <see cref="KMSRSAProvider"/>
        /// class.
        /// </summary>
        /// <param name="kms">The injected KMS provider.</param>
        public KMSRSAProvider(IAmazonKeyManagementService kms)
        {
            this._kms = kms;
        }

        /// <inheritdoc/>
        public async Task<RSAParameters> GetPublicKeyAsync()
        {
            var result = await this._kms.GetPublicKeyAsync(
                new GetPublicKeyRequest
                {
                    KeyId = "alias/HowlerAuthGatewayKey",
                });

            var pubkey = result.PublicKey;
            BinaryReader br = new BinaryReader(result.PublicKey);

            // x509 DER => RSA parameters, see:
            // https://datatracker.ietf.org/doc/html/rfc3447
            switch (br.ReadUInt16())
            {
                case 0x8130:
                    br.ReadByte();
                    break;
                case 0x8230:
                    br.ReadInt16();
                    break;
                default:
                    throw new InvalidDataException();
            }

            var bytes = br.ReadBytes(15);
            if (!bytes.SequenceEqual(this.oid))
            {
                throw new InvalidDataException();
            }

            switch (br.ReadUInt16())
            {
                case 0x8103:
                    br.ReadByte();
                    break;
                case 0x8203:
                    br.ReadInt16();
                    break;
                default:
                    throw new InvalidDataException();
            }

            if (br.ReadByte() != 0x00)
            {
                throw new InvalidDataException();
            }

            switch (br.ReadUInt16())
            {
                case 0x8130:
                    br.ReadByte();
                    break;
                case 0x8230:
                    br.ReadInt16();
                    break;
                default:
                    throw new InvalidDataException();
            }

            byte lowbyte = 0x00;
            byte highbyte = 0x00;

            switch (br.ReadUInt16())
            {
                case 0x8102:
                    lowbyte = br.ReadByte();
                    break;
                case 0x8202:
                    highbyte = br.ReadByte();
                    lowbyte = br.ReadByte();
                    break;
                default:
                    throw new InvalidDataException();
            }

            var modulusSize = BitConverter.ToInt32(
                new byte[] { lowbyte, highbyte, 0x00, 0x00 },
                0);

            var firstbyte = br.ReadByte();
            br.BaseStream.Seek(-1, SeekOrigin.Current);

            if (firstbyte == 0x00)
            {
                br.ReadByte();
                modulusSize -= 1;
            }

            byte[] modulus = br.ReadBytes(modulusSize);

            if (br.ReadByte() != 0x02)
            {
                throw new InvalidDataException();
            }

            byte[] exponent = br.ReadBytes((int)br.ReadByte());

            var rsaParameters = new RSAParameters
            {
                Modulus = modulus,
                Exponent = exponent,
            };

            return rsaParameters;
        }

        /// <inheritdoc/>
        public async Task<byte[]> SignAsync(
            byte[] payload,
            SignatureAlgorithm signatureAlgorithm)
        {
            using (var ms = new MemoryStream(payload))
            {
                using (var os = new MemoryStream())
                {
                    var response = await this._kms.SignAsync(new SignRequest
                    {
                        KeyId = "alias/HowlerAuthGatewayKey",
                        Message = ms,
                        MessageType = MessageType.RAW,
                        SigningAlgorithm =
                            signatureAlgorithm == SignatureAlgorithm.RS256 ?
                                SigningAlgorithmSpec.RSASSA_PKCS1_V1_5_SHA_256
                                : throw new InvalidOperationException(
                                    "Signature algorithm" +
                                    $" {signatureAlgorithm} unsupported."),
                    });

                    await response.Signature.CopyToAsync(os);

                    return os.ToArray();
                }
            }
        }
    }
}