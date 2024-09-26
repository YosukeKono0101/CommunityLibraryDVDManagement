namespace CommunityLibraryDVDManagement
{
    // Represents a node in a linked list, storing movie data.
    public class Node
    {
        // Data part of the node containing movie information.
        public Movie Data { get; set; }
        // Reference to the next node in the linked list.
        public Node Next { get; set; }

        public Node(Movie data)
        {
            Data = data;
            Next = null;
        }
    }

    // Manages a collection of movies using a hash table implementation.
    public class MovieCollection
    {
        // Array of node references, each being a potential head of a linked list.
        private Node[] buckets;
        // Size of the hash table array.
        private int size;

        // Computes the hash index for a given key (movie title).
        public MovieCollection(int size = 1000)
        {
            this.size = size;
            buckets = new Node[size];
        }

        // Computes the hash index for a given key (movie title).
        private int GetHash(string key)
        {
            int hash = key.Length;
            foreach (char c in key)
            {
                // Compute hash by summing character values, influenced by their order.
                hash = (hash * 31 + c) % size;
            }
            return hash;
        }

        // Adds a new movie to the hash table.
        public void AddMovie(Movie movie)
        {
            if (movie == null)
                throw new ArgumentNullException(nameof(movie), "Movie cannot be null.");

            // Get the hash index for the movie.
            int index = GetHash(movie.Title);
            // Create a new node for the movie.
            Node newNode = new Node(movie);
            if (buckets[index] == null)
            {
                // Place the node directly if no collision.
                buckets[index] = newNode;
            }
            else
            {
                // Collision occurred, traverse to the end of the linked list.
                Node current = buckets[index];
                while (current.Next != null)
                {
                    current = current.Next;
                }
                // Append the new node at the end of the list.
                current.Next = newNode;
            }
        }

        // Finds a movie by its title.
        public Movie FindMovie(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Title cannot be null or empty.");

            // Calculate hash index for the title.
            int index = GetHash(title);
            // Start at the bucket corresponding to the hash index.
            Node current = buckets[index];
            while (current != null)
            {
                if (current.Data.Title == title)
                    return current.Data;
                // Move to next node in the list.
                current = current.Next;
            }
            // Return null if the movie is not found.
            return null;
        }

        // Removes a movie from the collection by its title.
        public bool RemoveMovie(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("Title cannot be null or empty.");

            // Calculate hash index.
            int index = GetHash(title);
            Node current = buckets[index];
            // To keep track of the previous node during traversal.
            Node previous = null;
            while (current != null)
            {
                if (current.Data.Title == title)
                {
                    if (previous == null)
                    {
                        // Remove the node by changing the head of the list.
                        buckets[index] = current.Next;
                    }
                    else
                    {
                        // Connect previous node to the next, bypassing the current node.
                        previous.Next = current.Next;
                    }
                    // Return true on successful removal.
                    return true;
                }
                previous = current;
                current = current.Next;
            }
            return false;
        }

        // Displays the top three most borrowed movies.
        public void DisplayTopThreeBorrowedMovies()
        {
            // Initialize variables to keep track of top three movies.
            Movie top1 = null;
            Movie top2 = null;
            Movie top3 = null;

            // Iterate through all movies to find the top three.
            foreach (var movie in GetAllMovies())
            {
                if (movie == null) continue;

                if (top1 == null || movie.BorrowCount > top1.BorrowCount)
                {
                    top3 = top2;
                    top2 = top1;
                    top1 = movie;
                }
                else if (top2 == null || movie.BorrowCount > top2.BorrowCount)
                {
                    top3 = top2;
                    top2 = movie;
                }
                else if (top3 == null || movie.BorrowCount > top3.BorrowCount)
                {
                    top3 = movie;
                }
            }

            Console.WriteLine("Top 3 Most Borrowed Movies:");
            if (top1 != null)
                Console.WriteLine($"{top1.Title} - Borrowed {top1.BorrowCount} times");
            if (top2 != null)
                Console.WriteLine($"{top2.Title} - Borrowed {top2.BorrowCount} times");
            if (top3 != null)
                Console.WriteLine($"{top3.Title} - Borrowed {top3.BorrowCount} times");
        }

        // Retrieves all movies from the hash table and returns them as an array.
        public Movie[] GetAllMovies()
        {
            // First Pass: Count the total number of movies 
            int totalMovies = 0;
            foreach (var bucket in buckets)
            {
                Node current = bucket;
                while (current != null)
                {
                    totalMovies++;
                    current = current.Next;
                }
            }

            // Allocate an array with the exact size needed
            Movie[] movies = new Movie[totalMovies];
            int index = 0;

            // Second Pass: Populate the array with movie data
            foreach (var bucket in buckets)
            {
                Node current = bucket;
                while (current != null)
                {
                    movies[index++] = current.Data;
                    current = current.Next;
                }
            }

            return movies;
        }
    }
}
