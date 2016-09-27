using System.Threading.Tasks;
using Google.Apis.Books.v1.Data;

namespace BookJump.Services
{
    public interface IBooksService
    {
        Task<Volume> SearchTitleAsync(string title);
    }
}