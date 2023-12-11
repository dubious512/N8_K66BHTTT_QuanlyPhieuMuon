using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreWeb_MVC.Models;
using Newtonsoft.Json;


namespace CoreWeb_MVC.Controllers
{
    public class TicketsController : Controller
    {
        private readonly TicketDbContext _context;

        public TicketsController(TicketDbContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index(string _ticketId)
        {
            ViewData["CurrentFilter"] = _ticketId;
            var _ticket = from t in _context.tickets
                          select t;
            if (!String.IsNullOrEmpty(_ticketId))
            {
                _ticket = _ticket.Where(t => t.ticket_id.Contains(_ticketId));
            }
            return View(_ticket);
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.tickets
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ticket == null)
            {
                return NotFound();
            }
			// Lấy user_id từ ticket
			string user_id = ticket.user_id;
            
			// Gọi API để lấy thông tin về name, phone, email của user_id
			HttpClient httpClient = new HttpClient();
			HttpResponseMessage response = await httpClient.GetAsync($"https://localhost:7247/api/users/{user_id}");
			if (response.IsSuccessStatusCode)
			{
				var userData = await response.Content.ReadAsAsync<User>();

				// Truyền thông tin vào ViewData
				ViewData["user_name"] = userData.user_name;
				ViewData["user_phone"] = userData.user_phonenumber;
				ViewData["user_email"] = userData.user_email;
			}
			else
			{
				// Xử lý khi gọi API không thành công
				ViewData["user_name"] = "N/A";
				ViewData["user_phone"] = "N/A";
				ViewData["user_email"] = "N/A";
			}
			// Kiểm tra trạng thái hết hạn
			DateTime currentDate = DateTime.Now.Date;
			DateTime ticketDate = ticket.Date.Date;
			TimeSpan timeSinceTicketCreation = currentDate - ticketDate;
			bool isExpired = timeSinceTicketCreation.TotalDays > 7;

			// Ghi chú trạng thái hết hạn
			if (isExpired)
			{
				ViewData["expiration_status"] = "Hết hạn";
			}
			else
			{
				ViewData["expiration_status"] = "Còn hạn";
			}


			return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ticket_id,user_id,book_name,Date")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra user_id
                bool isUserIdValid = await CheckUserIdValidity(ticket.user_id);

                if (!isUserIdValid)
                {
                    ModelState.AddModelError("user_id", "Mã người dùng không hợp lệ");
                    return View(ticket);
                }

                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        private async Task<bool> CheckUserIdValidity(string userId)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7247/api/users");
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    List<User> users = JsonConvert.DeserializeObject<List<User>>(responseBody);

                    return users.Any(u => u.user_id.ToString() == userId);
                }
                return false;
            }
        }
        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ticket_id,user_id,book_name,Date")] Ticket ticket)
        {
            if (id != ticket.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.tickets
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.tickets.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.tickets.Any(e => e.ID == id);
        }
    }
}
