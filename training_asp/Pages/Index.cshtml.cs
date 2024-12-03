using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;
using training_asp.Data;
using training_asp.Models;

namespace training_asp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // ����� ��� ��������� GET-������� (����������� �����)
        public void OnGet()
        {
        }

        // ����� ��� ��������� POST-������� (�������� ������ � ������)
        public IActionResult OnPost()
        {
            // ���� ������������ � ����� ������� � ������� � ���� ������
            var user = _context.Users
                .FirstOrDefault(u => u.Login == Login && u.Password == Password);

            if (user != null)
            {
                // ��������� ID ������������ � TempData (��� � Session)
                TempData["UserId"] = user.Id;
                HttpContext.Session.SetInt32("UserId", user.Id);

                if (user.Role == 1)
                {
                    Console.WriteLine($"UserId �� TempData: {TempData["UserId"]}");
                    return RedirectToPage("/HomePage");  // ��������� �� ������� ��������
                }
                else
                {
                    return RedirectToPage("/ModeratorPage");  // ��������� �� �������� ����������
                }
            }
            else
            {
                // ���� ������������ �� ������, ��������� ������ � ModelState
                ModelState.AddModelError(string.Empty, "������������ ����� ��� ������.");
                return Page();  // ���������� �� �� �������� � �������
            }
        }

    }
}
