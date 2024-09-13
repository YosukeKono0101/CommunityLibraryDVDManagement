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
            // Retrieve all movies from the hash table.
            Movie[] movies = GetAllMovies();
            // Sort the movies based on borrow count using QuickSort.
            QuickSort(movies, 0, movies.Length - 1);
            // Determine the number of movies to display (up to 3).
            int count = Math.Min(3, movies.Length);
            Console.WriteLine("Top 3 Borrowed Movies:");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{movies[i].Title} - Borrowed {movies[i].BorrowCount} times");
            }
        }

        // Retrieves all movies from the hash table and returns them as an array.
        public Movie[] GetAllMovies()
        {
            Movie[] movies = new Movie[size];
            int index = 0;
            foreach (var bucket in buckets)
            {
                Node current = bucket;
                while (current != null)
                {
                    if (index < size)
                    {
                        // Collect movies from each linked list.
                        movies[index++] = current.Data;
                    }
                    current = current.Next;
                }
            }
            // Resize the array to the actual number of movies.
            Array.Resize(ref movies, index);
            return movies;
        }

        // Recursive QuickSort algorithm to sort movies by borrow count in descending order.
        private void QuickSort(Movie[] movies, int low, int high)
        {
            if (low < high)
            {
                // Partition the array and get the pivot index.
                int pi = Partition(movies, low, high);
                // Recursively sort the left subarray.
                QuickSort(movies, low, pi - 1);
                // Recursively sort the right subarray.
                QuickSort(movies, pi + 1, high);
            }
        }

        // Partition function used by QuickSort to rearrange the array.
        private int Partition(Movie[] movies, int low, int high)
        {
            // Choose the last element as pivot.
            Movie pivot = movies[high];
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                // If current movie has more borrows than pivot,
                if (movies[j].BorrowCount > pivot.BorrowCount)
                {
                    // increment index of smaller element.
                    i++;
                    // Swap the current element with the element at index i.
                    Swap(movies, i, j);
                }
            }
            // Swap the pivot element with the element at index i+1.
            Swap(movies, i + 1, high);
            return i + 1;
        }

        // Swaps two elements in the array of movies.
        private void Swap(Movie[] movies, int i, int j)
        {
            Movie temp = movies[i];
            movies[i] = movies[j];
            movies[j] = temp;
        }
    }
}
