using System;

namespace CommunityLibraryDVDManagement
{
  class Program
  {
    static MovieCollection movieCollection = new MovieCollection();
    static MemberCollection memberCollection = new MemberCollection();

    static void Main(string[] args)
    {
      while(true)
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
          default:
            Console.WriteLine("Invalid choice, please try again.");
            break;
        }
      }
    }

    static void StaffLogin()
    {
      Console.Write("Enter username: ");
      string username = Console.ReadLine()?.Trim();
      Console.Write("Enter password: ");
      string password = Console.ReadLine()?.Trim();

      if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
      {
        Console.WriteLine("Username or password cannot be empty.");
        return;
      }

      if(username == "staff" && password == "today123")
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
      while(true)
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
          default:
            Console.WriteLine("Invalid Choice, please try again.");
            break;
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

      if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(password))
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
      while(true)
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
            DisplayTopMovies();
            break;
          case 0:
            return;
          default:
            Console.WriteLine("Invalid choice, please try again."); 
            break;
        }
      }
    }

    // add movie
    static void AddMovie()
    {
      Console.Write("Enter movie title: ");
      string title = Console.ReadLine().Trim();
      if (string.IsNullOrEmpty(title))
      {
        Console.WriteLine("Movie title cannot be empty.");
        return;
      }

      Console.Write("Enter movie genre: ");
      string genre = Console.ReadLine().Trim();
      if (string.IsNullOrEmpty(genre))
      {
        Console.WriteLine("Genre cannot be empty.");
        return;
      }

      Console.Write("Enter movie classification (G, PG, M15+, MA15+): ");
      string classification = Console.ReadLine().Trim();
      if (string.IsNullOrEmpty(classification))
      {
        Console.WriteLine("Classification cannot be empty.");
        return;
      }

      Console.Write("Enter movie duration in minutes: ");
      if (!int.TryParse(Console.ReadLine(), out int duration) || duration <= 0)
      {
        Console.WriteLine("Invalid input for duration. Please enter a valid positve number.");
        return;
      }

      // check if the movie already exists
      var existingMovie = movieCollection.FindMovie(title);
      if (existingMovie != null)
      {
        // if the movie does exist, add the number of copies
        Console.Write("Enter additional number of copies: ");
        if (!int.TryParse(Console.ReadLine(), out int additionalCopies) || additionalCopies <= 0)
        {
          Console.WriteLine("Invalid input for copies available. Please enter a valid positive number.");
          return;
        }
        existingMovie.AvailableCopies += additionalCopies;
        Console.WriteLine($"Additional copies added. Total copies: {existingMovie.AvailableCopies}");
      }
      else
      {
        // if the movie does not exist, add the new movie
        Console.Write("Enter number of copies available: ");
        if (!int.TryParse(Console.ReadLine(), out int availableCopies) || availableCopies <= 0)
        {
          Console.WriteLine("Invalid input for copies available. Please enter a valid positive number.");
          return;
        }
        Movie newMovie = new Movie(title, genre, classification, duration, availableCopies);
        movieCollection.AddMovie(newMovie);
        Console.WriteLine("New movie added successfully.");
      }
    }

    // delete movie
    static void RemoveMovie()
    {
      Console.Write("Enter the title of the movie to remove copies from: ");
      string title = Console.ReadLine()?.Trim();
      if (string.IsNullOrEmpty(title))
      {
        Console.WriteLine("Movie title cannot be empty.");
        return;
      }

      Movie movie = movieCollection.FindMovie(title);
      if(movie == null)
      {
        Console.WriteLine("Movie not found.");
        return;
      }

      Console.Write($"Enter the number of copies to remove (currently {movie.AvailableCopies} available): ");
      if(!int.TryParse(Console.ReadLine(), out int copiesToRemove) || copiesToRemove < 0)
      {
        Console.WriteLine("Invalid input. Please enter a valid positive number.");
        return;
      }

      if(copiesToRemove > movie.AvailableCopies)
      {
        Console.WriteLine("Cannot remove more copies than are available.");
        return;
      }
      else if (copiesToRemove == 0)
      {
        Console.WriteLine("No copies removed. Please enter a number greater than 0.");
        return;
      }

      movie.AvailableCopies -= copiesToRemove;
      Console.WriteLine($"{copiesToRemove} copies removed. {movie.AvailableCopies} copies left.");

      if (movie.AvailableCopies == 0)
      {
        if(movieCollection.RemoveMovie(title))
        {
          Console.WriteLine("All copies removed. Movie has been deleted from the system.");
        }
        else
        {
          Console.WriteLine("Error removing the movie from the system.");
        }
      }
    }

    // register member
    static void RegisterMember()
    {
      Console.Write("Enter first name: ");
      string firstName = Console.ReadLine()?.Trim();
      Console.Write("Enter last name: ");
      string lastName = Console.ReadLine()?.Trim();

      if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
      {
        Console.WriteLine("First name or last name cannot be empty.");
        return;
      }
     
      // Check if member already exists
      if (memberCollection.FindMember(firstName, lastName) != null)
      {
        Console.WriteLine("This member is already registered.");
        return;
      }

      Console.Write("Enter contact number: ");
      string contactNumber = Console.ReadLine()?.Trim();
      Console.Write("Set a four-digit password for the member: ");
      string password = Console.ReadLine()?.Trim();

      if (password.Length != 4 || !int.TryParse(password, out _))
      {
        Console.WriteLine("Invalid password. Please enter a four-digit password.");
        return;
      }

      Member newMember = new Member(firstName, lastName, contactNumber, password);
      memberCollection.AddMember(newMember);
      Console.WriteLine("Member registered successfully.");
    }

    // remove member
    static void RemoveMember()
    {
      Console.Write("Enter first name of the member to remove: ");
      string firstName = Console.ReadLine()?.Trim();
      Console.Write("Enter last name of the member to remove: ");
      string lastName = Console.ReadLine()?.Trim();
      if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
      {
        Console.WriteLine("First name and last name cannot be empoty.");
        return;
      }

      Member member = memberCollection.FindMember(firstName, lastName);

      if (member == null)
      {
        Console.WriteLine("Member not found.");
        return;
      }

      // check if the member currently has any borrowed movies
      if (member.GetBorrowedMovies().Count > 0)
      {
        Console.WriteLine("This member cannot be removed because they still have borrowed DVDs.");
        Console.WriteLine("Please ensure all DVDs are returned before attempting to remove this member.");
        return;
      }
      else
      {
        // proceed to remove the member if no DVDs are borrowed
        if (memberCollection.RemoveMember(firstName, lastName))
        {
          Console.WriteLine("Member removed successfully.");
        }
        else
        {
          Console.WriteLine("An error occurred while trying to remove the member.");
        }
      }
    }

    // find member's contact phone num
    static void FindMemberPhoneNumber()
    {
      Console.Write("Enter the first name of the member: ");
      string firstName = Console.ReadLine()?.Trim();
      Console.Write("Enter the last name of the member: ");
      string lastName = Console.ReadLine()?.Trim();
      if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
      {
        Console.WriteLine("First name and last name cannot be empoty.");
        return;
      }

      Member member = memberCollection.FindMember(firstName, lastName);
      if (member != null)
      {
        Console.WriteLine($"The phone number of {firstName} {lastName} is {member.ContactNumber}. ");
      }
      else
      {
        Console.WriteLine("Member not found. Please check the spelling and try again.");
      }
    }

    // find members renting movie
    static void FindMembersRentingMovie()
    {
      Console.Write("Enter the movie title to find renting members: ");
      string movieTitle = Console.ReadLine()?.Trim();
      if (string.IsNullOrEmpty(movieTitle))
      {
        Console.WriteLine("Movie title cannot be empoty.");
        return;
      }

      var rentingMembers = memberCollection.FindMembersWithMovie(movieTitle);
      if (rentingMembers.Count > 0)
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

    // browse movies
    static void BrowseMovies()
    {
      var movies = movieCollection.GetAllMovies();
      if (movies.Count == 0)
      {
        Console.WriteLine("No movies available.");
        return;
      }

      // sort movie DVDs in dictonary order by movie title
      movies.Sort((x, y) => x.Title.CompareTo(y.Title));

      Console.WriteLine("Available Movies:");
      Console.WriteLine("--------------------------------------------------------------------------------");
      Console.WriteLine(String.Format("{0,-30} {1,-10} {2,-10} {3,10} {4,15}", "Title", "Genre", "Class", "Duration", "Copies Available"));
      Console.WriteLine("--------------------------------------------------------------------------------");
      foreach (var movie in movies)
      {
        Console.WriteLine(String.Format("{0,-30} {1,-10} {2,-10} {3,10} min {4,15}", movie.Title, movie.Genre, movie.Classification, movie.Duration, movie.AvailableCopies));
      }
      Console.WriteLine("--------------------------------------------------------------------------------");
    }

    // display info about a movie
    static void DisplayMovieInformation()
    {
      Console.Write("Enter the title of the movie you want information about: ");
      string title = Console.ReadLine()?.Trim();
      if (string.IsNullOrEmpty(title))
      {
        Console.WriteLine("Movie title cannot be empoty.");
        return;
      }
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

    // borrow movie
     static void BorrowMovie(Member member)
    {
      Console.Write("Enter the title of the movie you want to borrow: ");
      string title = Console.ReadLine()?.Trim();
      if (string.IsNullOrEmpty(title))
      {
        Console.WriteLine("Movie title cannot be empty.");
        return;
      }

      Movie movie = movieCollection.FindMovie(title);
      if (movie != null && movie.AvailableCopies > 0)
      {
        if (member.BorrowMovie(title))  // Checks if borrowing was successful
        {
          movie.AvailableCopies--;
          Console.WriteLine("Movie borrowed successfully.");
        }
      }
      else if (movie != null && movie.AvailableCopies == 0)
      {
        Console.WriteLine("No copies of the movie are currently available.");
      }
      else
      {
        Console.WriteLine("Movie not available.");
      }
    }

    // return movie
    static void ReturnMovie(Member member)
    {
      Console.Write("Enter the title of the movie you want to return: ");
      string title = Console.ReadLine()?.Trim();
      if (string.IsNullOrEmpty(title))
      {
        Console.WriteLine("Movie title cannot be empty.");
        return;
      }

      if (member.ReturnMovie(title))
      {
        Movie movie = movieCollection.FindMovie(title);
        if (movie != null)
        {
          movie.AvailableCopies++;
          Console.WriteLine("Movie returned successfully.");
        }
        else
        {
          Console.WriteLine("Error: The movie could not be found in the system.");
        }
      }
      else
      {
        Console.WriteLine("You did not borrow this movie.");
      }
    }

    // list borrowed movies
    static void ListBorrowedMovies(Member member)
    {
      var borrowedMovies = member.GetBorrowedMovies();
      if (borrowedMovies.Count > 0)
      {
        Console.WriteLine("Currently borrowed movies:");
        foreach (var movie in borrowedMovies)
        {
          Console.WriteLine(movie);
        }
      }
      else
      {
        Console.WriteLine("No movies currently borrowed.");
      }
    }

    // display top three movies 
    static void DisplayTopMovies()
    {
      var topMovies = movieCollection.GetTopBorrowedMovies(3);
      if(topMovies.Count == 0)
      {
        Console.WriteLine("No movies have been borrowed yet.");
        return;
      }

      Console.WriteLine("Top 3 Borrowed Movies:");
      foreach (var movie in topMovies)
      {
        Console.WriteLine($"{movie.Title} - Borrowed {movie.BorrowCount} times");
      }
    }

  }
}