﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace MYHGTOOLS
{
    /// <summary>
    /// 创建人：刘欢
    /// 创建时间：2014年11月14日 10:13:32
    /// 说明：sql
    /// </summary>
    public class CreateSql
    {
        #region 公共操作

        /// <summary>
        /// 插入的sql语句
        /// </summary>
        public const string SqlInsert = "INSERT INTO {0}({1}) VALUES ({2}) ;";

        /// <summary>
        /// 修改的Sql语句
        /// </summary>
        public const string SqlUpdate = "UPDATE {0} SET {1} {2} ";

        /// <summary>
        /// 删除SQl语句
        /// </summary>
        public const string SqlDelete = "DELETE FROM {0} {1} ";

        /// <summary>
        /// 查找数据
        /// </summary>
        public const string SqlSelect = "SELECT {0} FROM {1} {2} ";

        /// <summary>
        /// 分页sql语句,说明{0}字段's {1}排序字段{2} 正序还是倒叙 DESC(倒叙) ASC(正序) {3}表名 {4}筛选条件 {5}开始条数 {6}结束条数
        /// </summary>
        public const string PageSel = "SELECT * FROM ( SELECT {0},ROW_NUMBER() OVER(ORDER BY {1} {2}) AS ByRank FROM {3} {4}) AS Temp WHERE ByRank BETWEEN {5} AND {6}";

        /// <summary>
        /// 总记录数{0} 表名 {1} 筛选条件
        /// </summary>
        public const string RecordCount = "SELECT COUNT(1) FROM {0} {1}";



        /// <summary>
        /// 分页sql语句,说明{0}字段's {1}排序字段{2} 正序还是倒叙 DESC(倒叙) ASC(正序) {3}表名 {4}筛选条件 {5}开始条数 {6}结束条数
        /// </summary>
        // public const string PageSelOld = "SELECT * FROM ( SELECT {0} FROM {3} {4} ORDER BY {1} {2}) AS Temp LIMIT {5},{6}";


        /// <summary>
        /// 分组{0}字段 {1} 表明 {2} 排序字段 {3}排序字段 {4}正序或是序号 {5}开始位置{6}页面大小
        /// </summary>
        public const string GroupSelect = "SELECT {0} FROM {1} GROUP BY {2} ORDER BY {3} {4} LIMIT {5},{6}";

        /// <summary>
        /// 计算总行数
        /// </summary>
        public const string tempSelect = "SELECT COUNT(1) FROM ({0}) Temp";


        #endregion


    }
}