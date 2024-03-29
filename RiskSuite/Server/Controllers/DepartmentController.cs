﻿using Business.Repositories.IRepository;
using LogSuite.Shared;
using LogSuite.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace LogSuite.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _departmentRepository.GetAll();
            return Ok(departments);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments([FromQuery] Params parameters)
        {
            var pagedDepartments = await _departmentRepository.GetPaged(parameters);
            var departments = pagedDepartments.ToList();
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagedDepartments.MetaData));
            return Ok(departments);
        }

        [HttpGet("{departmentId}")]
        public async Task<IActionResult> GetDepartment(int? departmentId)
        {
            if (departmentId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Department Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var department = await _departmentRepository.Get(departmentId.Value);
            if (department == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Department Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }

            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DepartmentDTO departmentDTO)
        {
            if (ModelState.IsValid)
            {
                var isUnique = await _departmentRepository.IsUnique(departmentDTO);
                if (isUnique == null)
                {
                    var result = await _departmentRepository.Create(departmentDTO);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ErrorModel()
                    {
                        Title = "",
                        ErrorMessage = "Department with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new department"
                });
            }
        }

        [HttpPut("{departmentId}")]
        public async Task<IActionResult> Update([FromBody] DepartmentDTO departmentDTO, int? departmentId)
        {
            if (departmentId == null || departmentId != departmentDTO.Id)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Department Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            if (ModelState.IsValid)
            {
                var isUnique = await _departmentRepository.IsUnique(departmentDTO, departmentDTO.Id);
                if (isUnique == null)
                {
                    var result = await _departmentRepository.Update(departmentDTO);
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new ErrorModel()
                    {
                        Title = "",
                        ErrorMessage = "Department with such fields already exist",
                        StatusCode = StatusCodes.Status406NotAcceptable
                    });
                }
            }
            else
            {
                return BadRequest(new ErrorModel()
                {
                    ErrorMessage = "Error while creating new department"
                });
            }
        }

        [HttpDelete("{departmentId}")]
        public async Task<IActionResult> DeleteDepartment(int? departmentId)
        {
            if (departmentId == null)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Department Id",
                    StatusCode = StatusCodes.Status400BadRequest
                });
            }
            var result = await _departmentRepository.Delete(departmentId.Value);
            if (result == 0)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Invalid Department Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            if (result == -1)
            {
                return BadRequest(new ErrorModel()
                {
                    Title = "",
                    ErrorMessage = "Can't delete department with accounts",
                    StatusCode = StatusCodes.Status409Conflict
                });
            }
            return Ok();
        }
    }
}
