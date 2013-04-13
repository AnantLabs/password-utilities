// Copyright (c) 2013 Mark Pearce
// http://opensource.org/licenses/MIT

using System;
using System.Security.Cryptography;

namespace PasswordUtilities
{
    // Creates the SHA-1 hash as a specified number of bytes (normally 160 bits),
    // using a specified password, salt, and number of hash iterations via PBKDF2.
    // See http://en.wikipedia.org/wiki/PBKDF2
    // And http://msdn.microsoft.com/en-us/library/system.security.cryptography.rfc2898derivebytes%28v=VS.100%29.aspx
    internal static class SHA1_160
    {
        private const Int32 SHA1_160_HASH_SIZE_IN_BYTES = 20;

        internal static byte[] CreateSaltedHash(byte[] password, byte[] salt, Int32 hashIterations)
        {
            using (var hashBytes = new Rfc2898DeriveBytes(password, salt, hashIterations))
            {
                return hashBytes.GetBytes(SHA1_160_HASH_SIZE_IN_BYTES);
            }
        }
    }
}