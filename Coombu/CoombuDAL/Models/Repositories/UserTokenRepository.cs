using Coombu.Exceptions;
using Coombu.Utils;

namespace Coombu.Models.Repositories
{
    public class UserTokenRepository : IUserTokenRepository
    {
        private CoombuContext _db;

        public UserTokenRepository() : this(new CoombuContext()) { }

        public UserTokenRepository(CoombuContext context)
        {            
            this._db = context;
        }

        public UserToken Add(UserProfile user)
        {
            string token = Tokenizer.GenerateToken(user.ToString());
            UserToken userToken = new UserToken
            {
                token = token,
                User = user
            };

            _db.UsersToken.Add(userToken);
            _db.SaveChanges();
            return userToken;
        }

        public void Remove(string token)
        {
            UserToken tokenToDelete = _db.UsersToken.Find(token);
            if (tokenToDelete == null)
            {
                throw new UserTokenNotFoundException(string.Format("token : {0} not found",token));
            }
            _db.UsersToken.Remove(tokenToDelete);
        }


        public UserToken Get(string token)
        {
            UserToken userToken = _db.UsersToken.Find(token);
            if (userToken == null)
            {
                throw new UserTokenNotFoundException(string.Format("token : {0} not found", token));
            }
            return userToken;
        }
    }
}
