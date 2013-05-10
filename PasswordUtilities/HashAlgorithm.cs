// Copyright (c) 2013 Mark Pearce
// http://opensource.org/licenses/MIT

using System;

namespace PasswordUtilities
{
    /// <summary>
    /// List of password hash algorithms.
    /// </summary>
    /// <remarks>
    /// Only SHA-1 and SHA-2 are currently implemented.
    /// </remarks>
    public enum HashAlgorithm
    {
        /// <summary>
        /// SHA-1 has a 160-bit message digest.
        /// </summary>
        /// <remarks>
        /// There is an interesting collision attack 
        /// against SHA-1, but this has no implication 
        /// for password storage.
        /// </remarks>
        SHA1_160 = 0,
        /// <summary>
        /// SHA-2/256 has a 256-bit digest.
        /// </summary>
        SHA2_256 = 1,
        /// <summary>
        /// SHA-3/512 (Keccak) has a 512-bit digest.
        /// </summary>
        /// <remarks>
        /// 512 bits needs 64-bit arithmetic, which is
        /// fine for a CPU but not for current GPUs.
        /// </remarks>
        SHA3_512 = 2,
        /// <summary>
        /// BCRYPT has a 192-bit message digest, but can only 
        /// handle a maximum of 55 password characters. 
        /// </summary>
        /// <remarks>
        /// It's based on Blowfish rather than the SHA-X 
        /// algorithms. It's designed to be computationally 
        /// intensive and specifically slower on a GPU than 
        /// the combination of SHA-X + PBKDF2.
        /// It uses a 128-bit salt encoded in a Unix-specific
        /// Base64 format.
        /// </remarks>
        BCRYPT_192 = 3,
        /// <summary>
        /// In this library SCRYPT has a 512-bit message digest.
        /// </summary>
        /// <remarks>
        /// It's based on PKDBF2 and SALSA80, but designed 
        /// to be difficult to attack with GPUs or FPGAs. 
        /// It's tweakable to be either computationally-intensive,
        /// memory-intensive, or both.
        /// </remarks>
        SCRYPT_512 = 4
    }
}