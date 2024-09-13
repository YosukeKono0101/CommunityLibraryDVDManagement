namespace CommunityLibraryDVDManagement
{
    public class Movie
    {
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public string Classification { get; private set; }
        public int Duration { get; private set; }
        public int AvailableCopies { get; private set; }
        public int BorrowCount { get; private set; }

        public Movie(string title, string genre, string classification, int duration, int availableCopies)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Movie title cannot be empty.");
            if (string.IsNullOrEmpty(genre))
                throw new ArgumentException("Genre cannot be empty.");
            if (classification != "G" && classification != "PG" && classification != "M15+" && classification != "MA15+")
                throw new ArgumentException("Invalid classification. Please enter one of the following: G, PG, M15+, MA15+");
            if (duration <= 0)
                throw new ArgumentException("Invalid duration. Please enter a valid positive number.");
            if (availableCopies <= 0)
                throw new ArgumentException("Invalid number of available copies. Please enter a valid non-negative number.");

            Title = title;
            Genre = genre;
            Classification = classification;
            Duration = duration;
            AvailableCopies = availableCopies;
            BorrowCount = 0;
        }

        public void AddCopies(int additionalCopies)
        {
            if (additionalCopies <= 0)
                throw new ArgumentException("Invalid input for copies available. Please enter a valid positive number.");
            AvailableCopies += additionalCopies;
        }

        public void RemoveCopies(int copiesToRemove)
        {
            if (copiesToRemove < 0)
                throw new ArgumentException("Invalid input. Please enter a valid positive number.");
            if (copiesToRemove > AvailableCopies)
                throw new ArgumentException("Cannot remove more copies than are available.");
            if (copiesToRemove == 0)
                throw new ArgumentException("No copies removed. Please enter a number greater than 0.");

            AvailableCopies -= copiesToRemove;
        }

        public void Borrow()
        {
            if (AvailableCopies > 0)
            {
                AvailableCopies--;
                BorrowCount++;
            }
            else
            {
                throw new InvalidOperationException("No copies available to borrow.");
            }
        }

        public void Return()
        {
            AvailableCopies++;
        }
    }
}
