using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telligent.Member.Application.Dtos.MemberMapping
{
    public class MemberMappingBaseDto
    {
        public Guid MemberId { get; set; }
        public Guid OldMemberId { get; set; }
    }
}
