using AutoMapper;
using Microsoft.Extensions.Logging;
using Rm.Core.Dtos;
using Rm.Core.Entity;
using Rm.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rm.Services
{
    public class TableService : ITableService
    {
        private readonly IRepository<Table> _tableRepository;
        private readonly ILogger<ReservationService> _logger;
        private readonly IMapper _mapper;
        public TableService(ILogger<ReservationService> logger,
            IMapper mapper, IRepository<Table> tableRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _tableRepository =tableRepository;
        }
        public void Add(TableDto tableDto)
        {
            _tableRepository.Add(_mapper.Map<Table>(tableDto));
        }

        public ServiceResponse<List<TableDto>> GetAll()
        {
            ServiceResponse<List<TableDto>> result = new ServiceResponse<List<TableDto>>();
            result.Success=true;

            var data= _tableRepository.GetAll().ToList();
            result.Data = _mapper.Map<List<TableDto>>(data);
            return result;
        }
    }
}
