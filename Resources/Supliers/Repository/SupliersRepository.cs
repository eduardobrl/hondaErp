using System;
using hondaerp.Data;
using hondaerp.Supliers.Models;
using hondaerp.Supliers.Dtos;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace hondaerp.Supliers.Repositories
{
    public class SuplierRepository
    {
        private readonly ApplicationDbContext _context;

        public SuplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Suplier> Create(Suplier suplier)
        {
            if(suplier == null)
            {
                throw new ArgumentNullException(nameof(suplier));
            }

            if(await this.CpnjExists(suplier.CNPJ))
            {
                throw new Exception("CNPJ already exists");
            }
            
            await _context.Suplier.AddAsync(suplier);
            await _context.SaveChangesAsync();

            return suplier;
        }


        public async Task Delete(int Id)
        {
            var suplier = await _context.Suplier.FindAsync(Id);
            _context.Suplier.Remove(suplier);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Suplier>> FindAll()
        {
            return await _context.Suplier.ToListAsync();
        }

        public async Task<Suplier> FindOne(int Id)
        {
            return await _context.Suplier.FindAsync(Id);
        }

        public async Task<Suplier> Update(Suplier suplier)
        {
            if(suplier == null)
            {
                throw new ArgumentNullException(nameof(suplier));
            }

            _context.Suplier.Update(suplier);
            await _context.SaveChangesAsync();

            return suplier;
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Suplier.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> CpnjExists(string cnpj)
        {
            return await _context.Suplier.AnyAsync(e => e.CNPJ == cnpj);
        }


    }
}