using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using basic_authentication.Data;
using basic_authentication.Models;
using Microsoft.AspNetCore.Authorization;

namespace basic_authentication.Pages
{
    //[Authorize]
    public class IndexModel : PageModel
    {
        private readonly basic_authentication.Data.ApplicationDbContext _context;

        public IndexModel(basic_authentication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<ToDoItem> ToDoItem { get;set; } = default!;

        [BindProperty]
        public ToDoItem NewToDo { get; set; } = new();

        public async Task OnGetAsync()
        {
            var currentUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            ToDoItem = await _context.ToDoItem
                .Where(task=>task.OwnerId == currentUserId)
                .ToListAsync();
        }

        public async Task <IActionResult> OnPostAddAsync()
        {
            var myUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var allClaims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
            if (myUserId == null)
            {
                return RedirectToPage(); 
            }
            NewToDo.OwnerId = myUserId;
            _context.ToDoItem.Add(NewToDo);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var myUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var allClaims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
            if (myUserId == null)
            {
                return RedirectToPage(); 
            }
            NewToDo.OwnerId = myUserId;
            _context.ToDoItem.Add(NewToDo);
            await _context.SaveChangesAsync();
            return RedirectToPage();

        }

        public async Task<IActionResult> OnPostToggleAsync(int id)
        {
            var task = await _context.ToDoItem.FindAsync(id);

            if (task !=null)
            {
                task.IsCompleted = !task.IsCompleted;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();

        }
    }
}
