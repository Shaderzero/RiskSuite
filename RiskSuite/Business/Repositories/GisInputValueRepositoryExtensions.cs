﻿using LogSuite.DataAccess.DailyReview;
using System.Linq;

namespace LogSuite.Business.Repositories
{
    public static class GisInputValueRepositoryExtensions
    {
        public static IQueryable<GisInputValue> Search(this IQueryable<GisInputValue> source, string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
                return source;
            var f = filter.Trim().ToLower();
            var result =  source.Where(s => s.RequstedValue.ToString().Contains(f)
                        || s.AllocatedValue.ToString().Contains(f)
                        || s.EstimatedValue.ToString().Contains(f)
                        || s.FactValue.ToString().Contains(f)
                        );
            return result;
        }

        public static IQueryable<GisInputValue> Sort(this IQueryable<GisInputValue> source, string columnName, bool ascendind)
        {
            if (string.IsNullOrWhiteSpace(columnName))
            {
                source = source.OrderByDescending(s => s.DateReport);
            } 
            else
            {
                if (ascendind)
                {
                    switch (columnName)
                    {
                        case "Id":
                            source = source.OrderBy(s => s.Id);
                            break;
                        case "DateReport":
                            source = source.OrderBy(s => s.DateReport);
                            break;
                        case "RequstedValue":
                            source = source.OrderBy(s => s.RequstedValue);
                            break;
                        case "AllocatedValue":
                            source = source.OrderBy(s => s.AllocatedValue);
                            break;
                        case "EstimatedValue":
                            source = source.OrderBy(s => s.EstimatedValue);
                            break;
                        case "FactValue":
                            source = source.OrderBy(s => s.FactValue);
                            break;
                        default:
                            source = source.OrderBy(s => s.DateReport);
                            break;
                    }
                }
                else
                {
                    switch (columnName)
                    {
                        case "Id":
                            source = source.OrderByDescending(s => s.Id);
                            break;
                        case "DateReport":
                            source = source.OrderByDescending(s => s.DateReport);
                            break;
                        case "RequstedValue":
                            source = source.OrderByDescending(s => s.RequstedValue);
                            break;
                        case "AllocatedValue":
                            source = source.OrderByDescending(s => s.AllocatedValue);
                            break;
                        case "EstimatedValue":
                            source = source.OrderByDescending(s => s.EstimatedValue);
                            break;
                        case "FactValue":
                            source = source.OrderByDescending(s => s.FactValue);
                            break;
                        default:
                            source = source.OrderByDescending(s => s.DateReport);
                            break;
                    }
                }
            }
            return source;
        }
    }
}
