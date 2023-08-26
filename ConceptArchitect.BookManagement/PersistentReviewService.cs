using ConceptArchitect.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class PersistentReviewService : IReviewService
    {
        IRepository<review, string> repository;

        //constructor based DI
        public PersistentReviewService(IRepository<review, string> repository)
        {
            this.repository = repository;
        }
        public async Task<review> AddReview(review review)
        {
            return await repository.Add(review);
        }

        public Task DeleteReview(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<review>> GetAllReviews()
        {
            return await repository.GetAll();
        }
    }
}
