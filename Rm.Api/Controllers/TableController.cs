using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Rm.Api.Models;
using Rm.Core.Dtos;
using Rm.Services;
using System.Collections.Generic;

namespace Rm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TableController : Controller
    {
        private readonly ITableService _tableService;
        public TableController(ITableService tableService)
        {
            _tableService =tableService;
        }

        [HttpPost]
        public ActionResult<ApiResult<bool>> AddTable(TableDto tableDto)
        {
            _tableService.Add(tableDto);
            ApiResult<bool> apiResult = new ApiResult<bool>();
            apiResult.StatusCode=200;
            apiResult.Data=true;
            return Ok(apiResult);
        }

        [HttpGet]
        public ActionResult<ApiResult<List<TableDto>>> GetAll()
        {
            ServiceResponse<List<TableDto>> result = _tableService.GetAll();
            ApiResult<List<TableDto>> apiResult = new ApiResult<List<TableDto>>();
            apiResult.StatusCode=200;
            apiResult.Data = result.Data;
            return Ok(apiResult);
        }
    }

}
