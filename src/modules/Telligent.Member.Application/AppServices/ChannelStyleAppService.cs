using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using Telligent.Member.Application.Dtos.ChannelStyle;
using Telligent.Member.Domain.Channels;

namespace Telligent.Member.Application.AppServices
{
    public class ChannelStyleAppService : CrudAppService<ChannelStyle, ChannelStyleDto, ChannelStyleDto, ChannelStyleDto>
    {
        public ChannelStyleAppService(IRepository<ChannelStyle> repository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(repository, mapper, httpContextAccessor)
        {
        }
    }
}
