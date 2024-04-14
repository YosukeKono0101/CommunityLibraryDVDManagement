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

    public void BorrowMovie(string movieTitle)
    {
      if(borrowedMovies.Count < 5 && !borrowedMovies.Contains(movieTitle))
      {
        borrowedMovies.Add(movieTitle);
      }
    }

    public bool ReturnMovie(string movieTitle)
    {
      if (borrowedMovies.Contains(movieTitle))
      {
        borrowedMovies.Remove(movieTitle);
        return true;
      }
      return false;
    }

    public List<string> GetBorrowedMovies()
    {
      return borrowedMovies;
    }
  }
}

