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
        Console.WriteLine("Welcome to the Community Library Movie DVD Management System");
        Console.WriteLine("1. Staff  Login");
        Console.WriteLine("2. Member Login");
        Console.WriteLine("3. Exit");
        Console.Write("Please select an option: ");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
          case 1:
            StaffLogin();
            break;
          case 2:
            MemberLogin();
            break;
          case 3:
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
      string username = Console.ReadLine();
      Console.Write("Enter password: ");
      string password = Console.ReadLine();

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
        Console.WriteLine("1. Add a new movie DVD ");
        Console.WriteLine("2. Remove a movie DVD");
        Console.WriteLine("3. Register a new member");
        Console.WriteLine("4. Remove a member");
        Console.WriteLine("5. Find a member's phone number");
        Console.WriteLine("6. Find members renting a specific movie");
        Console.WriteLine("7. Exit");
        Console.Write("Select an option: ");
        int choice = Convert.ToInt32(Console.ReadLine());

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
          case 7:
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
      string firstName = Console.ReadLine();
      Console.Write("Enter last name: ");
      string lastName = Console.ReadLine();
      Console.Write("Enter password: ");
      string password = Console.ReadLine();

      Member member = memberCollection.FindMember(firstName, lastName);
      if (member != null && member.Password == password)
      {
        MemberMenu(member);
      }
      else
      {
        Console.WriteLine("Incorrect login details.");
      }
    }

    static void MemberMenu(Member member)
    {
      while(true)
      {
        Console.WriteLine("\nMember Menu");
        Console.WriteLine("1. Browse all movies");
        Console.WriteLine("2. Borrow a movie DVD");
        Console.WriteLine("3. Return a movie DVD");
        Console.WriteLine("4. List current borrowed movie DVDs");
        Console.WriteLine("5. Display the top three movies");
        Console.WriteLine("6. Exit");
        Console.Write("Select an option: ");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
          case 1:
            BrowseMovies();
            break;
          case 2:
            BorrowMovie(member);
            break;
          case 3:
            ReturnMovie(member);
            break;
          case 4:
            ListBorrowedMovies(member);
            break;
          case 5:
            DisplayTopMovies();
            break;
          case 6:
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
      string title = Console.ReadLine();
      Console.Write("Enter movie genre: ");
      string genre = Console.ReadLine();
      Console.Write("Enter movie classification (G, PG, M15+, MA15+): ");
      string classification = Console.ReadLine();
      Console.Write("Enter movie duration in minutes: ");
      if (!int.TryParse(Console.ReadLine(), out int duration))
      {
        Console.WriteLine("Invalid input for duration. Please enter a valid number.");
        return;
      }

      // check if the movie already exists
      var existingMovie = movieCollection.FindMovie(title);
      if (existingMovie != null)
      {
        // if the movie does exist, add the number of copies
        Console.Write("Enter additional number of copies: ");
        if (!int.TryParse(Console.ReadLine(), out int additionalCopies))
        {
          Console.WriteLine("Invalid input for copies available. Please enter a valid number.");
          return;
        }
        existingMovie.AvailableCopies += additionalCopies;
        Console.WriteLine($"Additional copies added. Total copies: {existingMovie.AvailableCopies}");
      }
      else
      {
        // if the movie does not exist, add the new movie
        Console.Write("Enter number of copies available: ");
        if (!int.TryParse(Console.ReadLine(), out int availableCopies))
        {
          Console.WriteLine("Invalid input for copies available. Please enter a valid number.");
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
      string title = Console.ReadLine();
      Movie movie = movieCollection.FindMovie(title);

      if(movie == null)
      {
        Console.WriteLine("Movie not found.");
        return;
      }

      Console.Write($"Enter the number of copies to remove (currently {movie.AvailableCopies} available): ");
      if(!int.TryParse(Console.ReadLine(), out int copiesToRemove))
      {
        Console.WriteLine("Invalid input. Please enter a valid number.");
        return;
      }

      if(copiesToRemove > movie.AvailableCopies)
      {
        Console.WriteLine("Cannot remove more copies than are available.");
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
      string firstName = Console.ReadLine();
      Console.Write("Enter last name: ");
      string lastName = Console.ReadLine();
     
      // Check if member already exists
      if (memberCollection.FindMember(firstName, lastName) != null)
      {
        Console.WriteLine("This member is already registered.");
        return;
      }

      Console.Write("Enter contact number: ");
      string contactNumber = Console.ReadLine();
      Console.Write("Set a four-digit password for the member: ");
      string password = Console.ReadLine();
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
      string firstName = Console.ReadLine();
      Console.Write("Enter last name of the member to remove: ");
      string lastName = Console.ReadLine();
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
      string firstName = Console.ReadLine();
      Console.Write("Enter the last name of the member: ");
      string lastName = Console.ReadLine();
      Member member = memberCollection.FindMember(firstName, lastName);
      if (member != null)
      {
        Console.WriteLine($"The phone number of {firstName} {lastName} is {member.ContactNumber}. ");
      }
      else
      {
        Console.WriteLine("Member not found.");
      }
    }

    // find members renting movie
    static void FindMembersRentingMovie()
    {
      Console.Write("Enter the movie title to find renting members: ");
      string movieTitle = Console.ReadLine();
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


    // borrow movie
    static void BorrowMovie(Member member)
    {
      Console.Write("Enter the title of the movie you want to borrow: ");
      string title = Console.ReadLine();
      Movie movie = movieCollection.FindMovie(title);
      if (movie != null && movie.AvailableCopies > 0)
      {
        member.BorrowMovie(title);
        movie.BorrowCount++;
        movie.AvailableCopies--;
        Console.WriteLine("Movie borrowed successfully.");
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
      string title = Console.ReadLine();
      if (member.ReturnMovie(title))
      {
        Movie movie = movieCollection.FindMovie(title);
        if (movie != null)
        {
          movie.AvailableCopies++;
          Console.WriteLine("Movie returned successfully.");
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