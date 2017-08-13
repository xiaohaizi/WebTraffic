using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTraffic.Models
{
    public partial class Task
    {
        // public  List< Task
        public PageModel<Task> PageList(int pageIndex, int pageSize, Dictionary<string, string> _dic = null)
        {
            PageModel<Task> pageItem = new PageModel<Task>();
            using (TrafficEntities modelDB = new TrafficEntities())
            {
                int allCount = modelDB.Task.Count();
                pageItem.PageIndex = pageIndex;
                pageItem.PageSize = pageSize;
                pageItem.PageCount = (int)Math.Ceiling((decimal)allCount / pageSize);
                pageItem.listData = modelDB.Task.OrderByDescending(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                if (_dic != null)
                    pageItem.ParameterDic = _dic;
            }
            //  modelDB.
            return pageItem;
        }
    }
}