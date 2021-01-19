namespace TAK.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INewsService
    {
        T GetByName<T>(string name);

        ICollection<string> GetPictureUrls(int id);

        Task<int> CreateAsync(string title, string content, string userId, List<string> imageUrls, string latinTitle);

        IEnumerable<T> GetAll<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetLast<T>(int count);

        int GetNewsCount();
    }
}
