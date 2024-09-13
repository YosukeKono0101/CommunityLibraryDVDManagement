namespace CommunityLibraryDVDManagement
{
    public class Member
    {
        // Member properties
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string ContactNumber { get; private set; }
        public string Password { get; private set; }
        // Private fields to manage borrowed movies
        private string[] borrowedMovies;
        private int borrowedCount;

        // Constructor to initialize a new member with their details
        // Class Constructor Validation to ensure if an error slips through the initial validation in the program.cs
        public Member(string firstName, string lastName, string contactNumber, string password)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentException("First name cannot be empty.");
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException("Last name cannot be empty.");
            if (string.IsNullOrEmpty(contactNumber))
                throw new ArgumentException("Contact number cannot be empty.");
            if (password.Length != 4 || !int.TryParse(password, out _))
                throw new ArgumentException("Invalid password. Please enter a four-digit password.");

            FirstName = firstName;
            LastName = lastName;
            ContactNumber = contactNumber;
            Password = password;
            borrowedMovies = new string[5]; // Initializes the array with capacity for 5 movies
            borrowedCount = 0; // Initially, the member has no borrowed movies
        }

        // Method to borrow a movie
        public bool BorrowMovie(string movieTitle)
        {
            if (string.IsNullOrEmpty(movieTitle))
            {
                throw new ArgumentException("Movie title cannot be empty.");
            }

            // Check if the member has already borrowed 5 movies
            if (borrowedCount >= 5)
            {
                throw new InvalidOperationException("Cannot borrow more than 5 DVDs at a time.");
            }

            // Check if the member is trying to borrow a copy of a movie they already have
            for (int i = 0; i < borrowedCount; i++)
            {
                if (borrowedMovies[i] == movieTitle)
                {
                    throw new InvalidOperationException("Cannot borrow more than one copy of the same movie.");
                }
            }

            // Add the movie to the borrowed movies array and increase the count
            borrowedMovies[borrowedCount++] = movieTitle;
            Console.WriteLine($"'{movieTitle}' has been successfully borrowed.");
            return true;
        }

        // Method to return a borrowed movie
        public bool ReturnMovie(string movieTitle)
        {
            if (string.IsNullOrEmpty(movieTitle))
            {
                throw new ArgumentException("Movie title cannot be empty.");
            }

            // Find the movie in the borrowed list and remove it
            for (int i = 0; i < borrowedCount; i++)
            {
                if (borrowedMovies[i] == movieTitle)
                {
                    borrowedMovies[i] = borrowedMovies[--borrowedCount]; // Remove the movie by shifting the last movie in the list
                    Console.WriteLine($"'{movieTitle}' has been successfully returned.");
                    return true;
                }
            }

            // If the movie is not found in the borrowed list
            throw new InvalidOperationException("This movie was not borrowed by you.");
        }

        // Method to get a copy of the array of borrowed movies
        public string[] GetBorrowedMovies()
        {
            string[] currentBorrowed = new string[borrowedCount];
            Array.Copy(borrowedMovies, currentBorrowed, borrowedCount);
            return currentBorrowed;
        }
    }
}
