using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PrimerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [DisableCors]
    public class FormController : ControllerBase
    {
        private readonly IFormRepository _formRepository;
        private readonly IMapper _mapper;

        public FormController(IFormRepository formRepository, IMapper mapper)
        {
            _formRepository = formRepository;
            _mapper = mapper;
        }
        
        // GET: api/Form
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var results = _formRepository.GetAllForms();

                List<FormModel> models = _mapper.Map<List<FormModel>>(results);

                return Ok(models);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // GET: api/Form/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _formRepository.GetFormById(id);

                if (result == null) return NotFound();

                FormModel model = _mapper.Map<FormModel>(result);

                return Ok(model);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // POST: api/Form
        [HttpPost]
        public IActionResult Post([FromBody] FormModel model)
        {
            try
            {
                var form = _mapper.Map<Form>(model);

                if(ModelState.IsValid)
                {
                    _formRepository.AddForm(form);
                    return Ok(form);
                }
                else return this.StatusCode(StatusCodes.Status422UnprocessableEntity, "Model nije dobar");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // PUT: api/Form/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FormModel model)
        {
            try
            {
                var oldForm = _formRepository.GetFormById(id);
                if (oldForm == null) return NotFound("Could not find form with id = {id}");

                var form = _mapper.Map<Form>(model);

                if(ModelState.IsValid)
                {
                    _formRepository.UpdateForm(id, form);
                    return Ok(form);
                }
                else return this.StatusCode(StatusCodes.Status422UnprocessableEntity, "Model nije dobar");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var oldForm = _formRepository.GetFormById(id);
                if (oldForm == null) return NotFound();

                _formRepository.DeleteForm(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("Failed to delete form");
            }
        }
    }
}