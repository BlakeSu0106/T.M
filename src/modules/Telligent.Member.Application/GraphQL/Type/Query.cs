using AutoMapper;
using HotChocolate.Data;
using HotChocolate.Types;
using Telligent.Member.Application.GraphQL.Attribute;
using Telligent.Member.Database;

namespace Telligent.Member.Application.GraphQL.Type
{
    public class Query
    {
        /// <summary>
        /// 會員
        /// </summary>
        [UseFirstOrDefault]
        public IQueryable<Member> GetMember(Guid id, MemberDbContext dbContext, IMapper mapper)
        {
            var member = dbContext.Members.Where(x => x.Id == id);
            return mapper.ProjectTo<Member>(member);
        }

        /// <summary>
        /// 公司
        /// </summary>
        [UseTemplate]
        [UsePaging(IncludeTotalCount = true)]
        public IQueryable<Company> GetCompanies(MemberDbContext dbContext, IMapper mapper)
        {
            var companies = dbContext.Company;
            return mapper.ProjectTo<Company>(companies);
        }
    }
}
