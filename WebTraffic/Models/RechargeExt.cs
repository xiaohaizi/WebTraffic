﻿using System;
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
        public PageModel PageList(int pageIndex, int pageSize, string _url, out List<Recharge> _list, Dictionary<string, string> _dic = null)
        {
            PageModel pageItem = new PageModel();
            using (TrafficEntities modelDB = new TrafficEntities())
            {
                int allCount = modelDB.Task.Count();
                pageItem.PageIndex = pageIndex;
                pageItem.PageSize = pageSize;
                pageItem.PageCount = (int)Math.Ceiling((decimal)allCount / pageSize);
                _list = modelDB.Recharge.OrderByDescending(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                if (_dic != null)
                    pageItem.ParameterDic = _dic;

                pageItem.WebUrl = _url;
            }
            //  modelDB.
            return pageItem;
        }
    }
}