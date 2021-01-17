namespace TAK.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INewsService
    {
        T GetByName<T>(string name);

        ICollection<string> GetPictureUrls(int id);

        IEnumerable<T> GetAll<T>(int? take = null, int skip = 0);

        int GetNewsCount();
    }
}
