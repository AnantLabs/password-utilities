// Copyright (c) 2013 Mark Pearce
// http://opensource.org/licenses/MIT

using System;
using System.Globalization;

namespace PasswordUtilities
{
	/// <summary>
	/// Policy used to control generation of a randomly-salted password hash.
	/// </summary>
	public sealed class HashPolicy
	{
		// We want a large number of hash iterations because it makes any 
		// incremental attack much slower. Speed is exactly what you don't 
		// want in a password hash function.
		// This also has the effect of stretching the password and thereby
		// increasing its entropy - 2^X iterations increases the password's 
		// entropy by X bits.
        // A good default is something that takes around 0.5-1.0 second 
        // to execute.
		private const int WORK_FACTOR_DEFAULT = 14;  // 2^14 hash iterations
        private const int WORK_FACTOR_MINIMUM = 1;
        private const int WORK_FACTOR_MAXIMUM = 24;

		// Other class constants.
		private const int SALT_MINIMUM_BYTES = 8;
		private const int SALT_DEFAULT_BYTES = 10;
        private const HashAlgorithm HASH_ALGORITHM_DEFAULT = HashAlgorithm.SHA1_160; 
        private const StorageFormat HASH_STORAGE_DEFAULT = StorageFormat.Hexadecimal;
        private const string COMMAS_AND_ZERO_DECIMAL_PLACES = "N0";

        private const int SHA1_NUMBER_OF_HASH_BYTES = 20;

		/// <summary>
		/// Use the default hash policy settings.
		/// </summary>
		public HashPolicy()
		{
            this.SetPolicy(HASH_ALGORITHM_DEFAULT, WORK_FACTOR_DEFAULT, HASH_STORAGE_DEFAULT, SALT_DEFAULT_BYTES);
		}

        /// <summary>
        /// Constructor for a non-default hash policy.
        /// </summary>
        /// <param name="hashAlgorithm">
        /// Required hash algorithm.
        /// </param>
        public HashPolicy(HashAlgorithm hashAlgorithm)
        {
            this.SetPolicy(hashAlgorithm, WORK_FACTOR_DEFAULT, HASH_STORAGE_DEFAULT, SALT_DEFAULT_BYTES);
        }

		/// <summary>
		/// Constructor for a non-default hash policy.
		/// </summary>
        /// <param name="hashAlgorithm">
        /// Required hash algorithm.
        /// </param>
        /// <param name="workFactor">
        /// Number of hash iterations expressed as 2^WorkFactor.
		/// </param>
        public HashPolicy(HashAlgorithm hashAlgorithm, int workFactor)
		{
            this.SetPolicy(hashAlgorithm, workFactor, HASH_STORAGE_DEFAULT, SALT_DEFAULT_BYTES);
		}

        /// <summary>
        /// Constructor for a non-default hash policy.
        /// </summary>
        /// <param name="hashAlgorithm">
        /// Required hash algorithm.
        /// </param>
        /// <param name="workFactor">
        /// Number of hash iterations expressed as 2^WorkFactor.
        /// </param>
        /// <param name="storageFormat">
        /// Storage encoding required.
        /// </param>
        public HashPolicy(HashAlgorithm hashAlgorithm, int workFactor, StorageFormat storageFormat)
        {
            this.SetPolicy(hashAlgorithm, workFactor, storageFormat, SALT_DEFAULT_BYTES);
        }

        /// <summary>
        /// Constructor for a non-default hash policy.
        /// </summary>
        /// <param name="hashAlgorithm">
        /// Required hash algorithm.
        /// </param>
        /// <param name="workFactor">
        /// Number of hash iterations expressed as 2^WorkFactor.
        /// </param>
        /// <param name="storageFormat">
        /// Storage encoding required.
        /// </param>
        /// <param name="numberOfSaltBytes">
        /// Number of salt bytes required.
        /// </param>
        public HashPolicy(HashAlgorithm hashAlgorithm, int workFactor, StorageFormat storageFormat, int numberOfSaltBytes)
        {
            this.SetPolicy(hashAlgorithm, workFactor, storageFormat, numberOfSaltBytes);
        }

		// Called from every constructor to setup and validate the hash policy.
        private void SetPolicy(HashAlgorithm hashAlgorithm, int workFactor, StorageFormat storageFormat, int numberOfSaltBytes)
		{
            this.HashMethod = hashAlgorithm;
            this.WorkFactor = workFactor;
            this.HashStorageFormat = storageFormat; 
			this.NumberOfSaltBytes = numberOfSaltBytes;

			this.ValidatePolicy();
		}

        /// <summary>
        /// Required hash algorithm.
        /// </summary>
        public HashAlgorithm HashMethod { get; set; }

		/// <summary>
		/// Number of hash iterations expressed as 2^WorkFactor.
		/// </summary>
        public Int32 WorkFactor { get; set; }

        /// <summary>
        /// Format in which to output encoded salt/hash for storage.
        /// </summary>
        public StorageFormat HashStorageFormat { get; set; }

        /// <summary>
        /// Required length of salt in bytes.
        /// </summary>
        public int NumberOfSaltBytes { get; set; }

		// Validates this policy.  
		private void ValidatePolicy()
		{
            // Hash algorithm must be within enumeration range.
            // Not using Enum.IsDefined for this validation check because 
            // that loads reflection code and some cold type metadata. 
            // See: http://blogs.msdn.com/b/brada/archive/2003/11/29/50903.aspx 
            switch (this.HashMethod)
            {
                case HashAlgorithm.SHA1_160:
                    break;
                case HashAlgorithm.SHA2_256:
                    break;
                case HashAlgorithm.SHA3_512:
                    throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture, "SHA3-512 not implemented yet"));
                case HashAlgorithm.BCRYPT_192:
                    throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture, "BCRYPT-192 not implemented yet"));
                case HashAlgorithm.Scrypt_512:
                    throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture, "SCRYPT-512 not implemented yet"));
                default:
                    throw new ArgumentOutOfRangeException(String.Format(CultureInfo.InvariantCulture, "Unknown hash algorithm"));
            }

            // 2^X iterations increases the password hash entropy by X bits.
			if (this.WorkFactor < WORK_FACTOR_MINIMUM)
			{
                throw new ArgumentOutOfRangeException("workFactor", String.Format(CultureInfo.InvariantCulture, "Work factor must be at least {0}, not {1}", WORK_FACTOR_MINIMUM, this.WorkFactor.ToString(COMMAS_AND_ZERO_DECIMAL_PLACES, CultureInfo.InvariantCulture)));
			}

            // 2^X iterations increases the password hash entropy by X bits.
            if (this.WorkFactor > WORK_FACTOR_MAXIMUM)
            {
                throw new ArgumentOutOfRangeException("workFactor", String.Format(CultureInfo.InvariantCulture, "Work factor must be less than {0}, not {1}", WORK_FACTOR_MAXIMUM, this.WorkFactor.ToString(COMMAS_AND_ZERO_DECIMAL_PLACES, CultureInfo.InvariantCulture)));
            }

			// Recommended salt length nowadays is at least 64 bits.
			if (this.NumberOfSaltBytes < SALT_MINIMUM_BYTES)
			{
                throw new ArgumentOutOfRangeException("numberOfSaltBytes", String.Format(CultureInfo.InvariantCulture, "Must have at least {0} salt bytes: not {1}", SALT_MINIMUM_BYTES, this.NumberOfSaltBytes.ToString(COMMAS_AND_ZERO_DECIMAL_PLACES, CultureInfo.InvariantCulture)));
			}

            // Storage format must be within enumeration range.
            // Not using Enum.IsDefined for this validation check because 
            // that loads reflection code and some cold type metadata. 
            // See: http://blogs.msdn.com/b/brada/archive/2003/11/29/50903.aspx 
            switch (this.HashStorageFormat)
            {
                case StorageFormat.Hexadecimal:
                    break;
                case StorageFormat.Base64:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(String.Format(CultureInfo.InvariantCulture, "Can only store hash/salt using base64 or hexadecimal"));
            }
        }

        /// <summary>
        /// Estimates the added entropy added to the password by this hash.
        /// </summary>
        /// <returns>
        /// The hash entropy in bits. 
        /// </returns>
        public double HashEntropy
        {
            get
            {
                return Math.Log(Math.Pow(2, this.WorkFactor), 2);
            }
        }

		/// <summary>
		/// Overrides the default ToString().
		/// </summary>
		/// <returns>
		/// The work factor and storage format specified by this policy. 
		/// </returns>
		public override string ToString()
		{
            return String.Format(CultureInfo.InvariantCulture, "HashPolicy: " + this.WorkFactor.ToString(COMMAS_AND_ZERO_DECIMAL_PLACES) + " work factor, " + this.HashStorageFormat.ToString() + " storage");
		}
	}
}