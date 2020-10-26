namespace TAK.Services.Data.Contracts
{
    using System.Collections.Generic;

    public interface ILocationsService
    {
        T GetByName<T>(string name);

        ICollection<string> GetPictureUrls(int id);

        ICollection<string> GetPerks(int id);

        IEnumerable<T> GetAll<T>(int? take = null, int skip = 0);

        int GetLocationsCount();
    }
}
