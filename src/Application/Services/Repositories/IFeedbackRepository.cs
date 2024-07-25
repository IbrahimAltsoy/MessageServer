using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Services.Repositories
{
    public interface IFeedbackRepository : IAsyncRepository<Feedback, Guid>, IRepository<Feedback, Guid> { }
}
