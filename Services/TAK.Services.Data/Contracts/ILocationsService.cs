namespace TAK.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILocationsService
    {
        Task<int> CreateAsync(string name, string description, string adress, string phoneNumber, string email, string website, string facebookPage, string instagramPage, string userId, string mapLink, string perks, string type, List<string> imageUrls, string latinName);

        IEnumerable<T> GetAll<T>(int? take = null, int skip = 0);

        T GetByName<T>(string name);

        ICollection<string> GetPictureUrls(int id);

        ICollection<string> GetPerks(int id);

        int GetLocationsCount();

        IEnumerable<T> GetRandomLocations<T>(int count);

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id);

        Task<int> EditAsync(string name, string description, string adress, string phoneNumber, string email, string website, string facebookPage, string instagramPage, string userId, string mapLink, string perks, string type, List<string> imageUrls, string latinName, int id);

        Task DeleteByIdAsync(int id);
    }
}
