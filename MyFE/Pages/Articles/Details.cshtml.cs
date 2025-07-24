using BussinessObjects.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace MyFE.Pages.Articles
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DetailsModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public ArticleDetailDTO? Article { get; set; }

        [BindProperty]
        public CommentDTO Comment { get; set; } = new();
        public List<CommentDTO> CommentList { get; set; } = new();

        [TempData]
        public string? SuccessMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var token = User.FindFirst("Token")?.Value;

            if (string.IsNullOrEmpty(token))
            {
                await HttpContext.SignOutAsync("MyCookieAuth");
                return RedirectToPage("/Auth/Login", new { returnUrl = Request.Path });
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var articlesResponse = await _httpClient.GetAsync($"articles/{Id}");
            if (articlesResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                articlesResponse.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                await HttpContext.SignOutAsync("MyCookieAuth");
                return RedirectToPage("/Auth/Login", new { returnUrl = Request.Path });
            }
            if (articlesResponse.IsSuccessStatusCode)
            {
                Article = await articlesResponse.Content.ReadFromJsonAsync<ArticleDetailDTO>();
            }

            var response = await _httpClient.GetAsync($"comments/{Id}");
            if (response.IsSuccessStatusCode)
            {
                CommentList = await response.Content.ReadFromJsonAsync<List<CommentDTO>>();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = User.FindFirst("Token")?.Value;

            if (string.IsNullOrEmpty(token))
            {
                await HttpContext.SignOutAsync("MyCookieAuth");
                return RedirectToPage("/Auth/Login", new { returnUrl = Request.Path });
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                ModelState.AddModelError("", "You need to log in to enrollment.");
                await OnGetAsync();
                return Page();
            }

            Comment.UserId = userId;
            Comment.ArticleId = Id;
            Comment.CreatedAt = DateTime.Now;

            var res = await _httpClient.PostAsJsonAsync("comments", Comment);
            if (res.IsSuccessStatusCode)
            {
                SuccessMessage = "Comment added successfully!";
                return RedirectToPage(new { id = Id });
            }
            else if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                     res.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                await HttpContext.SignOutAsync("MyCookieAuth");
                return RedirectToPage("/Auth/Login", new { returnUrl = Request.Path });
            }

            ModelState.AddModelError("", "Failed to add comments");
            await OnGetAsync();
            return Page();
        }
    }
}
