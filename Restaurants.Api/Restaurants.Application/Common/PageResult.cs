
namespace Restaurants.Application.Common
{
   public class PageResult<T>
    {
        public PageResult(IEnumerable<T>items , int totalCount , int pageSize, int pageNumber )
        {
            Items = items;
            TotalItemsCount = totalCount;
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            ItemsFrom = (pageNumber - 1) * pageSize + 1; //how many items we have skipped and get the next round value
            ItemsTo = ItemsFrom + pageSize-1;
            //pS =5 , pN = 2
            //1*6 = itemsFrom = 6
            //6 + 5 - 1 = 10 itemsTo

        }
        public IEnumerable<T> Items { get; set; }
        public int TotalPages { get; set; }// back to the client
        public int TotalItemsCount { get; set; } //items Count
        public int ItemsFrom { get; set; } // Range
        public int ItemsTo { get; set; }

    }
}
