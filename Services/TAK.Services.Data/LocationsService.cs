namespace TAK.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using System.Threading.Tasks;

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

        public async Task<int> CreateAsync(string name, string description, string adress, string phoneNumber, string email, string website, string facebookPage, string instagramPage, string userId, string mapLink, string perks, string type, List<string> imageUrls, string latinName)
        {
            var location = new Location
            {
                Name = name,
                Description = description,
                Adress = adress,
                PhoneNumber = phoneNumber,
                Email = email,
                Website = website,
                FacebookPage = facebookPage,
                InstagramPage = instagramPage,
                UserId = userId,
                MapLink = mapLink,
                Perks = perks,
                Type = type,
                ImageUrl = imageUrls.First().Insert(54, "c_fill,h_800,w_600/"),
                LatinName = latinName,
            };

            await this.locationsRepository.AddAsync(location);
            await this.locationsRepository.SaveChangesAsync();

            foreach (var url in imageUrls)
            {
                var locationPicture = new LocationPicture
                {
                    PictureUrl = url.Insert(54, "c_fill,h_960,w_1920/"),
                    LocationId = location.Id,
                };

                await this.locationPicturesRepository.AddAsync(locationPicture);
                await this.locationPicturesRepository.SaveChangesAsync();
            }

            return location.Id;
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

        public IEnumerable<T> GetRandomLocations<T>(int count)
        {
            var query = this.locationsRepository
               .All()
               .OrderBy(c => Guid.NewGuid())
               .Take(count);

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            var location = this.locationsRepository.All().Where(x => x.LatinName == name).To<T>().FirstOrDefault();

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

        public ICollection<string> GetPerks(int id)
        {
            var perks = this.locationsRepository
                .All()
                .Where(x => x.Id == id)
                .Select(p => p.Perks)
                .SingleOrDefault()
                .Split(", ")
                .ToList();

            return perks;
        }

        public int GetLocationsCount()
        {
            return this.locationsRepository.All().Count();
        }
    }
}
