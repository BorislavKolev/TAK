namespace TAK.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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

        public async Task<int> CreateAsync(string title, string content, string userId, List<string> imageUrls, string latinTitle)
        {
            var newsPost = new NewsPost
            {
                Title = title,
                Content = content,
                UserId = userId,
                ImageUrl = imageUrls.First().Insert(54, "c_fill,h_500,w_500/"),
                LatinTitle = latinTitle,
            };

            await this.newsPostsRepository.AddAsync(newsPost);
            await this.newsPostsRepository.SaveChangesAsync();

            foreach (var url in imageUrls)
            {
                var newsPostPicture = new NewsPostPicture
                {
                    PictureUrl = url.Insert(54, "c_fill,h_960,w_1920/"),
                    NewsPostId = newsPost.Id,
                };

                await this.newsPostPicturesRepository.AddAsync(newsPostPicture);
                await this.newsPostPicturesRepository.SaveChangesAsync();
            }

            return newsPost.Id;
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

        public IEnumerable<T> GetLast<T>(int count)
        {
            var query = this.newsPostsRepository
               .All()
               .OrderByDescending(x => x.CreatedOn)
               .Take(count);

            return query.To<T>().ToList();
        }

        public T GetByName<T>(string name)
        {
            var newsPost = this.newsPostsRepository.All().Where(x => x.LatinTitle == name).To<T>().FirstOrDefault();

            return newsPost;
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
