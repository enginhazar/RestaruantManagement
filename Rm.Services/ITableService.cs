using Rm.Core.Dtos;
using Rm.Core.Entity;

namespace Rm.Services
{
    public interface ITableService
    {
        void Add(TableDto tableDto);
        ServiceResponse<List<TableDto>> GetAll();
    }
}