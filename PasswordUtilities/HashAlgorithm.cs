// Copyright (c) 2013 Mark Pearce
// http://opensource.org/licenses/MIT

using System;

namespace PasswordUtilities
{
    /// <summary>
    /// List of password hash algorithms.
    /// </summary>
    /// <remarks>
    /// Only SHA-1 is currently implemented.
    /// </remarks>
    public enum HashAlgorithm
    {
        /// <summary>
        /// SHA-1 has a 160-bit (20 bytes) message digest.
        /// There is an interesting collision attack 
        /// against SHA-1, but this has no implication 
        /// for password storage.
        /// </summary>
        Sha1_160 = 0,
        /// <summary>
        /// SHA-2/256 has a 256-bit (32 bytes) digest.
        /// </summary>
        Sha2_256 = 1,
        /// <summary>
        /// SHA-3/512 (Keccak) has a 512-bit (64 bytes) digest.
        /// 512 bits needs 64-bit arithmetic, which is
        /// fine for a CPU but not for current GPUs.
        /// </summary>
        Sha3_512 = 2,
        /// <summary>
        /// BCRYPT is based on Blowfish rather than the SHA-X 
        /// algorithms. It's designed to be computationally 
        /// intensive and specifically slower on a GPU than 
        /// the combination of SHA-X + PBKDF2.
        /// BCRYPT has a 192-bit message digest, but can only 
        /// handle a maximum of 55 password characters. 
        /// It expects a 128-bit salt encoded in a Base64
        /// format (so 22 characters).
        /// </summary>
        Bcrypt_192 = 3,
        /// <summary>
        /// BCRYPT is based on Blowfish rather than the SHA-X 
        /// algorithms. It's designed to be both computationally 
        /// intensive and memory-intensive.
        /// In this library SCRYPT has a 512-bit message digest.
        /// </summary>
        Scrypt_512 = 4
    }
}