using BussinessObjects.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace MyFE.Pages.Articles
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ApiClient");
        }

        public List<ArticleDTO> Articles { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var token = User.FindFirst("Token")?.Value;

            if (string.IsNullOrEmpty(token))
            {
                await HttpContext.SignOutAsync("MyCookieAuth");
                return RedirectToPage("/Auth/Login", new { returnUrl = Request.Path });
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("articles");

            if (response.IsSuccessStatusCode)
            {
                var articless = await response.Content.ReadFromJsonAsync<List<ArticleDTO>>();
                if (articless != null)
                {
                    Articles = articless;
                }
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                     response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                await HttpContext.SignOutAsync("MyCookieAuth");
                return RedirectToPage("/Auth/Login", new { returnUrl = Request.Path });
            }

            return Page();
        }
    }
}
