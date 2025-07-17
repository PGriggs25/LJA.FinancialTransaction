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
    public class SourcesController : ControllerBase
    {
        private readonly FinancialTransactionDbContext _context;

        public SourcesController(FinancialTransactionDbContext context)
        {
            _context = context;
        }

        // GET: api/Sources
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sources>>> Sources()
        {
            var srcs = await _context.Sources.ToListAsync();
            return Ok(srcs);
        }
    }
}
