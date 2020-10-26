namespace TAK.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using TAK.Data.Common.Repositories;
    using TAK.Data.Models;
    using TAK.Services.Data.Contracts;
    using TAK.Services.Mapping;

    public class LocationsService : ILocationsService
    {
        private readonly IDeletableEntityRepository<Location> locationsRepository;
        private readonly IDeletableEntityRepository<LocationPicture> locationPicturesRepository;

        public LocationsService(IDeletableEntityRepository<Location> locationsRepository, IDeletableEntityRepository<LocationPicture> locationPicturesRepository)
        {
            this.locationsRepository = locationsRepository;
            this.locationPicturesRepository = locationPicturesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? take = null, int skip = 0)
        {
            var query = this.locationsRepository
               .All()
               .OrderByDescending(x => x.CreatedOn)
               .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            var location = this.locationsRepository.All().Where(x => x.Name == name).To<T>().FirstOrDefault();

            return location;
        }

        public ICollection<string> GetPictureUrls(int id)
        {
            var pictureUrls = this.locationPicturesRepository
               .All()
               .Where(x => x.LocationId == id)
               .Select(u => u.PictureUrl)
               .ToList();

            return pictureUrls;
        }

        public int GetLocationsCount()
        {
            return this.locationsRepository.All().Count();
        }
    }
}
