﻿using Business.Repositories.IRepository;
using Business.Repositories.IRepository.References;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using LogSuite.Shared.Models.References;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GisInputValueController : Controller
    {
        private IGisInputValueRepository _repository;

        public GisInputValueController(IGisInputValueRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GisInputValueDTO dto)
        {
            if (ModelState.IsValid)
            {
                var isUnique = await _repository.IsUnique(dto);
                if (isUnique == null)
                {
                    var result = await _repository.Create(dto);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ErrorModel()
                    {
                        Title = "",
                        ErrorMessage = "GisInputValue with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new GisInputValue"
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisInputValue Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var result = await _repository.Delete(id.Value);
            if (result == 0)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisInputValue Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            if (result == -1)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Can't delete GisInputValue with something assigned",
                    StatusCode = StatusCodes.Status409Conflict
                });
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisInputValue Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var entity = await _repository.Get(id.Value);
            if (entity == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisInputValue Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(entity);
        }

        [HttpGet("bygisidpaged/{id}")]
        public async Task<IActionResult> Get([FromQuery] Params parameters, int? id)
        {
            if (id == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisInputValue Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var pagedEntities = await _repository.GetPagedByGisId(id.Value, parameters);
            if (pagedEntities == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisInputValue Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            var entities = pagedEntities.ToList();
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedEntities.MetaData));
            return Ok(entities);
        }

        [HttpGet("bygisidondate/{id}")]
        public async Task<IActionResult> Get([FromQuery] DateTime date, int? id)
        {
            if (id == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisInputValue Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var entity = await _repository.GetOnDateByGisId(id.Value, date);
            if (entity == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisInputValue Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(entity);
        }

        [HttpGet("bygisidondaterange/{id}")]
        public async Task<IActionResult> Get([FromQuery] DateTime dateStart, [FromQuery] DateTime dateEnd, int? id)
        {
            if (id == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisInputValue Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var entities = await _repository.GetOnDateRangeByGisId(id.Value, dateStart, dateEnd);
            if (entities == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisInputValue Id or values not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(entities);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] GisInputValueDTO dto, int? id)
        {
            if (id == null || id != dto.Id)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid GisInputValue Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            if (ModelState.IsValid)
            {
                var isUnique = await _repository.IsUnique(dto, dto.Id);
                if (isUnique == null)
                {
                    var result = await _repository.Update(dto);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ErrorModel()
                    {
                        Title = "",
                        ErrorMessage = "GisInputValue with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while updating GisInputValue"
                });
            }
        }

    }
}
