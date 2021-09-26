using Core.DataAccess.Dapper.Interfaces;
using Core.DataAccess.Dapper.Models;
using Core.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Core.DataAccess.Dapper
{
    public class DapperQueryGenerator<T>: IDapperQueryGenerator<T> where T : IEntity
    {
        public string AddQueryGenerator(T model)
        {
            List<Tuple<string, string, object>> result = PropertyIdentifier(model);

            return new StringBuilder("INSERT INTO ")
                .Append(typeof(T).Name)
                .Append(" ( ")
                .Append(string.Join(",", result.Select(x => x.Item2)))
                .Append(" ) ")
                .Append("VALUES ")
                .Append("( ")
                .Append(string.Join(",", result.Select(x => $"@{x.Item2}")))
                .Append(") ").ToString();
        }

        public string SelectQueryGenerator(T model, PagingOrder pagingOrder = null)
        {
            List<Tuple<string, string, object>> result = PropertyIdentifier(model);

            var selectSb = new StringBuilder("SELECT * FROM ")
                .Append(typeof(T).Name);

            selectSb.Append(result.Any() ? " WHERE " : "");
            for (int i = 0; i < result.Count; i++)
            {
                var item = result[i];
                switch (item.Item1.ToLowerInvariant())
                {
                    case "int32":
                        selectSb.Append($"{item.Item2}={item.Item3} ");
                        break;
                    case "string":
                        selectSb.Append($"{item.Item2} LIKE {item.Item3} ");
                        break;
                    case "decimal":
                        selectSb.Append($"{item.Item2}={item.Item3} ");
                        break;
                }
                selectSb.Append(i != result.Count - 1 ? "OR " : "");
            }

            selectSb.Append(pagingOrder?.Order != null ? $"ORDER BY {pagingOrder.Order.SortingField} {pagingOrder.Order.SortingType.ToString()} " : "");
            selectSb.Append(pagingOrder?.Paging != null ? $"OFFSET {pagingOrder.Paging.PageOfset} ROWS FETCH NEXT {pagingOrder.Paging.PageSize} ROWS ONLY" : "");

            return selectSb.ToString();
        }

        public string SelectQueryGenerator(int Id)
        {
            var selectSb = new StringBuilder("SELECT * FROM ")
                .Append(typeof(T).Name)
                .Append(" WHERE Id=")
                .Append(Id);
            return selectSb.ToString();
        }

        public string CountQueryGenerator(T model)
        {
            List<Tuple<string, string, object>> result = PropertyIdentifier(model);

            var selectSb = new StringBuilder("SELECT COUNT(1) FROM ")
                .Append(typeof(T).Name);

            selectSb.Append(result.Any() ? " WHERE " : "");
            for (int i = 0; i < result.Count; i++)
            {
                var item = result[i];
                switch (item.Item1.ToLowerInvariant())
                {
                    case "int32":
                        selectSb.Append($"{item.Item2}={item.Item3} ");
                        break;
                    case "string":
                        selectSb.Append($"{item.Item2} LIKE {item.Item3} ");
                        break;
                    case "datetime":
                        var dateVal = DateTime.ParseExact(item.Item3.ToString(), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                        selectSb.Append($"{item.Item2} BETWEEN '{dateVal.ToString("yyyy-MM-dd 00:00:00.000")}' AND '{dateVal.ToString("yyyy-MM-dd 23:59:59.999")}' ");
                        break;
                }
                selectSb.Append(i != result.Count - 1 ? "OR " : "");
            }

            return selectSb.ToString();
        }

        #region Helper Methods
        private List<Tuple<string, string, object>> PropertyIdentifier(T model)
        {
            var result = new List<Tuple<string, string, object>>();

            foreach (var item in model.GetType().GetProperties())
            {
                var value = item.GetValue(model, null);

                if (value == null)
                    continue;
                if (typeof(int).IsAssignableFrom(item.PropertyType) && (int)value == 0)
                    continue;
                if (typeof(decimal).IsAssignableFrom(item.PropertyType) && (decimal)value == 0)
                    continue;
                if (typeof(DateTime).IsAssignableFrom(item.PropertyType) && (DateTime)value == new DateTime(0001, 1, 1))
                    continue;
                result.Add(new Tuple<string, string, object>(item.PropertyType.Name, item.Name, DataTypeDefiner(item.PropertyType.Name, value)));
            }

            return result;
        }

        private static object DataTypeDefiner(string type, object value)
        {
            switch (type.ToLowerInvariant())
            {
                case "int32":
                    return Convert.ToInt32(value);
                case "datetime":
                    return $"'{Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm")}'";
                case "string":
                    return $"'{value}'";
                case "decimal":
                    return Convert.ToDecimal(value);
                default:
                    throw new ValidationException("Unsupported primitive type");
            }
        }

        #endregion
    }
}
