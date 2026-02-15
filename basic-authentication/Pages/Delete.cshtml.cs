using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using basic_authentication.Data;
using basic_authentication.Models;

namespace basic_authentication.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly basic_authentication.Data.ApplicationDbContext _context;

        public DeleteModel(basic_authentication.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ToDoItem ToDoItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoitem = await _context.ToDoItem.FirstOrDefaultAsync(m => m.Id == id);

            if (todoitem is not null)
            {
                ToDoItem = todoitem;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoitem = await _context.ToDoItem.FindAsync(id);
            if (todoitem != null)
            {
                ToDoItem = todoitem;
                _context.ToDoItem.Remove(ToDoItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
