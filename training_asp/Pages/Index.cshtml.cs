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

        // Метод для обработки GET-запроса (отображение формы)
        public void OnGet()
        {
        }

        // Метод для обработки POST-запроса (проверка логина и пароля)
        public IActionResult OnPost()
        {
            // Ищем пользователя с таким логином и паролем в базе данных
            var user = _context.Users
                .FirstOrDefault(u => u.Login == Login && u.Password == Password);

            if (user != null)
            {
                // Сохраняем ID пользователя в TempData (или в Session)
                TempData["UserId"] = user.Id;
                HttpContext.Session.SetInt32("UserId", user.Id);

                if (user.Role == 1)
                {
                    Console.WriteLine($"UserId из TempData: {TempData["UserId"]}");
                    return RedirectToPage("/HomePage");  // Переходим на главную страницу
                }
                else
                {
                    return RedirectToPage("/ModeratorPage");  // Переходим на страницу модератора
                }
            }
            else
            {
                // Если пользователь не найден, добавляем ошибку в ModelState
                ModelState.AddModelError(string.Empty, "Неправильный логин или пароль.");
                return Page();  // Возвращаем ту же страницу с ошибкой
            }
        }

    }
}
