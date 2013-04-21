// Copyright (c) 2013 Mark Pearce
// http://opensource.org/licenses/MIT

using System;
using System.Security.Cryptography;

namespace PasswordUtilities
{
    // Creates the SHA-2 hash as a specified number of bytes (normally 256 bits),
    // using a specified password, salt, and number of hash iterations via PBKDF2.
    // See http://en.wikipedia.org/wiki/PBKDF2
    // For the HMAC function, see RFC 2104
    // http://www.ietf.org/rfc/rfc2104.txt
    internal static class SHA2_256
    {
        private const int SHA2_256_BLOCK_SIZE_IN_BYTES = 64;  // SHA2-256 uses a 512-bit block size.
        private const int SHA2_256_HASH_SIZE_IN_BYTES = 32;   // SHA2-256 has a 256-bit message digest.

        private const byte INNER_HASH_PADDING = 0x36;
        private const byte OUTER_HASH_PADDING = 0x5C;
        private const byte BLOCK_INDEX = 4;
        private const UInt32 BLOCK_COUNT = 2;

        internal static byte[] CreateSaltedHash(byte[] password, byte[] salt, Int32 hashIterations)
        {
            // Seed for the pseudo-random fcn: salt + block index.
            byte[] saltAndIndex = new byte[salt.Length + BLOCK_INDEX];
            Array.Copy(salt, 0, saltAndIndex, 0, salt.Length);

            byte[] hashOutput = new byte[BLOCK_COUNT * SHA2_256_HASH_SIZE_IN_BYTES];
            int hashOutputOffset = 0;

            SHA256Managed hashInner = new SHA256Managed();
            SHA256Managed hashOuter = new SHA256Managed();

            // For HMAC the key must be hashed or padded with zeros so
            // that it fits into a single block of the hash algorithm being used.
            if (password.Length > SHA2_256_BLOCK_SIZE_IN_BYTES)
            {
                password = hashInner.ComputeHash(password);
            }
            byte[] key = new byte[SHA2_256_BLOCK_SIZE_IN_BYTES];
            Array.Copy(password, 0, key, 0, password.Length);

            byte[] keyInner = new byte[SHA2_256_BLOCK_SIZE_IN_BYTES];
            byte[] keyOuter = new byte[SHA2_256_BLOCK_SIZE_IN_BYTES];
            for (Int32 i = 0; i < SHA2_256_BLOCK_SIZE_IN_BYTES; i++)
            {
                keyInner[i] = (byte)(key[i] ^ INNER_HASH_PADDING);
                keyOuter[i] = (byte)(key[i] ^ OUTER_HASH_PADDING);
            }

            // For each block of desired output...
            for (Int32 hashBlock = 0; hashBlock < BLOCK_COUNT; hashBlock++)
            {
                // Seed HMAC with salt & block index.
                IncrementInteger(saltAndIndex, salt.Length);
                byte[] hmacResult = saltAndIndex;

                for (Int32 i = 0; i < hashIterations; i++)
                {
                    // Simple implementation of HMAC-SHA2-256.
                    hashInner.Initialize();
                    hashInner.TransformBlock(keyInner, 0, SHA2_256_BLOCK_SIZE_IN_BYTES, keyInner, 0);
                    hashInner.TransformFinalBlock(hmacResult, 0, hmacResult.Length);

                    byte[] temp = hashInner.Hash;

                    hashOuter.Initialize();
                    hashOuter.TransformBlock(keyOuter, 0, SHA2_256_BLOCK_SIZE_IN_BYTES, keyOuter, 0);
                    hashOuter.TransformFinalBlock(temp, 0, temp.Length);
                    hmacResult = hashOuter.Hash; 

                    // XOR result into output buffer.
                    XorByteArray(hmacResult, 0, hashOutput, hashOutputOffset);
                }
                hashOutputOffset += SHA2_256_HASH_SIZE_IN_BYTES;
            }
            byte[] result = new byte[SHA2_256_HASH_SIZE_IN_BYTES];
            Array.Copy(hashOutput, 0, result, 0, SHA2_256_HASH_SIZE_IN_BYTES);
            return result;
        }

        // Treat the four bytes starting at buffer[bufferOffset]
        // as a big-endian integer, and increment it.
        private static void IncrementInteger(byte[] buffer, int bufferOffset)
        {
            unchecked
            {
                if (++buffer[bufferOffset + 3] == 0)
                {
                    if (++buffer[bufferOffset + 2] == 0)
                    {
                        if (++buffer[bufferOffset + 1] == 0)
                        {
                            if (++buffer[bufferOffset + 0] == 0)
                            {
                                throw new OverflowException();
                            }
                        }
                    }
                }
            }
        }

        private static void XorByteArray(byte[] source, int sourceOffset, byte[] destination, int destinationOffset)
        {
            int end = checked(sourceOffset + SHA2_256_HASH_SIZE_IN_BYTES);
            while (sourceOffset != end)
            {
                destination[destinationOffset] ^= source[sourceOffset];
                sourceOffset++;
                destinationOffset++;
            }
        }
    }
}