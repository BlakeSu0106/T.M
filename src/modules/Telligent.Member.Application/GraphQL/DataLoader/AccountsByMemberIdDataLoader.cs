using AutoMapper;
using GreenDonut;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Telligent.Member.Application.GraphQL.Type;
using Telligent.Member.Database;

namespace Telligent.Member.Application.GraphQL.DataLoader
{
    public class AccountsByMemberIdDataLoader : GroupedDataLoader<Guid, Account>
    {
        private readonly IDbContextFactory<MemberDbContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public AccountsByMemberIdDataLoader(IDbContextFactory<MemberDbContext> dbContextFactory, IMapper mapper, IBatchScheduler batchScheduler, DataLoaderOptions options = null)
            : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        protected override async Task<ILookup<Guid, Account>> LoadGroupedBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

            var accounts = dbContext.Accounts.Where(x => keys.Contains(x.MemberId));
            return _mapper.ProjectTo<Account>(accounts).ToLookup(x => x.MemberId);
        }
    }
}
