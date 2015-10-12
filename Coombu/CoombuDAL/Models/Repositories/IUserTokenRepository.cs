namespace Coombu.Models.Repositories
{
    public interface IUserTokenRepository
    {
        UserToken Add(UserProfile user);
        UserToken Get(string token);
        void Remove(string token);        
    }
}
