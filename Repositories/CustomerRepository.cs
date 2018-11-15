using System.Collections.Generic;
using System.Threading.Tasks;
using HsSports.Contracts;
using HsSports.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HsSports.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly H_Plus_SportsContext _ctx;
        public CustomerRepository( H_Plus_SportsContext context)
        {
            _ctx = context;
        }
        public async Task<Customer> Add(Customer customer)
        {
            await _ctx.Customer.AddAsync(customer);
            await _ctx.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> Exists(int id)
        {
            return await _ctx.Customer.AnyAsync(c => c.CustomerId == id);
        }

        public async Task<Customer> Find(int id)
        {
            return await _ctx.Customer.SingleOrDefaultAsync(c => c.CustomerId == id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _ctx.Customer;
        }

        public async Task<Customer> Remove(int id)
        {
            var customer = await _ctx.Customer.SingleOrDefaultAsync(c => c.CustomerId == id);
            _ctx.Remove(customer);
            await _ctx.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> Update(Customer customer)
        {
            _ctx.Customer.Update(customer);
            await _ctx.SaveChangesAsync();
            return customer;
        }
    }
}
