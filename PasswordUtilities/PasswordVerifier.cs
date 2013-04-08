// Copyright (c) 2013 Mark Pearce
// http://opensource.org/licenses/MIT

using System;
using System.Text;

namespace PasswordUtilities
{
    /// <summary>
    /// This class is used to check whether a specified 
    /// password matches a previously-stored hash.
    /// </summary>
    public static class PasswordVerifier
    {
        /// <summary>
        /// Verifies that the specified password matches the specified hash.
        /// </summary>
        /// <param name="password">
        /// The password that needs to be verified in the form of a Unicode (UTF-8) string.
        /// </param>
        /// <param name="storedHash">
        /// ASCII string in the format ".AAAA.BBBB.NNNN.XXXX.YYYY" 
        /// where . = delimiter character, AAAA = hash algorithm,
        /// BBBB = encoding format, NNNN = work factor, 
        /// XXXX = encoded password salt, YYYY = encoded password hash.
        /// </param>
        /// <returns>
        /// Whether the specified password matches the specified hash.
        /// </returns>
        public static bool PasswordVerify(string password, string storedHash)
        {
            return PasswordVerify(Encoding.UTF8.GetBytes(password), storedHash);
        }

        /// <summary>
        /// Verifies that the specified password matches the specified hash.
        /// </summary>
        /// <param name="password">
        /// The password that needs to be verified in the form of a Unicode (UTF-8) byte array.
        /// </param>
        /// <param name="storedHash">
        /// ASCII string in the format ".AAAA.BBBB.NNNN.XXXX.YYYY" 
        /// where . = delimiter character, AAAA = hash algorithm,
        /// BBBB = encoding format, NNNN = work factor, 
        /// XXXX = encoded password salt, YYYY = encoded password hash.
        /// </param>
        /// <returns>
        /// Whether the specified password matches the specified hash.
        /// </returns>
        public static bool PasswordVerify(byte[] password, string storedHash)
        {
            // Decode the stored hash.
            HashStorage store = new HashStorage();
            store.HashDecode(storedHash);
            // Generate new hash based on details extracted from stored hash.
            HashGenerator hashGen = new HashGenerator(new HashPolicy(store.HashMethod, store.WorkFactor, store.HashStorageFormat, store.PasswordSalt.Length));
            hashGen.CreatePasswordHash(password, store.PasswordSalt);
            // Does the stored hash match the new hash?
            return (storedHash == hashGen.StoredHash);
        }
    }
}