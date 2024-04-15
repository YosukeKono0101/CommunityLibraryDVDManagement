using System;
namespace CommunityLibraryDVDManagement
{
  public class Member
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactNumber { get; set; }
    public string Password { get; set; }
    private List<string> borrowedMovies;

    public Member(string firstName, string lastName, string contactNumber, string password)
    {
      FirstName = firstName;
      LastName = lastName;
      ContactNumber = contactNumber;
      Password = password;
      borrowedMovies = new List<string>();
    }

    public bool BorrowMovie(string movieTitle)
    {
      if (borrowedMovies.Count >= 5)
      {
        Console.WriteLine("Cannot borrow more than 5 DVDs at a time.");
        return false;
      }

      if (borrowedMovies.Contains(movieTitle))
      {
        Console.WriteLine("Cannot borrow more than one copy of the same movie.");
        return false;
      }

      borrowedMovies.Add(movieTitle);
      Console.WriteLine($"'{movieTitle}' has been successfully borrowed.");
      return true;
    }

    public bool ReturnMovie(string movieTitle)
    {
      if (borrowedMovies.Contains(movieTitle))
      {
        borrowedMovies.Remove(movieTitle);
        Console.WriteLine($"'{movieTitle}' has been successfully returned.");
        return true;
      }
      Console.WriteLine("This movie was not borrowed by you.");
      return false;
    }

    public List<string> GetBorrowedMovies()
    {
      return borrowedMovies;
    }
  }
}

