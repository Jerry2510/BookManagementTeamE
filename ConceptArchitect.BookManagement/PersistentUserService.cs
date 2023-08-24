using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConceptArchitect.BookManagement
{
    public class PersistentUserService : IUserService
    {
        IRepository<User, string> repository;

        //constructor based DI
        public PersistentUserService(IRepository<User, string> repository)
        {
            this.repository = repository;
        }
        public async Task<User> AddUser(User author)
        {
            //perform some validation if needed
            if (author == null)
                throw new InvalidDataException("User can't be null");

            return await repository.Add(author);
        }
        public async Task DeleteUser(string authorId)
        {
            await repository.Delete(authorId);
        }
        public async Task<List<User>> GetAllUser()
        {
            return await repository.GetAll();
        }
        public async Task<User> GetUserById(string id)
        {
            return await repository.GetById(id);
        }
        public async Task<List<User>> SearchUser(string term)
        {
            term = term.ToLower();

            return await repository.GetAll(a => a.Email.ToLower().Contains(term) || a.Name.ToLower().Contains(term));
        }
        public async Task<User> UpdateUser(User author)
        {
            return await repository.Update(author, (old, newDetails) =>
            {
                old.Email = newDetails.Email;
                old.Password = newDetails.Password;
                old.Name = newDetails.Name;
                old.ProfilePhoto = newDetails.ProfilePhoto;
            });
        }
    }
}