using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTraffic.Models
{
    public partial class Task
    {
        // public  List< Task
        public PageModel PageList(int pageIndex, int pageSize, string  _url,ref List<Task> _taskList, Dictionary<string, string> _dic = null)
        {
            PageModel pageItem = new PageModel();
          
            using (TrafficEntities modelDB = new TrafficEntities())
            {
                int allCount = _taskList.Count();
                pageItem.PageIndex = pageIndex;
                pageItem.PageSize = pageSize;
                pageItem.PageCount = (int)Math.Ceiling((decimal)allCount / pageSize);
                _taskList = _taskList.OrderByDescending(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                if (_dic != null)
                    pageItem.ParameterDic = _dic;

                pageItem.WebUrl = _url;
            }
            //  modelDB.
            return pageItem;
        }
    }
}