using AutoMapper;
using AutoMapper.QueryableExtensions;
using EPS.Data;
using EPS.Utils.Common;
using EPS.Utils.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using EPS.Data.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using EPS.Utils.Repository.Audit;

namespace EPS.Service.Helpers
{
    public class EPSBaseService : BaseService
    {
        public EPSBaseService(EPSRepository repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public override PagingResult<TDto> FilterPaged<TEntity, TDto>(PagingParams<TDto> pagingParams, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            if (pagingParams == null)
            {
                throw new ArgumentNullException("pagingParams");
            }

            var result = new PagingResult<TDto>() { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };

            IQueryable<TEntity> entityQuery = _repository.Filter<TEntity>();

            //if (pagingParams is IUnitTraversal<TEntity>)
            //{
            //    (pagingParams as IUnitTraversal<TEntity>).Traversing(_repository.All<UnitAncestor>(), ref entityQuery);
            //}

            IQueryable<TDto> query = entityQuery.ProjectTo<TDto>(_mapper.ConfigurationProvider);

            var pagingPredicates = pagingParams.GetPredicates();
            if (pagingPredicates != null && pagingPredicates.Any())
            {
                query = query.WhereMany(pagingPredicates);
            }

            if (predicates != null && predicates.Any())
            {
                query = query.WhereMany(predicates);
            }

            result.TotalRows = query.Count();

            // Ordering
            if (pagingParams.SortExpression != null)
            {
                query = query.OrderBy(pagingParams.SortExpression);

                if (pagingParams.Start > 0)
                {
                    query = query.Skip(pagingParams.Start);
                }
                // Skipping only work after ordering
                else if (pagingParams.StartingIndex > 0)
                {
                    query = query.Skip(pagingParams.StartingIndex);
                }
            }

            if (pagingParams.ItemsPerPage != -1 && pagingParams.ItemsPerPage <= 0)
            {
                pagingParams.ItemsPerPage = 100;
            }

            if (pagingParams.ItemsPerPage > 0)
            {
                query = query.Take(pagingParams.ItemsPerPage);
            }

            result.Data = query.ToList();

            return result;
        }

        public override PagingResult<TDto> FilterPaged<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, PagingParams<TDto> pagingParams, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            if (pagingParams == null)
            {
                throw new ArgumentNullException("pagingParams");
            }

            var result = new PagingResult<TDto>() { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };

            IQueryable<TEntity> entityQuery = _repository.Filter<TEntity>();

            //if (pagingParams is IUnitTraversal<TEntity>)
            //{
            //    (pagingParams as IUnitTraversal<TEntity>).Traversing(_repository.All<UnitAncestor>(), ref entityQuery);
            //}

            IQueryable<TDto> query = entityQuery.Select(mapping);

            var pagingPredicates = pagingParams.GetPredicates();
            if (pagingPredicates != null && pagingPredicates.Any())
            {
                query = query.WhereMany(pagingPredicates);
            }

            if (predicates != null && predicates.Any())
            {
                query = query.WhereMany(predicates);
            }

            result.TotalRows = query.Count();

            // Ordering
            if (pagingParams.SortExpression != null)
            {
                if (pagingParams.SortBy == "NEWID")
                {
                    query = query.OrderBy(x => Guid.NewGuid());
                }
                else
                {
                    query = query.OrderBy(pagingParams.SortExpression);
                }

                // Skipping only work after ordering
                if (pagingParams.Start > 0)
                {
                    query = query.Skip(pagingParams.Start);
                }
                // Skipping only work after ordering
                else if (pagingParams.StartingIndex > 0)
                {
                    query = query.Skip(pagingParams.StartingIndex);
                }
            }

            if (pagingParams.ItemsPerPage != -1 && pagingParams.ItemsPerPage <= 0)
            {
                pagingParams.ItemsPerPage = 100;
            }

            if (pagingParams.ItemsPerPage > 0)
            {
                query = query.Take(pagingParams.ItemsPerPage);
            }

            result.Data = query.ToList();

            return result;
        }

        public async override Task<PagingResult<TDto>> FilterPagedAsync<TEntity, TDto>(PagingParams<TDto> pagingParams, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            if (pagingParams == null)
            {
                throw new ArgumentNullException("pagingParams");
            }

            var result = new PagingResult<TDto>() { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };

            IQueryable<TEntity> entityQuery = _repository.Filter<TEntity>();

            //if (pagingParams is IUnitTraversal<TEntity>)
            //{
            //    (pagingParams as IUnitTraversal<TEntity>).Traversing(_repository.All<UnitAncestor>(), ref entityQuery);
            //}

            IQueryable<TDto> query = entityQuery.ProjectTo<TDto>(_mapper.ConfigurationProvider);

            var pagingPredicates = pagingParams.GetPredicates();
            if (pagingPredicates != null && pagingPredicates.Any())
            {
                query = query.WhereMany(pagingPredicates);
            }

            if (predicates != null && predicates.Any())
            {
                query = query.WhereMany(predicates);
            }

            result.TotalRows = await query.CountAsync();
            // Ordering
            if (pagingParams.SortExpression != null)
            {
                if (pagingParams.SortBy == "NEWID")
                {
                    query = query.OrderBy(x => Guid.NewGuid());
                }
                else
                {
                    query = query.OrderBy(pagingParams.SortExpression);
                }
                if (pagingParams.Start > 0)
                {
                    query = query.Skip(pagingParams.Start);
                }
                // Skipping only work after ordering
                else if (pagingParams.StartingIndex > 0)
                {
                    query = query.Skip(pagingParams.StartingIndex);
                }
            }

            if (pagingParams.ItemsPerPage != -1 && pagingParams.ItemsPerPage <= 0)
            {
                pagingParams.ItemsPerPage = 100;
            }

            if (pagingParams.ItemsPerPage > 0)
            {
                query = query.Take(pagingParams.ItemsPerPage);
            }
            result.Data = await query.ToListAsync();

            return result;
        }
        public async Task<List<TDto>> GetDataAsync<TEntity, TDto>(PagingParams<TDto> pagingParams, params Expression<Func<TDto, bool>>[] predicates)
           where TEntity : class
        {
            if (pagingParams == null)
            {
                throw new ArgumentNullException("pagingParams");
            }



            IQueryable<TEntity> entityQuery = _repository.Filter<TEntity>();

            //if (pagingParams is IUnitTraversal<TEntity>)
            //{
            //    (pagingParams as IUnitTraversal<TEntity>).Traversing(_repository.All<UnitAncestor>(), ref entityQuery);
            //}

            IQueryable<TDto> query = entityQuery.ProjectTo<TDto>(_mapper.ConfigurationProvider);

            var pagingPredicates = pagingParams.GetPredicates();
            if (pagingPredicates != null && pagingPredicates.Any())
            {
                query = query.WhereMany(pagingPredicates);
            }

            if (predicates != null && predicates.Any())
            {
                query = query.WhereMany(predicates);
            }


            // Ordering
            if (pagingParams.SortExpression != null)
            {
                if (pagingParams.SortBy == "NEWID")
                {
                    query = query.OrderBy(x => Guid.NewGuid());
                }
                else
                {
                    query = query.OrderBy(pagingParams.SortExpression);
                }
                if (pagingParams.Start > 0)
                {
                    query = query.Skip(pagingParams.Start);
                }
                // Skipping only work after ordering
                else if (pagingParams.StartingIndex > 0)
                {
                    query = query.Skip(pagingParams.StartingIndex);
                }
            }

            if (pagingParams.ItemsPerPage != -1 && pagingParams.ItemsPerPage <= 0)
            {
                pagingParams.ItemsPerPage = 100;
            }

            if (pagingParams.ItemsPerPage > 0)
            {
                query = query.Take(pagingParams.ItemsPerPage);
            }
            return await query.ToListAsync();


        }
        public async override Task<PagingResult<TDto>> FilterPagedAsync<TEntity, TDto>(Expression<Func<TEntity, TDto>> mapping, PagingParams<TDto> pagingParams, params Expression<Func<TDto, bool>>[] predicates)
            where TEntity : class
        {
            if (pagingParams == null)
            {
                throw new ArgumentNullException("pagingParams");
            }

            var result = new PagingResult<TDto>() { PageSize = pagingParams.ItemsPerPage, CurrentPage = pagingParams.Page };

            IQueryable<TEntity> entityQuery = _repository.Filter<TEntity>();

            //if (pagingParams is IUnitTraversal<TEntity>)
            //{
            //    (pagingParams as IUnitTraversal<TEntity>).Traversing(_repository.All<UnitAncestor>(), ref entityQuery);
            //}

            IQueryable<TDto> query = entityQuery.Select(mapping);

            var pagingPredicates = pagingParams.GetPredicates();
            if (pagingPredicates != null && pagingPredicates.Any())
            {
                query = query.WhereMany(pagingPredicates);
            }

            if (predicates != null && predicates.Any())
            {
                query = query.WhereMany(predicates);
            }

            result.TotalRows = await query.CountAsync();

            // Ordering
            if (pagingParams.SortExpression != null)
            {
                query = query.OrderBy(pagingParams.SortExpression);

                if (pagingParams.Start > 0)
                {
                    query = query.Skip(pagingParams.Start);
                }
                // Skipping only work after ordering
                else if (pagingParams.StartingIndex > 0)
                {
                    query = query.Skip(pagingParams.StartingIndex);
                }
            }

            if (pagingParams.ItemsPerPage != -1 && pagingParams.ItemsPerPage <= 0)
            {
                pagingParams.ItemsPerPage = 100;
            }

            if (pagingParams.ItemsPerPage > 0)
            {
                query = query.Take(pagingParams.ItemsPerPage);
            }

            result.Data = await query.ToListAsync();

            return result;
        }
    }
}

