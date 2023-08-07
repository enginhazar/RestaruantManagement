using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Rm.Api.Models;
using Rm.Core.Dtos;
using Rm.Services;
using System;
using System.Collections.Generic;

namespace Rm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly ISmsService _smsService;

        public ReservationController(IReservationService reservationService, ISmsService smsService)
        {
            _reservationService =reservationService;
            _smsService = smsService;
        }

        [Route("GetAviableTables")]
        [HttpPost]
        public ActionResult<ApiResult<List<TableDto>>> GetAviableTables(GetAviableTablesRequest request)
        {
            ServiceResponse<List<TableDto>> result = _reservationService.GetavailabilityTables(request.ReservationDateTime, request.Size);
            ApiResult<List<TableDto>> apiResult = new ApiResult<List<TableDto>>();

            apiResult.StatusCode= 200;
            if (result.Success)
                return Ok(apiResult.Data=result.Data);
            else
            {
                apiResult.StatusCode=400;
                apiResult.ErrorMessage=result.Error.ErrorMessage;
                return BadRequest(apiResult);
            }
        }

        [Route("MakeReservation")]
        [HttpPost]
        public ActionResult<ApiResult<ReservationDto>> MakeReservation(ReservationDto request)
        {
            ApiResult<ReservationDto> apiResult = new ApiResult<ReservationDto>();
            ServiceResponse<ReservationDto> result = _reservationService.CreateReservation(request);

            apiResult.StatusCode=200;
            if (result.Success)
            {
                ServiceResponse<bool> smsResult = _smsService.SendSms(request);

                apiResult.Data=result.Data;
                return Ok(apiResult);
            }
            else
            {
                apiResult.StatusCode=400;
                apiResult.ErrorMessage = result.Error.ErrorMessage;
                return BadRequest(apiResult);
            }

        }

        [HttpGet]
        public ActionResult<ApiResult<List<ReservationDto>>> GetAll()
        {
            ServiceResponse<List<ReservationDto>> result = _reservationService.GetAll();
            ApiResult<List<ReservationDto>> apiResult = new ApiResult<List<ReservationDto>>();
            apiResult.StatusCode=200;
            apiResult.Data = result.Data;
            return Ok(apiResult);
        }

    }
}
