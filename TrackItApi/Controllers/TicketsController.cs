using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SQLitePCL;
using TrackItApi.Data;
using TrackItApi.DTOs;
using TrackItApi.Models;

namespace TrackItApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetTickets()
        {
            var tickets = await _context.Tickets
            .Include(t => t.CreatedByUser)
            .Include(t => t.Assignees)
                .ThenInclude(a => a.User)
            .Include(t => t.Asset)
            .ToListAsync();

            return tickets.Select(t => new TicketDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                CreatedAt = t.CreatedAt,
                AssetId = t.AssetId,
                AssetName = t.Asset?.Name,
                CreatedByUserId = t.CreatedByUserId,
                CreatedByUserName = t.CreatedByUser != null 
                    ? $"{t.CreatedByUser.FirstName} {t.CreatedByUser.LastName}" 
                    : null,
                Assignees = t.Assignees.Select(a => new AssigneeDto
                {
                    UserId = a.UserId,
                    FullName = $"{a.User.FirstName} {a.User.LastName}"
                }).ToList()
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDto>> GetTicket(int id)
        {
            var ticket = await _context.Tickets
            .Include(t => t.CreatedByUser)
            .Include(t => t.Assignees)
                .ThenInclude(a => a.User)
            .Include(t => t.Asset)
            .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null) return NotFound();

            return new TicketDto
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status,
                CreatedAt = ticket.CreatedAt,
                AssetId = ticket.AssetId,
                AssetName = ticket.Asset?.Name,
                CreatedByUserId = ticket.CreatedByUserId,
                CreatedByUserName = ticket.CreatedByUser != null
                    ? $"{ticket.CreatedByUser.FirstName} {ticket.CreatedByUser.LastName}"
                    : null,
                Assignees = ticket.Assignees.Select(a => new AssigneeDto
                {
                    UserId = a.UserId,
                    FullName = $"{a.User.FirstName} {a.User.LastName}"
                }).ToList()
            };
        }
        
            [HttpPost]
        public async Task<ActionResult<TicketDto>> CreateTicket(CreateTicketDto createDto)
        {
            var ticket = new Ticket
            {
                Title = createDto.Title,
                Description = createDto.Description,
                AssetId = createDto.AssetId,
                CreatedByUserId = createDto.CreatedByUserId
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            // Re-fetch with all related data
            var createdTicket = await _context.Tickets
                .Include(t => t.CreatedByUser)
                .Include(t => t.Asset)
                .Include(t => t.Assignees)
                    .ThenInclude(a => a.User)
                .FirstOrDefaultAsync(t => t.Id == ticket.Id);

            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, new TicketDto
            {
                Id = createdTicket!.Id,
                Title = createdTicket.Title,
                Description = createdTicket.Description,
                Status = createdTicket.Status,
                CreatedAt = createdTicket.CreatedAt,
                AssetId = createdTicket.AssetId,
                AssetName = createdTicket.Asset?.Name,
                CreatedByUserId = createdTicket.CreatedByUserId,
                CreatedByUserName = createdTicket.CreatedByUser != null
                    ? $"{createdTicket.CreatedByUser.FirstName} {createdTicket.CreatedByUser.LastName}"
                    : null,
                Assignees = createdTicket.Assignees.Select(a => new AssigneeDto
                {
                    UserId = a.UserId,
                    FullName = $"{a.User.FirstName} {a.User.LastName}"
                }).ToList()
            });
        }
    }   
}