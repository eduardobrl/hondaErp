using System;
using hondaerp.Data;
using hondaerp.Dealerships.Models;
using hondaerp.Dealerships.Dtos;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace hondaerp.Dealerships.Repositories
{
    public class DealershipsRepository
    {
        private readonly ApplicationDbContext _context;

        public DealershipsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Dealership> Create(Dealership dealership)
        {
            if(dealership == null)
            {
                throw new ArgumentNullException(nameof(dealership));
            }

            if(await this.CpnjExists(dealership.CNPJ))
            {
                throw new Exception("CNPJ already exists");
            }
            
            await _context.Dealership.AddAsync(dealership);
            await _context.SaveChangesAsync();

            return dealership;
        }


        public async Task Delete(int Id)
        {
            var dealership = await _context.Dealership.FindAsync(Id);
            _context.Dealership.Remove(dealership);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Dealership>> FindAll()
        {
            return await _context.Dealership.ToListAsync();
        }

        public async Task<Dealership> FindOne(int Id)
        {
            return await _context.Dealership.FindAsync(Id);
        }

        public async Task<Dealership> Update(Dealership dealership)
        {
            if(dealership == null)
            {
                throw new ArgumentNullException(nameof(dealership));
            }

            _context.Dealership.Update(dealership);
            await _context.SaveChangesAsync();

            return dealership;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Dealership.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> CpnjExists(string cnpj)
        {
            return await _context.Dealership.AnyAsync(e => e.CNPJ == cnpj);
        }


    }
}