using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Store_API.Queries
{
    public class GetEstateByIdQuery : IRequest<Estate>
    {
        public uint Id { get; set; }
    }
}
