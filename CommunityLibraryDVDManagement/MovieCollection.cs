using System;
using System.Collections.Generic;

namespace CommunityLibraryDVDManagement
{
  public class MovieCollection
  {
    private const int Size = 1000;
    private List<Movie>[] table;

    public MovieCollection()
    {
      table = new List<Movie>[Size];
      for(int i = 0; i < Size; i++)
      {
        table[i] = new List<Movie>();
      }
    }

    private int GetHash(string key)
    {
      int hash = 0;
      foreach(char c in key)
      {
        hash = (hash * 31 + c) % Size;
      }
      return hash;
    }

    public void AddMovie(Movie movie)
    {
      int index = GetHash(movie.Title);
      table[index].Add(movie);
    }

    public bool RemoveMovie(string title)
    {
      int index = GetHash(title);
      var bucket = table[index];
      var movieToRemove = bucket.Find(m => m.Title == title && m.AvailableCopies == 0);
      if(movieToRemove != null)
      {
        bucket.Remove(movieToRemove);
        return true;
      }
      return false;
    }
    public Movie FindMovie(string title)
    {
      int index = GetHash(title);
      var bucket = table[index];
      return bucket.Find(m => m.Title == title);
    }

    public List<Movie> GetAllMovies()
    {
      List<Movie> allMovies = new List<Movie>();
      foreach (var bucket in table)
      {
        allMovies.AddRange(bucket);
      }
      return allMovies;
    }

    public List<Movie> GetTopBorrowedMovies(int count)
    {
      var allMovies = GetAllMovies();
      return allMovies.OrderByDescending(m => m.BorrowCount).Take(count).ToList();
    }

  }
}

