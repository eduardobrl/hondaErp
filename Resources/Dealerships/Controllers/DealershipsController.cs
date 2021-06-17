using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hondaerp.Data;
using hondaerp.Dealerships.Models;
using hondaerp.Dealerships.Dtos;
using hondaerp.Dealerships.Repositories;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace hondaerp.Dealerships.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DealershipController : ControllerBase
    {
        private readonly DealershipsRepository _repository;
        private readonly IMapper _mapper;

        public DealershipController(DealershipsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DealershipReadDto>>> FindAll()
        {
            var dealerships = await _repository.FindAll();
            
            var dealershipsRead = _mapper.Map<IEnumerable<DealershipReadDto>>(dealerships);

            return Ok(dealershipsRead);
        }

        // GET: Dealership/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DealershipReadDto>> FindOne(int id)
        {
            var dealership = await _repository.FindOne(id);
            var dealershipRead = _mapper.Map<DealershipReadDto>(dealership);
            
            return Ok(dealershipRead);
        }

        // POST: Dealership/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<DealershipReadDto>> Create(DealershipCreateDto dealershipCreate)
        {
            var cnpjExists = await _repository.CpnjExists(dealershipCreate.CNPJ);

            if(cnpjExists)
            {
                return ValidationProblem("CNPJ already exists");
            }

            try{
                var dealership = _mapper.Map<Dealership>(dealershipCreate);
                var now = DateTime.Now;

                dealership.CreatedAt = now;
                dealership.UpdatedAt = now;

                await _repository.Create(dealership);
                var dealershipRead = _mapper.Map<DealershipReadDto>(dealership);
                return Created("/Dealership",dealershipRead);
            }

            catch(DbUpdateException error)
            {
                var baseException = error.GetBaseException();
                return Conflict(baseException.GetType());
            }

            
        }

        // POST: Dealership/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<DealershipReadDto>> Edit(DealershipUpdateDto dealershipUpdate)
        {
            var exists = await _repository.Exists(dealershipUpdate.Id);

            if (!exists)
            {
                return ValidationProblem("Invalid dealership");
            }


            var dealership = _mapper.Map<Dealership>(dealershipUpdate);
            var dealershipRead = _mapper.Map<DealershipReadDto>(await _repository.Update(dealership));
            
            return Ok(dealershipRead);
        }

        // // DELETE: Dealership
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.Delete(id);

            return Ok();
        }
    }
}
