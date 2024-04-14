using System;
namespace CommunityLibraryDVDManagement
{
  public class Movie
  {
      public string Title { get; set; }
      public string Genre { get; set; }
      public string Classification { get; set; }
      public int Duration { get; set; }
      public int AvailableCopies { get; set; }
      public int BorrowCount { get; set; }

    public Movie(string title, string genre, string classificaition, int duration, int availableCopies)
    {
      Title = title;
      Genre = genre;
      Classification = classificaition;
      Duration = duration;
      AvailableCopies = availableCopies;
      BorrowCount = 0; // initialize
    }
  }
}

