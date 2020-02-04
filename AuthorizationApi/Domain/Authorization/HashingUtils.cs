using System;
using System.Security.Cryptography;

namespace AuthorizationApi
{
    public static class HashingUtils
    {
        private const int NUMBER_OF_ITERATIONS = 1000;
        private const byte SALT_SIZE = 16;
        private const byte HASH_SIZE = 20;

        /// <summary>
        /// This is a method to compute a hash from a string.
        /// </summary>
        /// <param name="password">Enter a string to be hashed</param>
        /// <returns>A password hash</returns>
        public static string ComputeHash(string password)
        {
            byte[] salt = new byte[SALT_SIZE];
            new RNGCryptoServiceProvider().GetBytes(salt);
            byte[] hash = new Rfc2898DeriveBytes(password, salt, NUMBER_OF_ITERATIONS).GetBytes(HASH_SIZE);

            byte[] hashBytes = new byte[SALT_SIZE + HASH_SIZE];
            Array.Copy(salt, 0, hashBytes, 0, SALT_SIZE);
            Array.Copy(hash, 0, hashBytes, SALT_SIZE, HASH_SIZE);

            string passwordHash = Convert.ToBase64String(hashBytes);

            return passwordHash;
        }

        /// <summary>
        /// This is method to check if a given hash is identical with given string
        /// </summary>
        /// <param name="storedPasswordHash">A password hash</param>
        /// <param name="enteredPassword">A password string entered by an user</param>
        /// <returns>True if the entered password is identical with a stored hash</returns>
        public static bool IsGivenHashIdenticalWithGivenString(string storedPasswordHash, string enteredPassword)
        {
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(storedPasswordHash);

            if (storedPasswordHash.Equals(enteredPassword)) 
            {
                return true;
            }

            return false;
            /* Get the salt */
            //byte[] salt = new byte[SALT_SIZE];
            //Array.Copy(hashBytes, 0, salt, 0, SALT_SIZE);

            ///* Compute the hash on the password the user entered */
            //Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, NUMBER_OF_ITERATIONS);
            //byte[] hash = pbkdf2.GetBytes(HASH_SIZE);

            ///* Compare the results */
            //for (int i = 0; i < HASH_SIZE; i++)
            //{
            //    if (hashBytes[i + SALT_SIZE] != hash[i])
            //    {
            //        /* User entered a password that does not correspond to the password saved */
            //        return false;
            //    }
            //}
            ///* The password that user enetered is identical with the saved password */
            //return true;
        }
    }
}
