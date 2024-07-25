using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class FeedbackRepository : EfRepositoryBase<Feedback, Guid, BaseDbContext>, IFeedbackRepository
    {
        public FeedbackRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
