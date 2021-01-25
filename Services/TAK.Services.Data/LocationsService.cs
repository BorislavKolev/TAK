namespace TAK.Services.Data
{
    using Microsoft.EntityFrameworkCore;
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

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id)
        {
            var locationViewModel = await this.locationsRepository
                .All()
                .Where(l => l.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

            return locationViewModel;
        }

        public async Task<int> EditAsync(string name, string description, string adress, string phoneNumber, string email, string website, string facebookPage, string instagramPage, string userId, string mapLink, string perks, string type, List<string> imageUrls, string latinName, int id)
        {
            var location = await this.locationsRepository
               .All()
               .FirstOrDefaultAsync(l => l.Id == id);

            if (imageUrls.Count > 0)
            {
                location.ImageUrl = imageUrls.First().Insert(54, "c_fill,h_800,w_600/");
                var locationPictures = await this.locationPicturesRepository
              .All()
              .Where(m => m.LocationId == id)
              .ToListAsync();

                foreach (var locPic in locationPictures)
                {
                    locPic.IsDeleted = true;
                    locPic.DeletedOn = DateTime.UtcNow;
                    this.locationPicturesRepository.Update(locPic);
                }

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
            }

            location.Name = name;
            location.Description = description;
            location.Adress = adress;
            location.PhoneNumber = phoneNumber;
            location.Email = email;
            location.Website = website;
            location.FacebookPage = facebookPage;
            location.InstagramPage = instagramPage;
            location.UserId = userId;
            location.MapLink = mapLink;
            location.Perks = perks;
            location.Type = type;
            location.LatinName = latinName;

            await this.locationsRepository.SaveChangesAsync();

            return location.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var location = await this.locationsRepository.All().FirstOrDefaultAsync(l => l.Id == id);

            location.IsDeleted = true;
            location.DeletedOn = DateTime.UtcNow;
            this.locationsRepository.Update(location);
            await this.locationsRepository.SaveChangesAsync();

            var locationPictures = await this.locationPicturesRepository
                .All()
                .Where(m => m.LocationId == id)
                .ToListAsync();

            foreach (var locationPicture in locationPictures)
            {
                locationPicture.IsDeleted = true;
                locationPicture.DeletedOn = DateTime.UtcNow;
                this.locationPicturesRepository.Update(locationPicture);
            }

            await this.locationPicturesRepository.SaveChangesAsync();
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
