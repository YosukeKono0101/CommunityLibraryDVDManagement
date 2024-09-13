namespace CommunityLibraryDVDManagement
{
    class Program
    {
        static MovieCollection movieCollection = new MovieCollection();
        static MemberCollection memberCollection = new MemberCollection(100);

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("====================================================");
                Console.WriteLine("Welcome to the Community Library Movie DVD Management System");
                Console.WriteLine("====================================================");
                Console.WriteLine("");
                Console.WriteLine("Main Menu");
                Console.WriteLine("-------------------------------------------------------------------------------------------");
                Console.WriteLine("1. Staff ");
                Console.WriteLine("2. Member");
                Console.WriteLine("0. End the program");
                Console.Write("Please select an option: ");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > 2)
                {
                    Console.WriteLine("Invalid choice, please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        StaffLogin();
                        break;
                    case 2:
                        MemberLogin();
                        break;
                    case 0:
                        return;
                }
            }
        }

        static void StaffLogin()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine()?.Trim();
            Console.Write("Enter password: ");
            string password = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Username or password cannot be empty.");
                return;
            }

            if (username == "staff" && password == "today123")
            {
                StaffMenu();
            }
            else
            {
                Console.WriteLine("Incorrect username or password.");
            }
        }

        static void StaffMenu()
        {
            while (true)
            {
                Console.WriteLine("\nStaff Menu");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("1. Add DVDs to system");
                Console.WriteLine("2. Remove DVDs from system");
                Console.WriteLine("3. Register a new member to system");
                Console.WriteLine("4. Remove a registered member from system");
                Console.WriteLine("5. Find a member contact phone number, given the member's name");
                Console.WriteLine("6. Find members who are currently renting a particular movie");
                Console.WriteLine("0. Return to main menu");
                Console.Write("Select an option: ");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > 6)
                {
                    Console.WriteLine("Invalid choice, please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddMovie();
                        break;
                    case 2:
                        RemoveMovie();
                        break;
                    case 3:
                        RegisterMember();
                        break;
                    case 4:
                        RemoveMember();
                        break;
                    case 5:
                        FindMemberPhoneNumber();
                        break;
                    case 6:
                        FindMembersRentingMovie();
                        break;
                    case 0:
                        return;
                }
            }
        }

        static void MemberLogin()
        {
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine()?.Trim();
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine()?.Trim();
            Console.Write("Enter password: ");
            string password = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("First name, last name, and password fields cannot be empty.");
                return;
            }

            Member member = memberCollection.FindMember(firstName, lastName);
            if (member != null && member.Password == password)
            {
                MemberMenu(member);
            }
            else
            {
                Console.WriteLine("Incorrect login details. Please check your username and password.");
            }
        }

        static void MemberMenu(Member member)
        {
            while (true)
            {
                Console.WriteLine("\nMember Menu");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("1. Browse all the movies");
                Console.WriteLine("2. Display all the information about a movie, given the title of the movie");
                Console.WriteLine("3. Borrow a movie DVD");
                Console.WriteLine("4. Return a movie DVD");
                Console.WriteLine("5. List current borrowing movies");
                Console.WriteLine("6. Display the top 3 movies rented by the members");
                Console.WriteLine("0. Return to main menu");
                Console.Write("Select an option: ");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > 6)
                {
                    Console.WriteLine("Invalid choice, please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        BrowseMovies();
                        break;
                    case 2:
                        DisplayMovieInformation();
                        break;
                    case 3:
                        BorrowMovie(member);
                        break;
                    case 4:
                        ReturnMovie(member);
                        break;
                    case 5:
                        ListBorrowedMovies(member);
                        break;
                    case 6:
                        DisplayTopBorrowedMovies();
                        break;
                    case 0:
                        return;
                }
            }
        }

        static void AddMovie()
        {
            string title, genre, classification;
            int duration, availableCopies, additionalCopies;
            string[] validClassifications = { "G", "PG", "M15+", "MA15+" };

            do
            {
                Console.Write("Enter movie title: ");
                title = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("Movie title cannot be empty.");
                }
            } while (string.IsNullOrEmpty(title));

            do
            {
                Console.Write("Enter movie genre: ");
                genre = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(genre))
                {
                    Console.WriteLine("Movie genre cannot be empty.");
                }
            } while (string.IsNullOrEmpty(genre));

            do
            {
                Console.Write("Enter movie classification (G, PG, M15+, MA15+): ");
                classification = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(classification) || !Array.Exists(validClassifications, c => c.Equals(classification, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Invalid classification. Please enter one of the following: G, PG, M15+, MA15+");
                    classification = null;
                }
            } while (string.IsNullOrEmpty(classification));

            do
            {
                Console.Write("Enter movie duration in minutes: ");
            } while (!int.TryParse(Console.ReadLine(), out duration) || duration <= 0);

            var existingMovie = movieCollection.FindMovie(title);
            if (existingMovie != null)
            {
                do
                {
                    Console.Write("Enter additional number of copies: ");
                } while (!int.TryParse(Console.ReadLine(), out additionalCopies) || additionalCopies <= 0);

                existingMovie.AddCopies(additionalCopies);
                Console.WriteLine($"Additional copies added. Total copies: {existingMovie.AvailableCopies}");
            }
            else
            {
                do
                {
                    Console.Write("Enter number of copies available: ");
                } while (!int.TryParse(Console.ReadLine(), out availableCopies) || availableCopies <= 0);

                Movie newMovie = new Movie(title, genre, classification, duration, availableCopies);
                movieCollection.AddMovie(newMovie);
                Console.WriteLine("New movie added successfully.");
            }
        }

        static void RemoveMovie()
        {
            string title;
            int copiesToRemove;

            do
            {
                Console.Write("Enter the title of the movie to remove copies from: ");
                title = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("Movie title cannot be empty.");
                }
            } while (string.IsNullOrEmpty(title));

            Movie movie = movieCollection.FindMovie(title);
            if (movie == null)
            {
                Console.WriteLine("Movie not found.");
                return;
            }

            do
            {
                Console.Write($"Enter the number of copies to remove (currently {movie.AvailableCopies} available): ");
            } while (!int.TryParse(Console.ReadLine(), out copiesToRemove) || copiesToRemove <= 0);

            try
            {
                movie.RemoveCopies(copiesToRemove);
                Console.WriteLine($"{copiesToRemove} copies removed. {movie.AvailableCopies} copies left.");

                if (movie.AvailableCopies == 0)
                {
                    if (movieCollection.RemoveMovie(title))
                    {
                        Console.WriteLine("All copies removed. Movie has been deleted from the system.");
                    }
                    else
                    {
                        Console.WriteLine("Error removing the movie from the system.");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void RegisterMember()
        {
            string firstName, lastName, contactNumber, password;

            do
            {
                Console.Write("Enter first name: ");
                firstName = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(firstName))
                {
                    Console.WriteLine("First name cannot be empty.");
                }
            } while (string.IsNullOrEmpty(firstName));

            do
            {
                Console.Write("Enter last name: ");
                lastName = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(lastName))
                {
                    Console.WriteLine("Last name cannot be empty.");
                }
            } while (string.IsNullOrEmpty(lastName));

            do
            {
                Console.Write("Enter contact number: ");
                contactNumber = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(contactNumber))
                {
                    Console.WriteLine("Contact number cannot be empty.");
                }
            } while (string.IsNullOrEmpty(contactNumber));

            do
            {
                Console.Write("Set a four-digit password for the member: ");
                password = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(password) || password.Length != 4 || !int.TryParse(password, out _))
                {
                    Console.WriteLine("Password must be a four-digit number.");
                    password = null;
                }
            } while (string.IsNullOrEmpty(password));

            try
            {
                Member newMember = new Member(firstName, lastName, contactNumber, password);
                memberCollection.AddMember(newMember);
                Console.WriteLine("Member registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to register member: " + ex.Message);
            }
        }

        static void RemoveMember()
        {
            string firstName, lastName;

            do
            {
                Console.Write("Enter first name of the member to remove: ");
                firstName = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(firstName))
                {
                    Console.WriteLine("First name cannot be empty.");
                }
            } while (string.IsNullOrEmpty(firstName));

            do
            {
                Console.Write("Enter last name of the member to remove: ");
                lastName = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(lastName))
                {
                    Console.WriteLine("Last name cannot be empty.");
                }
            } while (string.IsNullOrEmpty(lastName));

            try
            {
                bool removed = memberCollection.RemoveMember(firstName, lastName);
                if (removed)
                {
                    Console.WriteLine("Member removed successfully.");
                }
                else
                {
                    Console.WriteLine("Member not found or could not be removed.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void FindMemberPhoneNumber()
        {
            string firstName, lastName;

            do
            {
                Console.Write("Enter the first name of the member: ");
                firstName = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(firstName))
                {
                    Console.WriteLine("First name cannot be empty.");
                }
            } while (string.IsNullOrEmpty(firstName));

            do
            {
                Console.Write("Enter the last name of the member: ");
                lastName = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(lastName))
                {
                    Console.WriteLine("Last name cannot be empty.");
                }
            } while (string.IsNullOrEmpty(lastName));

            try
            {
                Member member = memberCollection.FindMember(firstName, lastName);
                if (member != null)
                {
                    Console.WriteLine($"The phone number of {firstName} {lastName} is {member.ContactNumber}.");
                }
                else
                {
                    Console.WriteLine("Member not found. Please check the spelling and try again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void FindMembersRentingMovie()
        {
            string movieTitle;

            do
            {
                Console.Write("Enter the movie title to find renting members: ");
                movieTitle = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(movieTitle))
                {
                    Console.WriteLine("Movie title cannot be empty.");
                }
            } while (string.IsNullOrEmpty(movieTitle));

            try
            {
                var rentingMembers = memberCollection.FindMembersWithMovie(movieTitle);
                if (rentingMembers.Length > 0)
                {
                    Console.WriteLine($"Members currently renting {movieTitle}:");
                    foreach (var member in rentingMembers)
                    {
                        Console.WriteLine($"{member.FirstName} {member.LastName}");
                    }
                }
                else
                {
                    Console.WriteLine("No members are currently renting this movie.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void BrowseMovies()
        {
            try
            {
                var movies = movieCollection.GetAllMovies();
                if (movies.Length == 0)
                {
                    Console.WriteLine("No movies available.");
                    return;
                }

                Array.Sort(movies, (x, y) => x.Title.CompareTo(y.Title));

                Console.WriteLine("Available Movies:");
                Console.WriteLine("-------------------------------------------------------------------------------------");
                Console.WriteLine(String.Format("{0,-30} {1,-10} {2,-10} {3,10} {4,15}", "Title", "Genre", "Class", "Duration", "Copies Available"));
                Console.WriteLine("-------------------------------------------------------------------------------------");
                foreach (var movie in movies)
                {
                    Console.WriteLine(String.Format("{0,-30} {1,-10} {2,-10} {3,10} min {4,15}", movie.Title, movie.Genre, movie.Classification, movie.Duration, movie.AvailableCopies));
                }
                Console.WriteLine("-------------------------------------------------------------------------------------");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void DisplayMovieInformation()
        {
            string title;

            do
            {
                Console.Write("Enter the title of the movie you want information about: ");
                title = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("Movie title cannot be empty.");
                }
            } while (string.IsNullOrEmpty(title));

            try
            {
                Movie movie = movieCollection.FindMovie(title);
                if (movie != null)
                {
                    Console.WriteLine("Movie Details:");
                    Console.WriteLine($"Title: {movie.Title}");
                    Console.WriteLine($"Genre: {movie.Genre}");
                    Console.WriteLine($"Classification: {movie.Classification}");
                    Console.WriteLine($"Duration: {movie.Duration} minutes");
                    Console.WriteLine($"Available Copies: {movie.AvailableCopies}");
                    Console.WriteLine($"Total Times Borrowed: {movie.BorrowCount}");
                }
                else
                {
                    Console.WriteLine("Movie not found. Please check the title and try again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void BorrowMovie(Member member)
        {
            try
            {
                string title;
                do
                {
                    Console.Write("Enter the title of the movie you want to borrow: ");
                    title = Console.ReadLine()?.Trim();
                    if (string.IsNullOrEmpty(title))
                    {
                        Console.WriteLine("Movie title cannot be empty.");
                    }
                } while (string.IsNullOrEmpty(title));

                Movie movie = movieCollection.FindMovie(title);
                if (movie != null)
                {
                    if (movie.AvailableCopies > 0)
                    {
                        member.BorrowMovie(title);
                        movie.Borrow();
                        Console.WriteLine("Movie borrowed successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No copies of the movie are currently available.");
                    }
                }
                else
                {
                    Console.WriteLine("Movie not available.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while borrowing the movie: {ex.Message}");
            }
        }

        static void ReturnMovie(Member member)
        {
            try
            {
                string title;
                do
                {
                    Console.Write("Enter the title of the movie you want to return: ");
                    title = Console.ReadLine()?.Trim();
                    if (string.IsNullOrEmpty(title))
                    {
                        Console.WriteLine("Movie title cannot be empty.");
                    }
                } while (string.IsNullOrEmpty(title));

                if (member.ReturnMovie(title))
                {
                    Movie movie = movieCollection.FindMovie(title);
                    if (movie != null)
                    {
                        movie.Return();
                        Console.WriteLine("Movie returned successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Error: The movie could not be found in the system.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: The movie is not currently borrowed by the member.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while returning the movie: {ex.Message}");
            }
        }

        static void ListBorrowedMovies(Member member)
        {
            try
            {
                var borrowedMovies = member.GetBorrowedMovies();

                if (borrowedMovies == null || borrowedMovies.Length == 0)
                {
                    Console.WriteLine("No movies currently borrowed.");
                    return;
                }

                Console.WriteLine("Currently borrowed movies:");
                foreach (var movie in borrowedMovies)
                {
                    Console.WriteLine(movie);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while listing borrowed movies: {ex.Message}");
            }
        }

        static void DisplayTopBorrowedMovies()
        {
            try
            {
                movieCollection.DisplayTopThreeBorrowedMovies();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
