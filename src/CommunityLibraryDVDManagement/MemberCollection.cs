namespace CommunityLibraryDVDManagement
{
    public class MemberCollection
    {
        private Member[] members;
        private int count;

        public MemberCollection(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be a positive integer.");
            members = new Member[capacity];
            count = 0;
        }

        // Adds a new member to the collection.
        public void AddMember(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member), "Member cannot be null.");

            // Throws an exception if the array is full, preventing any more members from being added.
            if (count >= members.Length)
                throw new InvalidOperationException("Member collection is full.");

            // Check for duplicates
            for (int i = 0; i < count; i++)
            {
                if (members[i].FirstName == member.FirstName && members[i].LastName == member.LastName)
                    throw new InvalidOperationException("Member already exists.");
            }

            // Adds the new member to the next available position and increments the count.
            members[count++] = member;
        }

        // Removes a member from the collection by their name.
        public bool RemoveMember(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentException("First name cannot be null or empty.");
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException("Last name cannot be null or empty.");

            for (int i = 0; i < count; i++)
            {
                // Identifies the member by first and last name.
                if (members[i].FirstName == firstName && members[i].LastName == lastName)
                {
                    // Check if the borrowed movies count is greater than 0
                    if (members[i].GetBorrowedMovies().Length > 0)
                        throw new InvalidOperationException("This member cannot be removed because they still have borrowed DVDs.");

                    // Removes the member by replacing them with the last member in the collection.
                    members[i] = members[count - 1];
                    members[count - 1] = null;
                    count--;
                    return true;
                }
            }
            return false;
        }

        // Finds a member by their first and last name.
        public Member FindMember(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));
            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));

            for (int i = 0; i < count; i++)
            {
                if (members[i].FirstName == firstName && members[i].LastName == lastName)
                    return members[i];
            }
            return null;
        }

        // Finds all members who have currently borrowed a specific movie.
        public Member[] FindMembersWithMovie(string movieTitle)
        {
            if (string.IsNullOrEmpty(movieTitle))
                throw new ArgumentException("Movie title cannot be null or empty.", nameof(movieTitle));

            Member[] membersWithMovie = new Member[count];
            int foundCount = 0;
            for (int i = 0; i < count; i++)
            {
                // Checks if the member has borrowed the specified movie.
                if (members[i] != null && Array.Exists(members[i].GetBorrowedMovies(), title => title == movieTitle))
                {
                    membersWithMovie[foundCount++] = members[i];
                }
            }
            Member[] result = new Member[foundCount];
            Array.Copy(membersWithMovie, result, foundCount);
            return result;
        }
    }
}
