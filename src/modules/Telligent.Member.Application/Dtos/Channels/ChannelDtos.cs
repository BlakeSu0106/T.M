using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telligent.Core.Application.DataTransferObjects;
using Telligent.Member.Domain.Shared.Channels;

namespace Telligent.Member.Application.Dtos.Channels
{
    public class ChannelDtos : EntityDto
    {
        /// <summary>
        /// 公司識別碼
        /// </summary>
        public Guid CompanyId { get; set; }
        /// <summary>
        /// 渠道識別碼
        /// </summary>
        public Guid ChannelId { get; set; }
        /// <summary>
        /// 渠道類別
        /// </summary>
        public ChannelType ChannelType { get; set; }

        public static implicit operator ChannelDtos(ChannelDto v)
        {
            throw new NotImplementedException();
        }
    }
}
