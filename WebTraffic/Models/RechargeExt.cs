using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTraffic.Models
{
    public partial class Recharge
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="_dic"></param>
        /// <returns></returns>
        public PageModel PageList(int pageIndex, int pageSize, string _url, ref List<Recharge> _list, Dictionary<string, string> _dic = null)
        {
            PageModel pageItem = new PageModel();
            
                int allCount = _list.Count();
                pageItem.PageIndex = pageIndex;
                pageItem.PageSize = pageSize;
                pageItem.PageCount = (int)Math.Ceiling((decimal)allCount / pageSize);
                _list = _list.OrderByDescending(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                if (_dic != null)
                    pageItem.ParameterDic = _dic;

                pageItem.WebUrl = _url;
           
            //  modelDB.
            return pageItem;
        }
    }
}