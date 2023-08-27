using ConceptArchitect.Data;
using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConceptArchitect.BookManagement.Repositories.Ado
{
    public class AdoUserRepository : IRepository<User, string>
    {
        DbManager db;
        public AdoUserRepository(DbManager db)
        {
            this.db = db;
        }

        public async Task<User> Add(User author)
        {
            var query = $"insert into users(email, password, name, profile_photo) " +
                              $"values('{author.Email}','{author.Password}','{author.Name}','{author.ProfilePhoto}')";

            await db.ExecuteUpdateAsync(query);
            return author;
        }
        public async Task Delete(string id)
        {
            await db.ExecuteUpdateAsync($"delete from users where email='{id}'");
        }
        private User UserExtractor(IDataReader reader)
        {
            return new User()
            {
                Email = reader["email"].ToString(),
                Password = reader["password"].ToString(),
                Name = reader["name"].ToString(),
                ProfilePhoto = reader["profile_photo"].ToString()



            };
        }
        public async Task<List<User>> GetAll()
        {
            return await db.QueryAsync("select * from users", UserExtractor);
        }
        public async Task<List<User>> GetAll(Func<User, bool> predicate)
        {
            var authors = await GetAll();
            return (from author in authors
                    where predicate(author)
                    select author).ToList();
        }
        public async Task<User> GetById(string id)
        {
            return await db.QueryOneAsync($"select * from users where email='{id}'", UserExtractor);
        }
        public async Task<User> Update(User entity, Action<User, User> mergeOldNew)
        {
            var oldAuthor = await GetById(entity.Email);
            if (oldAuthor != null)
            {
                mergeOldNew(oldAuthor, entity);
                var query = $"update authors set " +
                            $"email='{oldAuthor.Email}', " +
                            $"password='{oldAuthor.Password}', " +
                            $"name='{oldAuthor.Name}', " +
                            $"profile_photo='{oldAuthor.ProfilePhoto}'";

                await db.ExecuteUpdateAsync(query);
            }
            return entity;
        }

        public Task<List<User>> GetAllF()
        {
            throw new NotImplementedException();
        }

        public Task<User> fav(User entity, string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFav(string bookId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}