// Copyright (c) 2013 Mark Pearce
// http://opensource.org/licenses/MIT

using System;
using System.Globalization;
using System.Diagnostics;
using System.Text;

namespace PasswordUtilities
{
    /// <summary>
    /// This class lets you encode a password hash and accompanying info for storage.
    /// It also lets you decode a stored password hash and accompanying info.
    /// Finally it lets you verify a password against a stored password hash. 
    /// </summary>
    internal sealed class HashStorage
    {
        // Type constants.
        private const string DEFAULT_STORAGE_DELIMITER = ".";
        private const Int32 CORRECT_NUMBER_OF_HASH_SECTIONS = 6;
        private const Int32 SECTION_HASH_ALGORITHM = 1;
        private const Int32 SECTION_STORAGE_FORMAT = 2;
        private const Int32 SECTION_WORK_FACTOR = 3;
        private const Int32 SECTION_ENCODED_SALT = 4;
        private const Int32 SECTION_ENCODED_HASH = 5;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public HashStorage()
        {
        }

        /// <summary>
        /// Algorithm used to generate the hash.
        /// </summary>
        public HashAlgorithm HashMethod { get; private set; }

        /// <summary>
        /// Storage format used to encode the password 
        /// hash and its accompanying info.
        /// </summary>
        public StorageFormat HashStorageFormat { get; private set; }

        /// <summary>
        /// Number of hash iterations expressed as 2^WorkFactor.
        /// </summary>
        public Int32 WorkFactor { get; private set; }

        /// <summary>
        /// Password salt as a byte array.
        /// </summary>
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// Password hash as a byte array.
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Password salt encoded using the specified storage format.
        /// </summary>
        public string PasswordSaltEncoded
        {
            get
            {
                string hexSalt;
                switch (this.HashStorageFormat)
                {
                    // Hexadecimal.
                    case StorageFormat.Hexadecimal:
                        hexSalt = BitConverter.ToString(this.PasswordSalt);
                        return hexSalt.Replace("-", "");
                    // Base64.
                    case StorageFormat.Base64:
                        return Convert.ToBase64String(this.PasswordSalt);
                    // Invalid - default to hexadecimal.
                    default:
                        Debug.Assert(false, "Invalid storage format!");
                        hexSalt = BitConverter.ToString(this.PasswordSalt);
                        return hexSalt.Replace("-", "");
                }
            }
        }

        /// <summary>
        /// Password hash encoded using the specified storage format.
        /// </summary>
        public string PasswordHashEncoded
        {
            get
            {
                string hexHash; 
                switch (this.HashStorageFormat)
                {
                    // Hexadecimal.
                    case StorageFormat.Hexadecimal:
                        hexHash = BitConverter.ToString(this.PasswordHash);
                        return hexHash.Replace("-", "");
                    // Base64.
                    case StorageFormat.Base64:
                        return Convert.ToBase64String(this.PasswordHash);
                    // Invalid - default to hexadecimal.
                    default:
                        Debug.Assert(false, "Invalid storage format!");
                        hexHash = BitConverter.ToString(this.PasswordHash);
                        return hexHash.Replace("-", "");
                }
            }
        }

        /// <summary>
        /// ASCII string in the format ".AAAA.BBBB.NNNN.XXXX.YYYY" 
        /// where . = delimiter character, AAAA = hash algorithm,
        /// BBBB = encoding format, NNNN = work factor, 
        /// XXXX = encoded password salt, YYYY = encoded password hash.
        /// </summary>
        public string StoredHash
        {
            get
            {
                return String.Concat(DEFAULT_STORAGE_DELIMITER,
                                     this.HashMethod.ToString(), 
                                     DEFAULT_STORAGE_DELIMITER, 
                                     this.HashStorageFormat.ToString(),
                                     DEFAULT_STORAGE_DELIMITER,
                                     this.WorkFactor.ToString(CultureInfo.InvariantCulture),
                                     DEFAULT_STORAGE_DELIMITER,
                                     this.PasswordSaltEncoded,
                                     DEFAULT_STORAGE_DELIMITER,
                                     this.PasswordHashEncoded
                                    );
            }
        }

        /// <summary>
        /// Encodes everything needed to store a password hash 
        /// so that its associated password can be verified later.
        /// </summary>
        /// <param name="passwordSalt">
        /// The password salt in the form of a Unicode string.
        /// </param>
        /// <param name="passwordHash">
        /// The password hash in the form of a Unicode string.
        /// </param>
        /// <param name="hashPolicy">
        /// The hash policy. This is used to extract the hash 
        /// algorithm, the hash work factor, and the hash 
        /// storage format.
        /// </param>
        /// <returns>
        /// ASCII string in the format ".AAAA.BBBB.NNNN.XXXX.YYYY" 
        /// where . = delimiter character, AAAA = hash algorithm,
        /// BBBB = encoding format, NNNN = work factor, 
        /// XXXX = encoded password salt, YYYY = encoded password hash.
        /// </returns>
        public string HashEncode(string passwordSalt, string passwordHash, HashPolicy hashPolicy)
        {
            return this.HashEncode(Encoding.UTF8.GetBytes(passwordSalt), Encoding.UTF8.GetBytes(passwordHash), hashPolicy);
        }

        /// <summary>
        /// Encodes everything needed to store a password hash 
        /// so that its associated password can be verified later.
        /// </summary>
        /// <param name="passwordSalt">
        /// The password salt in the form of a byte array.
        /// </param>
        /// <param name="passwordHash">
        /// The password hash in the form of a byte array.
        /// </param>
        /// <param name="hashPolicy">
        /// The hash policy. This is used to extract the hash 
        /// algorithm, the hash work factor, and the hash 
        /// storage format.
        /// </param>
        /// <returns>
        /// ASCII string in the format ".AAAA.BBBB.NNNN.XXXX.YYYY" 
        /// where . = delimiter character, AAAA = hash algorithm,
        /// BBBB = encoding format, NNNN = work factor, 
        /// XXXX = encoded password salt, YYYY = encoded password hash.
        /// </returns>
        public string HashEncode(byte[] passwordSalt, byte[] passwordHash, HashPolicy hashPolicy)
        {
            this.PasswordSalt = ValidatePasswordSalt(passwordSalt);
            this.PasswordHash = ValidatePasswordHash(passwordHash);
            this.HashMethod = hashPolicy.HashMethod; 
            this.WorkFactor = hashPolicy.WorkFactor;
            this.HashStorageFormat = hashPolicy.HashStorageFormat;
            return this.StoredHash;
        }

        /// <summary>
        /// Decodes a stored hash into its component parts.
        /// </summary>
        /// <param name="storedHash">
        /// ASCII string in the format ".AAAA.BBBB.NNNN.XXXX.YYYY" 
        /// where . = delimiter character, AAAA = hash algorithm,
        /// BBBB = encoding format, NNNN = work factor, 
        /// XXXX = encoded password salt, YYYY = encoded password hash.
        /// </param>
        /// <returns>
        /// True if the stored hash is valid, or an exception. 
        /// explaining why the stored hash is invalid.
        /// </returns>
        public bool HashDecode(string storedHash)
        {
            return this.ValidateEncodedHash(storedHash);
        }

        private bool ValidateEncodedHash(string storedHash)
        {
            // Must not be empty.
            if (String.IsNullOrEmpty(storedHash))
            {
                throw new ArgumentNullException("storedHash", String.Format(CultureInfo.InvariantCulture, "Stored password hash cannot be null or empty"));
            }

            // Must have 5 sections: hash algorithm, encoding format, 
            // hash work factor, encoded password salt, encoded password hash.
            char delimiter = Convert.ToChar(storedHash.Substring(0, 1), CultureInfo.InvariantCulture);
            string[] storedSections = storedHash.Split(delimiter);
            if (storedSections.Length != CORRECT_NUMBER_OF_HASH_SECTIONS)
            {
                throw new ArgumentException("storedHash", String.Format(CultureInfo.InvariantCulture, "Stored password hash must have {0} sections", CORRECT_NUMBER_OF_HASH_SECTIONS));
            }

            // Validate and store each of the 5 relevant sections (section zero should be empty).
            this.HashMethod = ValidateHashAlgorithm(storedSections[SECTION_HASH_ALGORITHM]);
            this.HashStorageFormat = ValidateStorageFormat(storedSections[SECTION_STORAGE_FORMAT]);
            this.WorkFactor = ValidateWorkFactor(storedSections[SECTION_WORK_FACTOR]);
            this.PasswordSalt = this.ValidatePasswordSaltEncoded(storedSections[SECTION_ENCODED_SALT]);
            this.PasswordHash = this.ValidatePasswordHashEncoded(storedSections[SECTION_ENCODED_HASH]);
            return true;
        }

        private static HashAlgorithm ValidateHashAlgorithm(string hashAlgorithm)
        {
            if (String.IsNullOrEmpty(hashAlgorithm))
            {
                throw new ArgumentNullException("hashAlgorithm", String.Format(CultureInfo.InvariantCulture, "Hash algorithm cannot be null or empty"));
            }

            // Hash algorithm must be within storage enumeration range.
            if (hashAlgorithm == HashAlgorithm.Sha1_160.ToString())
            {
                return HashAlgorithm.Sha1_160;
            }
            else if (hashAlgorithm == HashAlgorithm.Sha2_256.ToString())
            {
                return HashAlgorithm.Sha2_256;
            }
            else if (hashAlgorithm == HashAlgorithm.Sha3_512.ToString())
            {
                return HashAlgorithm.Sha3_512;
            }
            else if (hashAlgorithm == HashAlgorithm.Bcrypt_192.ToString())
            {
                return HashAlgorithm.Bcrypt_192;
            }
            else if (hashAlgorithm == HashAlgorithm.Scrypt_512.ToString())
            {
                return HashAlgorithm.Scrypt_512;
            }
            else
            {
                throw new ArgumentOutOfRangeException("hashAlgorithm", String.Format(CultureInfo.InvariantCulture, "Hash algorithm isn't recognised!"));
            }
        }

        private static StorageFormat ValidateStorageFormat(string storageFormat)
        {
            if (String.IsNullOrEmpty(storageFormat))
            {
                throw new ArgumentNullException("storageFormat", String.Format(CultureInfo.InvariantCulture, "Storage format cannot be null or empty"));
            }

            // Storage format must be within storage enumeration range.
            if (storageFormat == StorageFormat.Hexadecimal.ToString())
            {
                return StorageFormat.Hexadecimal;
            }
            else if (storageFormat == StorageFormat.Base64.ToString())
            {
                return StorageFormat.Base64;
            }
            else
            {
                throw new ArgumentOutOfRangeException("storageFormat", String.Format(CultureInfo.InvariantCulture, "Storage format isn't recognised!"));
            }
        }

        private static byte[] ValidatePasswordSalt(byte[] passwordSalt)
        {
            if ((passwordSalt == null) || (passwordSalt.Length == 0))
            {
                throw new ArgumentNullException("passwordSalt", String.Format(CultureInfo.InvariantCulture, "Password salt must not be null or empty!"));
            }
            return passwordSalt;
        }

        private static byte[] ValidatePasswordHash(byte[] passwordHash)
        {
            if ((passwordHash == null) || (passwordHash.Length == 0))
            {
                throw new ArgumentNullException("passwordHash", String.Format(CultureInfo.InvariantCulture, "Password hash must not be null or empty!"));
            }
            return passwordHash;
        }

        private static Int32 ValidateWorkFactor(string workFactor)
        {
            if (String.IsNullOrEmpty(workFactor))
            {
                throw new ArgumentOutOfRangeException("workFactor", String.Format(CultureInfo.InvariantCulture, "Hash work factor must not be null or empty!"));
            }

            Int32 iterations = 0;
            if (!Int32.TryParse(workFactor, out iterations))
            {
                throw new ArgumentOutOfRangeException("workFactor", String.Format(CultureInfo.InvariantCulture, "Hash work factor must be numeric!"));
            }

            if (iterations < 1)
            {
                throw new ArgumentOutOfRangeException("workFactor", String.Format(CultureInfo.InvariantCulture, "Hash work factor must be positive!"));
            }
            return iterations;
        }

        private byte[] ValidatePasswordSaltEncoded(string passwordSaltEncoded)
        {
            if (String.IsNullOrEmpty(passwordSaltEncoded))
            {
                throw new ArgumentNullException("passwordSaltEncoded", String.Format(CultureInfo.InvariantCulture, "Encoded password salt must not be null or empty!"));
            }

            switch (this.HashStorageFormat)
            {
                // Hexadecimal.
                case StorageFormat.Hexadecimal:
                    return ConvertFromHexString(passwordSaltEncoded);
                // Base64.
                case StorageFormat.Base64:
                    return Convert.FromBase64String(passwordSaltEncoded);
                // Invalid storage format.
                default:
                    Debug.Assert(false, this.HashStorageFormat.ToString() + " is an invalid storage format!");
                    return null;
            }
        }

        private byte[] ValidatePasswordHashEncoded(string passwordHashEncoded)
        {
            if (String.IsNullOrEmpty(passwordHashEncoded))
            {
                throw new ArgumentNullException("passwordHashEncoded", String.Format(CultureInfo.InvariantCulture, "Encoded password hash must not be null or empty!"));
            }

            switch (this.HashStorageFormat)
            {
                // Hexadecimal.
                case StorageFormat.Hexadecimal:
                    return ConvertFromHexString(passwordHashEncoded);
                // Base64.
                case StorageFormat.Base64:
                    return Convert.FromBase64String(passwordHashEncoded);
                // Invalid storage format.
                default:
                    Debug.Assert(false, this.HashStorageFormat.ToString() + " is an invalid storage format!");
                    return null;
            }
        }

        private static byte[] ConvertFromHexString(string hexString)
        {
            int offset = hexString.StartsWith("0x", true, CultureInfo.CurrentCulture) ? 2 : 0;

            if ((hexString.Length % 2) != 0)
            {
                throw new ArgumentException("hexString", String.Format(CultureInfo.InvariantCulture, "Length of hex string must be even, not {0}", hexString.Length));
            }
            byte[] ret = new byte[(hexString.Length - offset) / 2];

            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = (byte)((ParseNybble(hexString[offset]) << 4) | ParseNybble(hexString[offset + 1]));
                offset += 2;
            }
            return ret;
        }

        private static int ParseNybble(char symbol)
        {
            if (symbol >= '0' && symbol <= '9')
            {
                return symbol - '0';
            }
            if (symbol >= 'A' && symbol <= 'F')
            {
                return symbol - 'A' + 10;
            }
            if (symbol >= 'a' && symbol <= 'f')
            {
                return symbol - 'a' + 10;
            }
            throw new ArgumentException(String.Format(CultureInfo.InvariantCulture, "Invalid hex character: " + symbol), "symbol");
        }

        /// <summary>
        /// Overrides the default ToString().
        /// </summary>
        /// <returns>
        /// The password as a Unicode (UTF-8) string. 
        /// </returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture, "HashStorage: " + this.StoredHash);
        }
    }
}