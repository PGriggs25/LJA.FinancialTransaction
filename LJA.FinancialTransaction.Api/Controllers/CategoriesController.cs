using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LJA.FinancialTransaction.Models;
using LJA.FinancialTransaction.Persistence.DbContexts;

namespace LJA.FinancialTransaction.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly FinancialTransactionDbContext _context;

        public CategoriesController(FinancialTransactionDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> Categories()
        {
            var cats = await _context.Categories.ToListAsync();
            return Ok(cats);
        }
    }
}
