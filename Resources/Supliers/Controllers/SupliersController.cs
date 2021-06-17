using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hondaerp.Data;
using hondaerp.Supliers.Models;
using hondaerp.Supliers.Dtos;
using hondaerp.Supliers.Repositories;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace hondaerp.Supliers.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SuplierController : ControllerBase
    {
        private readonly SuplierRepository _repository;
        private readonly IMapper _mapper;

        public SuplierController(SuplierRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuplierReadDto>>> FindAll()
        {
            var supliers = await _repository.FindAll();
            
            var supliersRead = _mapper.Map<IEnumerable<SuplierReadDto>>(supliers);

            return Ok(supliersRead);
        }

        // GET: Suplier/Details/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SuplierReadDto>> FindOne(int id)
        {
            var suplier = await _repository.FindOne(id);
            var suplierRead = _mapper.Map<SuplierReadDto>(suplier);
            
            return Ok(suplierRead);
        }

        // POST: Suplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<SuplierReadDto>> Create(SuplierCreateDto suplierCreate)
        {
            var cnpjExists = await _repository.CpnjExists(suplierCreate.CNPJ);

            if(cnpjExists)
            {
                return ValidationProblem("CNPJ already exists");
            }

            try{
                var suplier = _mapper.Map<Suplier>(suplierCreate);
                var now = DateTime.Now;

                suplier.CreatedAt = now;
                suplier.UpdatedAt = now;

                await _repository.Create(suplier);
                var suplierRead = _mapper.Map<SuplierReadDto>(suplier);
                return Created("/Suplier",suplierRead);
            }

            catch(DbUpdateException error)
            {
                var baseException = error.GetBaseException();
                return Conflict(baseException.GetType());
            }

            
        }

        // POST: Suplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult<SuplierReadDto>> Edit(SuplierUpdateDto suplierUpdate)
        {
            var exists = await _repository.Exists(suplierUpdate.Id);

            if (!exists)
            {
                return ValidationProblem("Invalid suplier");
            }

            var suplier = _mapper.Map<Suplier>(suplierUpdate);
            var suplierRead = _mapper.Map<SuplierReadDto>(await _repository.Update(suplier));
            
            return Ok(suplierRead);
        }

        // // DELETE: Suplier
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.Delete(id);

            return Ok();
        }
    }
}
