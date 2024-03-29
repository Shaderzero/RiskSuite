﻿using AutoMapper;
using Business.Repositories.IRepository;
using LogSuite.Business;
using LogSuite.Business.Repositories;
using LogSuite.Business.Repositories.References;
using LogSuite.DataAccess;
using LogSuite.DataAccess.DailyReview;
using LogSuite.Shared;
using LogSuite.Shared.Models.DailyReview;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Repositories
{
    public class GisCountryRepository : IGisCountryRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GisCountryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<GisCountryDTO> Create(GisCountryDTO dto)
        {
            GisCountry toDb = _mapper.Map<GisCountryDTO, GisCountry>(dto);
            var result = await _db.GisCountries.AddAsync(toDb);
            await _db.SaveChangesAsync();
            return _mapper.Map<GisCountry, GisCountryDTO>(result.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var fromDb = await _db.GisCountries
                .Include(x => x.Values)
                .Include(x => x.Resources)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (fromDb != null)
            {
                _db.GisCountries.Remove(fromDb);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<GisCountryDTO> Get(int id)
        {
            var fromDb = await _db.GisCountries
                .Include(x => x.Country)
                .Include(x => x.Gis)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<GisCountry, GisCountryDTO>(fromDb);
            return dto;
        }

        public async Task<IEnumerable<GisCountryDTO>> GetAll()
        {
            var source = await _db.GisCountries
                    .Include(x => x.Country)
                    .Include(x => x.Gis)
                    .OrderBy(x => x.Country.Name)
                    .ToListAsync();
            var entities = _mapper.Map<IEnumerable<GisCountryDTO>>(source);
            return entities;
        }

        public async Task<IEnumerable<GisCountryDTO>> GetAllByCountryId(int countryId)
        {
            var source = await _db.GisCountries
                    .Include(x => x.Country)
                    .Include(x => x.Gis)
                    .OrderBy(x => x.Gis.Name)
                    .Where(x => x.CountryId == countryId)
                    .ToListAsync();
            var entities = _mapper.Map<IEnumerable<GisCountryDTO>>(source);
            return entities;
        }

        public async Task<IEnumerable<GisCountryDTO>> GetAllByGisId(int gisId)
        {
            var source = await _db.GisCountries
                    .Include(x => x.Country)
                    .Include(x => x.Gis)
                    .OrderBy(x => x.Country.Name)
                    .Where(x => x.GisId == gisId)
                    .ToListAsync();
            var entities = _mapper.Map<IEnumerable<GisCountryDTO>>(source);
            return entities;
        }

        public async Task<PagedList<GisCountryDTO>> GetAllPaged(Params parameters)
        {
            var source = _db.GisCountries
                    .Include(x => x.Country)
                    .Include(x => x.Gis)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<GisCountry>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<GisCountryDTO>>(result);
            return new PagedList<GisCountryDTO>(entities, result.MetaData);
        }

        public async Task<PagedList<GisCountryDTO>> GetAllPagedByCountryId(int countryId, Params parameters)
        {
            var source = _db.GisCountries
                    .Include(x => x.Country)
                    .Include(x => x.Gis)
                    .Where(x => x.CountryId == countryId)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<GisCountry>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<GisCountryDTO>>(result);
            return new PagedList<GisCountryDTO>(entities, result.MetaData);
        }

        public async Task<PagedList<GisCountryDTO>> GetAllPagedByGisId(int gisId, Params parameters)
        {
            var source = _db.GisCountries
                    .Include(x => x.Country)
                    .Include(x => x.Gis)
                    .Where(x => x.GisId == gisId)
                    .AsQueryable();
            source = source.Search(parameters.Filter);
            source = source.Sort(parameters.Order, parameters.OrderAsc);
            var result = await PagedList<GisCountry>.ToPagedListAsync(source, parameters.PageNumber, parameters.PageSize);
            var entities = _mapper.Map<List<GisCountryDTO>>(result);
            return new PagedList<GisCountryDTO>(entities, result.MetaData);
        }

        public async Task<GisCountryDTO> IsUnique(GisCountryDTO dto, int id = 0)
        {
            if (id == 0)
            {
                var fromDb = await _db.GisCountries
                    .Where(x => x.GisId == dto.GisId && x.CountryId == dto.CountryId)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisCountry, GisCountryDTO>(fromDb);
                return result;
            }
            else
            {
                var fromDb = await _db.GisCountries
                    .Where(x => x.GisId == dto.GisId && x.CountryId == dto.CountryId && x.Id != id)
                    .FirstOrDefaultAsync();
                var result = _mapper.Map<GisCountry, GisCountryDTO>(fromDb);
                return result;
            }
        }

        public async Task<GisCountryDTO> Update(GisCountryDTO dto)
        {
            var fromDb = await _db.GisCountries
                .FindAsync(dto.Id);
            var toUpdate = _mapper.Map(dto, fromDb);
            var updated = _db.GisCountries.Update(toUpdate);
            await _db.SaveChangesAsync();
            var result = _mapper.Map<GisCountry, GisCountryDTO>(updated.Entity);
            return result;
        }
    }
}