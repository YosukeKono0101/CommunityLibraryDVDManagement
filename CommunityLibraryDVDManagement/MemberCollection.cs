using System;
namespace CommunityLibraryDVDManagement
{
  public class MemberCollection
  {
    private List<Member> members;

    public MemberCollection()
    {
      members = new List<Member>();
    }

    public void AddMember(Member member)
    {
      if(!members.Any(m=>m.FirstName == member.FirstName && m.LastName == member.LastName))
      {
        members.Add(member);
      }
    }

    public bool RemoveMember(string firstName, string lastName)
    {
      var memberToRemove = members.Find(m => m.FirstName == firstName && m.LastName == lastName);
      if(memberToRemove != null)
      {
        members.Remove(memberToRemove);
        return true;
      }
      return false;
    }

    public Member FindMember(string firstName, string lastName)
    {
      return members.Find(m => m.FirstName == firstName && m.LastName == lastName);
    }

    public List<Member> FindMembersWithMovie(string movieTitle)
    {
      return members.Where(member => member.GetBorrowedMovies().Contains(movieTitle)).ToList();
    }
  }
}

