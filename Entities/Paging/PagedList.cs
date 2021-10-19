
using BlazorTemplate.Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorTemplate.Entities.Paging
{
    public class PagingResponse<T> where T : class
    {
        public List<T> Items { get; set; }
        public MetaData MetaData { get; set; }

  

        public static PagingResponse<T> getPaginatedResponse(List<T> items, int pageNumber, int pageSize)
        {
            return new PagingResponse<T>
            {
                Items = items,
                MetaData = new MetaData
                {
                    TotalCount = items.Count,
                    PageSize = pageSize,
                    CurrentPage = pageNumber,
                    TotalPages = (int)Math.Ceiling(items.Count / (double)pageSize)
                }
            };
        }
    }
    /*   public class PagedList<T> : List<T>
       {
           public MetaData MetaData { get; set; }

           public PagedList(List<T> items, int count, int pageNumber, int pageSize)
           {
               MetaData = new MetaData
               {
                   TotalCount = count,
                   PageSize = pageSize,
                   CurrentPage = pageNumber,
                   TotalPages = (int)Math.Ceiling(count / (double)pageSize)
               };

               AddRange(items);
           }

           public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
           {
               var count = source.Count();
               var items = source
                   .Skip((pageNumber - 1) * pageSize)
                   .Take(pageSize).ToList();

               return new PagedList<T>(items, count, pageNumber, pageSize);
           }
       }
   */
}
