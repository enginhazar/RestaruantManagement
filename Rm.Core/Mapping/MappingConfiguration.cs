using AutoMapper;
using Rm.Core.Dtos;
using Rm.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rm.Core.Mapping
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {

            CreateMap<Table, TableDto>().ReverseMap();
            CreateMap<Reservation, ReservationDto>().ReverseMap();
        }
    }
}
