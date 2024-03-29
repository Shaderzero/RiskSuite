﻿using LogSuite.Business;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Repositories.IRepository
{
    public interface IGisOutputValueRepository
    {
        Task<GisOutputValueDTO> Create(GisOutputValueDTO dto);
        Task<GisOutputValueDTO> Update(GisOutputValueDTO dto);
        Task<GisOutputValueDTO> Get(int id);
        Task<int> Delete(int id);
        Task<GisOutputValueDTO> IsUnique(GisOutputValueDTO dto, int id = 0);
        Task<PagedList<GisOutputValueDTO>> GetPagedByGisId(int gisId, Params parameters);
        Task<GisOutputValueDTO> GetOnDateByGisId(int gisId, DateTime date);
        Task<List<GisOutputValueDTO>> GetOnDateRangeByGisId(int gisId, DateTime dateStart, DateTime dateEnd);
    }
}
