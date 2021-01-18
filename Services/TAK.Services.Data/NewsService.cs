namespace TAK.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using TAK.Data.Common.Repositories;
    using TAK.Data.Models;
    using TAK.Services.Data.Contracts;
    using TAK.Services.Mapping;

    public class NewsService : INewsService
    {
        private readonly IDeletableEntityRepository<NewsPost> newsPostsRepository;
        private readonly IDeletableEntityRepository<NewsPostPicture> newsPostPicturesRepository;

        public NewsService(IDeletableEntityRepository<NewsPost> newsPostsRepository, IDeletableEntityRepository<NewsPostPicture> newsPostPicturesRepository)
        {
            this.newsPostsRepository = newsPostsRepository;
            this.newsPostPicturesRepository = newsPostPicturesRepository;
        }

        public IEnumerable<T> GetAll<T>(int? take = null, int skip = 0)
        {
            var query = this.newsPostsRepository
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
            var location = this.newsPostsRepository.All().Where(x => x.LatinTitle == name).To<T>().FirstOrDefault();

            return location;
        }

        public ICollection<string> GetPictureUrls(int id)
        {
            var pictureUrls = this.newsPostPicturesRepository
               .All()
               .Where(x => x.NewsPostId == id)
               .Select(u => u.PictureUrl)
               .ToList();

            return pictureUrls;
        }

        public int GetNewsCount()
        {
            return this.newsPostsRepository.All().Count();
        }
    }
}
