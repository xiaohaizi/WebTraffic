using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTraffic.Models
{
    public class PageModel<T>
    {
        public List<T> listData;
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public Dictionary<string, string> ParameterDic = new Dictionary<string, string>();


    }
}