using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Basket.Domain;
using Newtonsoft.Json;

namespace Basket.Infrastructure
{
    public class BasketService
    {
        public Domain.Basket GetBasket(IList<BasketLineArticle> basketLineArticles)
        {
            // here your code implementation
            var list = new List<BasketLine>();
            foreach (var article in basketLineArticles)
            {
                var articleData = ImperativeProgramming.GetArticleDatabase(article.Id);
                list.Add(new BasketLine(new Article(articleData.Price, articleData.Category), article.Number));

            }

            return new Domain.Basket(list);
            
        }
        
        public ArticleDatabase GetArticleDatabase(string id)
        {
            // Retrive article from database
            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
            var assemblyDirectory = Path.GetDirectoryName(path);
            var jsonPath = Path.Combine(assemblyDirectory, "articledatabase.json");
            IList<ArticleDatabase> articleDatabases =
                JsonConvert.DeserializeObject<List<ArticleDatabase>>(File.ReadAllText(jsonPath));
            var article = articleDatabases.First(articleDatabase => articleDatabase.Id == id);

            return article;
        }
    }
}