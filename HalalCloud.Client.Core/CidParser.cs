﻿namespace HalalCloud.Client.Core
{
    public class CidParser
    {
        /// <summary>
        /// The type of multibase encoding.
        /// </summary>
        public enum MultibaseEncoding
        {
            /// <summary>
            /// No base encoding
            /// </summary>
            None,

            /// <summary>
            /// Unsupported base encoding
            /// </summary>
            Unsupported,

            /// <summary>
            /// Hexadecimal (lowercase)
            /// </summary>
            Base16,

            /// <summary>
            /// Hexadecimal (uppercase)
            /// </summary>
            Base16Upper,

            /// <summary>
            /// RFC4648 case-insensitive - no padding (lowercase)
            /// </summary>
            Base32,

            /// <summary>
            /// RFC4648 case-insensitive - no padding (uppercase)
            /// </summary>
            Base32Upper,

            /// <summary>
            /// Base58 Bitcoin
            /// </summary>
            Base58Btc,

            /// <summary>
            /// RFC4648 no padding
            /// </summary>
            Base64,

            /// <summary>
            /// RFC4648 no padding
            /// </summary>
            Base64Url,

            /// <summary>
            /// RFC4648 with padding
            /// </summary>
            Base64UrlPad
        }

        /// <summary>
        /// Parse an integer from a byte array with unsigned varint (VARiable
        /// INTeger) format.
        /// </summary>
        /// <param name="InputBytes">
        /// The byte array with unsigned varint (VARiable INTeger) format.
        /// </param>
        /// <returns>The parsed integer.</returns>
        /// <see cref="https://github.com/multiformats/unsigned-varint"/>
        /// <remarks>
        /// For security, to avoid memory attacks, we use a "practical max" of
        /// 9 bytes. Though there is no theoretical limit, and future specs can
        /// grow this number if it is truly necessary to have code or length
        /// values equal to or larger than 2^63.
        /// </remarks>
        public static (long Integer, int Parsed) ParseUnsignedVarint(
            byte[] InputBytes)
        {
            const int MaximumBytes = 9;

            (long Integer, int Parsed) Result = (0, 0);
            for (int i = 0; i < InputBytes.Length && i < MaximumBytes; ++i)
            {
                Result.Integer |=
                    (long)(InputBytes[i] & 0x7F) << (Result.Parsed * 7);
                ++Result.Parsed;
                if ((InputBytes[i] & 0x80) == 0)
                {
                    break;
                }
            }

            return Result;
        }
    }
}
