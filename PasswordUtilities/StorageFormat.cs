// Copyright (c) 2013 Mark Pearce
// http://opensource.org/licenses/MIT

using System;

namespace PasswordUtilities
{
    /// <summary>
    /// List of password storage formats.
    /// </summary>
    /// <remarks>
    /// Currently only handles base64 and hexadecimal.
    /// </remarks>
    public enum StorageFormat
    {
        /// <summary>
        /// Store password salt and password hash as a hexadecimal string.
        /// Hex takes 2 characters to represent 1 byte, so is less space-efficient than Base64.
        /// </summary>
        Hexadecimal = 0,
        /// <summary>
        /// Store password salt and password hash as a base64 string.
        /// Base64 takes 4 characters to represent 3 bytes, so is more space-efficient than hex.
        /// </summary>
        Base64 = 1
    }
}