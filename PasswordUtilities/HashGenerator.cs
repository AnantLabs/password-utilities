// Copyright (c) 2013 Mark Pearce
// http://opensource.org/licenses/MIT

using System;
using System.Security.Cryptography;
using System.Globalization;
using System.Diagnostics;
using System.Text;

[assembly: CLSCompliant(true)]
namespace PasswordUtilities
{
	/// <summary>
	/// This class generates a randomly-salted hash for a specified password.
	/// This means you can store the salt and hash and never need to store the password.
	/// This class can also verify that a specified password and stored salt matches a stored hash. 
	/// </summary>
	/// <remarks>
	/// A random salt makes the use of a rainbow table cracker (such as Ophcrack) much harder. 
	/// A large number of hash iterations makes the use of an incremental cracker (such as LC5) much harder.
	/// But nothing will save you if the password is very weak, e.g. something like "passw0rd".
	/// http://chargen.matasano.com/chargen/2007/9/7/enough-with-the-rainbow-tables-what-you-need-to-know-about-s.html
	/// </remarks>
	public sealed class HashGenerator
	{
        private const Int32 SHA1_160_HASH_SIZE_IN_BYTES = 20;
        /// <summary>
		/// Use the default hash policy.
		/// </summary>
		/// <remarks>
		/// If you use the default password policy with the default hash policy,
		/// you will have a combined entropy of around 83 bits - greater than 
		/// NIST's recommendation of 80 bits for the most secure passwords.
		/// </remarks>
		public HashGenerator()
		{
			this.Policy = new HashPolicy();
		}

		/// <summary>
		/// Use the specified hash policy.
		/// </summary>
		public HashGenerator(HashPolicy hashPolicy)
		{
			this.Policy = hashPolicy;
		}

		/// <summary>
		/// Supplied password converted to a UTF-8 string.
		/// </summary>
        public string Password
        {
            get
            {
                if ((this.PasswordBytes == null) || (this.PasswordBytes.Length == 0))
                {
                    return String.Empty;
                }
                else
                {
                    return Encoding.UTF8.GetString(this.PasswordBytes);
                }
            }
        }

		/// <summary>
		/// Supplied password is always stored as a UTF-8 byte array.
		/// </summary>
        public byte[] PasswordBytes { get; private set; }

        /// <summary>
        /// Generated salt bytes converted to a UTF-8 Unicode string.
        /// </summary>
        public string Salt
        {
            get
            {
                if ( (this.SaltBytes == null) || (this.SaltBytes.Length == 0) )
                {
                    return String.Empty;
                }
                else
                {
                    return Encoding.UTF8.GetString(this.SaltBytes);
                }
            }
        }

        /// <summary>
        /// Generated salt is always stored as a byte array.
        /// </summary>
        public byte[] SaltBytes { get; private set; }

        /// <summary>
        /// Generated hash is always stored as a byte array.
        /// </summary>
        public byte[] HashBytes { get; private set; }

		/// <summary>
		/// Time taken to perform the password hashing.
		/// </summary>
        public TimeSpan GenerationTime { get; private set; }

		/// <summary>
		/// The policy used to control hash generation.
		/// </summary>
        public HashPolicy Policy { get; private set; }

        /// <summary>
        /// Password hash encoded using the specified storage format.
        /// </summary>
        public string HashEncoded
        {
            get
            {
                if (String.IsNullOrEmpty(this.Password))
                {
                    return String.Empty;
                }
                else
                {
                    HashStorage store = new HashStorage();
                    store.HashEncode(this.SaltBytes, this.HashBytes, this.Policy);
                    return store.PasswordHashEncoded;
                }
            }
        }

        /// <summary>
        /// Password salt encoded using the specified storage format.
        /// </summary>
        public string SaltEncoded
        {
            get
            {
                if (String.IsNullOrEmpty(this.Salt))
                {
                    return String.Empty;
                }
                else
                {
                    HashStorage store = new HashStorage();
                    store.HashEncode(this.SaltBytes, this.HashBytes, this.Policy);
                    return store.PasswordSaltEncoded;
                }
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
                return Math.Log(Math.Pow(2, this.Policy.WorkFactor), 2);
            }
        }

        /// <summary>
        /// Returns all the hash details in a format suitable 
        /// for storage and for password verification.
        /// </summary>
        /// <returns>
        /// ASCII string in the format ".AAAA.BBBB.NNNN.XXXX.YYYY" 
        /// where . = delimiter character, AAAA = hash algorithm,
        /// BBBB = encoding format, NNNN = work factor, 
        /// XXXX = encoded password salt, YYYY = encoded password hash.
        /// </returns>
        public string StoredHash
        {
            get
            {
                HashStorage store = new HashStorage();
                return store.HashEncode(this.SaltBytes, this.HashBytes, this.Policy);
            }
        }

        /// <summary>
        /// Creates a random password salt using the specified hash policy.
        /// </summary>
        /// <returns>
        /// The password salt as a Unicode (UTF-8) byte array.
        /// </returns>
        /// <remarks>
        /// As a side-effect this method 
        /// populates the password salt property. 
        /// </remarks>
        public byte[] CreatePasswordSalt()
        {
            this.SaltBytes = CreateRandomSalt(this.Policy.NumberOfSaltBytes);
            return this.SaltBytes;
        }

        /// <summary>
        /// Hashes the specified password and a random salt using a specified hash policy.
        /// </summary>
        /// <param name="password">
        /// The password that needs to be hashed in the form of a Unicode (UTF-8) string.
        /// </param>
        /// <returns>
        /// The password hash as a Unicode (UTF-8) byte array.
        /// </returns>
        /// <remarks>
        /// The password salt must already be available 
        /// via a previous call to CreatePasswordSalt().
        /// As a side-effect this method populates 
        /// the password and password hash properties. 
        /// </remarks>
        public byte[] CreatePasswordHash(string password)
        {
            return this.CreatePasswordHash(Encoding.UTF8.GetBytes(password), this.SaltBytes);
        }

        /// <summary>
        /// Hashes the specified password and a random salt using a specified hash policy.
        /// </summary>
        /// <param name="password">
        /// The password that needs to be hashed in the form of a Unicode (UTF-8) string.
        /// </param>
        /// <param name="salt">
        /// The password salt in the form of a Unicode (UTF-8) string.
        /// </param>
        /// <returns>
        /// The password hash as a Unicode (UTF-8) byte array.
        /// </returns>
        /// <remarks>
        /// As a side-effect this method populates the
        /// password, password salt, and password hash properties. 
        /// </remarks>
        public byte[] CreatePasswordHash(string password, string salt)
        {
            return this.CreatePasswordHash(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt));
        }

        /// <summary>
        /// Hashes the specified password and a random salt using a specified hash policy.
        /// </summary>
        /// <param name="password">
        /// The password that needs to be hashed as a Unicode (UTF-8) byte array.
        /// </param>
        /// <returns>
        /// The password hash as a Unicode (UTF-8) byte array.
        /// </returns>
        /// <remarks>
        /// As a side-effect this method populates 
        /// the password and password hash properties. 
        /// </remarks>
        public byte[] CreatePasswordHash(byte[] password)
        {
            return this.CreatePasswordHash(password, this.SaltBytes);
        }

        /// <summary>
        /// Hashes the specified password and a random salt using a specified hash policy.
        /// </summary>
        /// <param name="password">
        /// The password that needs to be hashed as a Unicode (UTF-8) byte array.
        /// </param>
        /// <returns>
        /// The password hash as a Unicode (UTF-8) byte array.
        /// </returns>
        /// <param name="salt">
        /// The password salt as a Unicode (UTF-8) byte array.
        /// </param>
        /// <remarks>
        /// The password salt must already be available 
        /// via a previous call to CreatePasswordSalt().
        /// As a side-effect this method populates the
        /// password, password salt, and password hash properties. 
        /// </remarks>
        public byte[] CreatePasswordHash(byte[] password, byte[] salt)
        {
            ValidatePasswordAndSalt(password, salt);
            this.PasswordBytes = password;
            this.SaltBytes = salt;

            Stopwatch timer = TimerStart();
            try
            {
                this.HashBytes = CreateSaltedHash(this.PasswordBytes, this.SaltBytes, this.Policy.WorkFactor, this.Policy.HashMethod);
            }
            finally
            {
                this.GenerationTime = TimerStop(timer);
            }
            return this.HashBytes;
        }

        /// <summary>
        /// Hashes the specified password and a random salt using a specified hash policy.
        /// </summary>
        /// <param name="password">
        /// The password that needs to be hashed as a Unicode (UTF-8) string.
        /// </param>
        /// <returns>
        /// The password hash as a Unicode (UTF-8) byte array.
        /// </returns>
        /// <remarks>
        /// As a side-effect this method populates the 
        /// password, password salt, and password hash properties. 
        /// </remarks>
        public byte[] CreatePasswordSaltAndHash(string password)
        {
            return this.CreatePasswordSaltAndHash(Encoding.UTF8.GetBytes(password));
        }

		/// <summary>
		/// Hashes the specified password and a random salt using a specified hash policy.
		/// </summary>
		/// <param name="password">
		/// The password that needs to be hashed as a Unicode (UTF-8) byte array.
		/// </param>
		/// <returns>
        /// The password hash as a Unicode (UTF-8) byte array.
		/// </returns>
		/// <remarks>
        /// As a side-effect this method populates the 
        /// password, password salt, and password hash properties. 
        /// </remarks>
		public byte[] CreatePasswordSaltAndHash(byte[] password)
		{
            this.CreatePasswordSalt();
            return this.CreatePasswordHash(password);
		}

        // Validate specified password and salt. 
		private static void ValidatePasswordAndSalt(byte[] password, byte[] salt)
		{
			// Password can't be null or empty.
			if ( (password == null) || (password.Length == 0) )
			{
				throw new ArgumentNullException("password", String.Format(CultureInfo.InvariantCulture, "Password cannot be null or empty"));
			}
            // Salt can't be null or empty.
            if ((salt == null) || (salt.Length == 0))
            {
                throw new ArgumentNullException("salt", "Salt cannot be null or empty");
            }
        }

		// Create and start the timer.
		private static Stopwatch TimerStart()
		{
			Stopwatch timer = new Stopwatch();
			timer.Start();
			return timer;
		}

		// Stop the timer and return result.
		private static TimeSpan TimerStop(Stopwatch timer)
		{
			timer.Stop();
			return timer.Elapsed;
		}

		// Creates the specified number of random bytes.
		private static byte[] CreateRandomSalt(Int32 numberOfBytes)
		{
			byte[] randomBytes = new byte[numberOfBytes];
            using (var saltBytes = new RNGCryptoServiceProvider())
            {
                saltBytes.GetBytes(randomBytes);
                return randomBytes;
            }
		}

		// Creates the hash using the specified password, salt, 
        // and number of hash iterations via PBKDF2.
		// See http://en.wikipedia.org/wiki/PBKDF2
		// And http://msdn.microsoft.com/en-us/library/system.security.cryptography.rfc2898derivebytes%28v=VS.100%29.aspx
		private static byte[] CreateSaltedHash(byte[] password, byte[] salt, Int32 workFactor, HashAlgorithm hashMethod)
		{
            switch (hashMethod)
            {
                case HashAlgorithm.Sha1_160:
                    return SHA1_160.CreateSaltedHash(password, salt, (Int32)Math.Pow(2, workFactor));
                case HashAlgorithm.Sha2_256:
                    return SHA2_256.CreateSaltedHash(password, salt, (Int32)Math.Pow(2, workFactor));
                case HashAlgorithm.Sha3_512:
                    throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture, "SHA3-512 not implemented yet"));
                case HashAlgorithm.Bcrypt_192:
                    throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture, "BCRYPT-192 not implemented yet"));
                case HashAlgorithm.Scrypt_512:
                    throw new NotImplementedException(String.Format(CultureInfo.InvariantCulture, "SCRYPT-512 not implemented yet"));
                default:
                    throw new ArgumentOutOfRangeException(String.Format(CultureInfo.InvariantCulture, "Unknown hash algorithm"));
            }
		}

		/// <summary>
		/// Overrides the default ToString().
		/// </summary>
		/// <returns>
		/// The password hash. 
		/// </returns>
		public override string ToString()
		{
            return String.Format(CultureInfo.InvariantCulture, "HashGenerator: " + this.HashEncoded); 
		}
	}
}