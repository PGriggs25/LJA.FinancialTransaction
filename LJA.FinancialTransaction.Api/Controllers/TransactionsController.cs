using LJA.FinancialTransaction.Api.Extensions;
using LJA.FinancialTransaction.Models;
using LJA.FinancialTransaction.Models.QueryObjects;
using LJA.FinancialTransaction.Persistence.DbContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LJA.FinancialTransaction.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly FinancialTransactionDbContext _context;

        public TransactionsController(FinancialTransactionDbContext context)
        {
            _context = context;
        }

       //GET: api/Transactions
       [HttpGet]
        public async Task<ActionResult<IEnumerable<Transactions>>> Transactions([FromQuery] TransactionQueryObject query)
        {
            // Start with the full set
            var q = _context.Transactions.AsQueryable();

            // Apply filters only when the query property is set
            q = q
                .WhereIf(query?.Id != null, x => x.Id == query.Id)
                .WhereIf(query?.Amount != null, x=> x.Amount == query.Amount)
                .WhereIf(!string.IsNullOrWhiteSpace(query?.Description),x => x.Description == query.Description)
                .WhereIf(query?.Date != null, x => x.Date == query.Date)
                .WhereIf(query?.SourceId != null, x => x.SourceId == query.SourceId)
                .WhereIf(query?.CategoryId != null, x => x.CategoryId == query.CategoryId);

            // Execute
            var list = await q.ToListAsync();
            return Ok(list);
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transactions>> Transactions(int id)
        {
            var trans = await _context.Transactions.FindAsync(id);
            if (trans == null)
            {
                return NotFound();
            }
            return Ok(trans);
        }

        // POST: api/Transactions
        [HttpPost]
        public async Task<ActionResult<Transactions>> Transactions(Transactions transaction)
        {
            // Assume the Date is not valid if set to minimal value.  This might not always be true.
            if (transaction.Date == DateTime.MinValue)
            {
                return BadRequest($"Date {transaction.Date} is invalid.");
            }

            if (transaction.Amount <= 0)
            {
                return BadRequest($"Amount {transaction.Amount} is invalid.");
            }

            if (string.IsNullOrEmpty(transaction.Description))
            {
                return BadRequest($"Description is invalid.");
            }

            var categoryExists = await _context.Categories.AnyAsync(c => c.Id == transaction.CategoryId);

            if (!categoryExists)
                return BadRequest($"CategoryId {transaction.CategoryId} does not exist.");

            var sourceExists = await _context.Sources.AnyAsync(s => s.Id == transaction.SourceId);
            if (!sourceExists)
                return BadRequest($"SourceId {transaction.SourceId} does not exist.");

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Transactions), new { id = transaction.Id }, transaction);
        }
    }
}
