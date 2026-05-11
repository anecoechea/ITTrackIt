using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackItApi.Data;
using TrackItApi.DTOs;
using TrackItApi.Models;

namespace TrackItApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role = u.Role,
                Department = u.Department,
                Team = u.Team,
                IsActive = u.IsActive
            }).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                Department = user.Department,
                Team = user.Team,
                IsActive = user.IsActive
            };
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createdUser)
        {
            var user = new User
            {
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                Email = createdUser.Email,
                Role = createdUser.Role,
                Department = createdUser.Department,
                Team = createdUser.Team,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new {id = user.Id}, new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                Department = user.Department,
                Team = user.Team,
                IsActive = user.IsActive
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, CreateUserDto updatedUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Department = updatedUser.Department;
            user.Email = updatedUser.Email;
            user.Role = updatedUser.Role;
            user.Team = updatedUser.Team;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            user.IsActive = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}/purge")]
        public async Task<ActionResult<User>> PurgeUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        
    }
}