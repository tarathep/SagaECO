namespace SagaDB
{
    /// <summary>
    /// Defines the <see cref="AccountDB" />.
    /// </summary>
    public interface AccountDB
    {
        /// <summary>
        /// The WriteUser.
        /// </summary>
        /// <param name="user">User that needs to be added.</param>
        void WriteUser(Account user);

        /// <summary>
        /// The GetUser.
        /// </summary>
        /// <param name="name">Name of the user.</param>
        /// <returns>The user with the given name.</returns>
        Account GetUser(string name);

        /// <summary>
        /// The CheckPassword.
        /// </summary>
        /// <param name="user">The user<see cref="string"/>.</param>
        /// <param name="password">The password<see cref="string"/>.</param>
        /// <param name="frontword">The frontword<see cref="uint"/>.</param>
        /// <param name="backword">The backword<see cref="uint"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        bool CheckPassword(string user, string password, uint frontword, uint backword);

        /// <summary>
        /// The GetAccountID.
        /// </summary>
        /// <param name="user">The user<see cref="string"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        int GetAccountID(string user);

        /// <summary>
        /// The Connect.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        bool Connect();

        /// <summary>
        /// The isConnected.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        bool isConnected();
    }
}
