using EPS.Data.SolrEntities;
using EPS.Utils.Service;
using SolrNet;
using SolrNet.Commands.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using EPS.Data.Entities;
using System.Dynamic;
using EPS.Utils.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;

namespace EPS.Service.Helpers
{
    public class SolrServices<T> where T : new()
    {
        private readonly ISolrOperations<T> _solr;

        public SolrServices(ISolrOperations<T> solr)
        {
            _solr = solr;
        }
        /// <summary>
        /// Tìm kiếm theo chuỗi query trực tiếp
        /// </summary>
        /// <param name="pagingParams">Tham số phân trang</param>
        /// <param name="searchString">Chuỗi query</param>
        /// <returns></returns>
        public PagingResult<T> FilterPaged(PagingParamsSolr pagingParams, string searchString)
        {
            QueryOptions options = new QueryOptions()
            {
                OrderBy = new List<SortOrder> { pagingParams.SortExpression },
                Rows = pagingParams.ItemsPerPage,
                Start = pagingParams.StartingIndex
            };
            var data = _solr.Query(searchString, options);
            var result = new PagingResult<T>()
            {
                PageSize = pagingParams.ItemsPerPage,
                CurrentPage = pagingParams.Page,
                TotalRows = data.NumFound,
                Data = data
            };
            return result;
        }
        public async Task<PagingResult<T>> FilterPagedAsync(PagingParamsSolr pagingParams, string searchString)
        {
            QueryOptions options = new QueryOptions()
            {
                OrderBy = new List<SortOrder> { pagingParams.SortExpression },
                Rows = pagingParams.ItemsPerPage,
                Start = pagingParams.StartingIndex
            };
            var data = await _solr.QueryAsync(searchString, options);
            var result = new PagingResult<T>()
            {
                PageSize = pagingParams.ItemsPerPage,
                CurrentPage = pagingParams.Page,
                TotalRows = data.NumFound,
                Data = data
            };
            return result;
        }
        public async Task<T> GetById(int id)
        {
            QueryOptions options = new QueryOptions()
            {

                Rows = 1

            };
            var data = await _solr.QueryAsync("((!VersionDraft:* ) OR VersionDraft:false) && T_ID:" + id, options);
            if (data.Count > 0)
                return data[0];
            return new T();
        }
        public async Task<T> GetByIdDanhMuc(int id)
        {
            QueryOptions options = new QueryOptions()
            {

                Rows = 1

            };
            var data = await _solr.QueryAsync("T_ID:" + id, options);
            if (data.Count > 0)
                return data[0];
            return new T();
        }
        public async Task<T> GetByVer(int id, int ver)
        {
            QueryOptions options = new QueryOptions()
            {

                Rows = 1

            };
            var data = await _solr.QueryAsync("lstVersion:,*" + ver + ",* && T_ID:" + id, options);
            if (data.Count > 0)
                return data[0];
            return new T();
        }

        /// <summary>
        /// Lấy danh sách dữ liệu ra với thông tin phân trang
        /// </summary>
        /// <param name="pagingParams">tham số phân trang</param>
        /// <param name="solrQueries">thông tin truy vấn</param>
        /// <returns></returns>
        public async Task<PagingResult<T>> FilterPagedAsync(PagingParamsSolr pagingParams, params ISolrQuery[] solrQueries)
        {
            QueryOptions options = new QueryOptions()
            {
                OrderBy = new List<SortOrder> { pagingParams.SortExpression },

                Start = pagingParams.StartingIndex
            };
            if (pagingParams.ItemsPerPage != -1)
                options.Rows = pagingParams.ItemsPerPage;
            if (pagingParams.FieldGets?.Count > 0)
                options.Fields = pagingParams.FieldGets;
            var data = await _solr.QueryAsync(new SolrMultipleCriteriaQuery(solrQueries, "AND"), options);

            var result = new PagingResult<T>()
            {
                PageSize = pagingParams.ItemsPerPage,
                CurrentPage = pagingParams.Page,
                TotalRows = data.NumFound,
                Data = data
            };
            return result;
        }
       
        public async Task<PagingResult<T>> FilterPagedAsync2(PagingParamsSolr pagingParams)
        {

            return await FilterPagedAsync(pagingParams, pagingParams.GetSolrQuery().ToArray());
        }
        public async Task<long> CountAsync2(PagingParamsSolr pagingParams)
        {
            return await CountAsync(pagingParams, pagingParams.GetSolrQuery().ToArray());
        }
        public async Task<long> CountAsync(PagingParamsSolr pagingParams, params ISolrQuery[] solrQueries)
        {
            QueryOptions options = new QueryOptions()
            {
                Fields = new List<string> { "id" },
                Rows = 0,
                Start = 0
            };
            var data = await _solr.QueryAsync(new SolrMultipleCriteriaQuery(solrQueries, "AND"), options);
            return data.NumFound;
        }
        public async Task<CountResult> CountAsyncAndSumFields(PagingParamsSolr pagingParams, params ISolrQuery[] solrQueries)
        {
            StatsParameters oStatsParameters = new StatsParameters();
            if (pagingParams.lstFieldsGetSum?.Count > 0)
                foreach (var item in pagingParams.lstFieldsGetSum)
                {
                    oStatsParameters.AddField(item);
                }

            QueryOptions options = new QueryOptions()
            {
                Fields = new List<string> { "id" },
                Rows = 0,
                Start = 0,
                Stats = oStatsParameters,
            };
            var data = await _solr.QueryAsync(new SolrMultipleCriteriaQuery(solrQueries, "AND"), options);
            var result = new CountResult()
            {
                NumFound = data.NumFound,

                SumFields = new Dictionary<string, StatsResult>() { }
            };

            StatsResult statsResult;
            if (pagingParams.lstFieldsGetSum?.Count > 0)
                foreach (var item in pagingParams.lstFieldsGetSum)
                {

                    if (data.Stats.TryGetValue(item, out statsResult))
                        result.SumFields.Add(item, statsResult);
                }

            return result;

        }
        public PagingResult<T> FilterPaged(PagingParamsSolr pagingParams, params ISolrQuery[] solrQueries)
        {
            QueryOptions options = new QueryOptions()
            {
                OrderBy = new List<SortOrder> { pagingParams.SortExpression },
                Rows = pagingParams.ItemsPerPage,
                Start = pagingParams.StartingIndex
            };
            var data = _solr.Query(new SolrMultipleCriteriaQuery(solrQueries, "AND"), options);

            var result = new PagingResult<T>()
            {
                PageSize = pagingParams.ItemsPerPage,
                CurrentPage = pagingParams.Page,
                TotalRows = data.NumFound,
                Data = data
            };
            return result;
        }
        public async Task AddItemAsync(T tSolrModel)
        {
            try
            {
                await _solr.AddAsync(tSolrModel);
                _solr.Commit();

            }
            catch (Exception exx)
            {
                throw exx;
            }


        }
        public async Task DeleteAsync(string idSolr)
        {
            if (idSolr.Contains("*"))
                return;
            await _solr.DeleteAsync(new SolrQuery("id:" + idSolr));
            _solr.Commit();


        }
        public async Task AddItemsAsync(List<T> tSolrModel)
        {
            try
            {
                await _solr.AddRangeAsync(tSolrModel);
                _solr.Commit();
            }
            catch (Exception exx)
            {
                throw exx;
            }

        }
        public void AddItem(T tSolrModel)
        {
            _solr.Add(tSolrModel);
            _solr.Commit();
        }
        
        private List<string> checkDictionary(Dictionary<string, string> d1, Dictionary<string, string> d2)
        {

            var lstreturn = d2.Where(entry => !d1.ContainsKey(entry.Key) && !string.IsNullOrEmpty(entry.Value)).Select(x => x.Key).ToList();
            lstreturn.AddRange(d1.Where(entry => !d2.ContainsKey(entry.Key) || d2[entry.Key] != entry.Value)
              .Select(x => x.Key));
            return lstreturn;
        }
        private List<string> checkDictionary(Dictionary<string, DateTime?> d1, Dictionary<string, DateTime?> d2)
        {

            var lstreturn = d2.Where(entry => !d1.ContainsKey(entry.Key) && entry.Value != null).Select(x => x.Key).ToList();
            lstreturn.AddRange(d1.Where(entry => !d2.ContainsKey(entry.Key) || d2[entry.Key] != entry.Value)
              .Select(x => x.Key));
            return lstreturn;
        }
        private List<string> checkDictionary(Dictionary<string, bool> d1, Dictionary<string, bool> d2)
        {

            var lstreturn = d2.Where(entry => !d1.ContainsKey(entry.Key) && entry.Value != null).Select(x => x.Key).ToList();
            lstreturn.AddRange(d1.Where(entry => !d2.ContainsKey(entry.Key) || d2[entry.Key] != entry.Value)
              .Select(x => x.Key));
            return lstreturn;
        }
        private List<string> checkDictionary(Dictionary<string, int?> d1, Dictionary<string, int?> d2)
        {

            var lstreturn = d2.Where(entry => !d1.ContainsKey(entry.Key) && entry.Value != null).Select(x => x.Key).ToList();
            lstreturn.AddRange(d1.Where(entry => !d2.ContainsKey(entry.Key) || d2[entry.Key] != entry.Value)
              .Select(x => x.Key));
            return lstreturn;
        }
        private List<string> checkDictionary(Dictionary<string, double?> d1, Dictionary<string, double?> d2)
        {

            var lstreturn = d2.Where(entry => !d1.ContainsKey(entry.Key) && entry.Value != null).Select(x => x.Key).ToList();
            lstreturn.AddRange(d1.Where(entry => !d2.ContainsKey(entry.Key) || d2[entry.Key] != entry.Value)
              .Select(x => x.Key));
            return lstreturn;
        }



    }
}
