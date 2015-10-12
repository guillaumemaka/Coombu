using PagedList;

namespace Coombu.Models.Repositories
{
    public interface IUserRepository
    {
        IPagedList<UserProfile> GetAll( int page = 1, int size = 25);
        UserProfile Get(string username);
        UserProfile Get(int id);
        UserProfile Add(UserProfile user);
        UserProfile Update(UserProfile user);
        void Remove(int id);
    }
}
