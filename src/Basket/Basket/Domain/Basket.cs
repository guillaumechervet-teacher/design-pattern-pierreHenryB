using System.Collections.Generic;

namespace Basket.Domain
{
    public class Basket
    {
        private readonly IList<BasketLine> _basketLine;

        public Basket(IList<BasketLine> basketLine)
        {
            _basketLine = basketLine;
        }

        public int Calculate()
        {
            int total = 0;
            foreach (var line in _basketLine)
            {
                total += line.Calculate();
            }

            return total;
        }
    }

    public class BasketLine
    {
        private readonly Article _article;
        private readonly int _numberArticle;

        public BasketLine(Article article, int numberArticle)
        {
            _article = article;
            _numberArticle = numberArticle;
        }

        public int Calculate()
        {
            return _article.Calculate() * _numberArticle;
        }
    }

    public class Article
    {
        private readonly int _price;
        private readonly string _category;

        public Article(int price, string category)
        {
            _price = price;
            _category = category;
        }

        public int Calculate()
        {
            switch (_category)
            {
                case "food":
                    return _price * 100 + _price * 12;
                case "electronic":
                    return _price * 100 + _price * 20 + 4;
                case "desktop":
                    return _price * 100 + _price * 20;
            }
        }
}