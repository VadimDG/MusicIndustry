using AutoMapper;
using boilerplate.api.common.Helpers;
using boilerplate.api.common.Models;
using boilerplate.api.core.Models;
using boilerplate.api.data.Models;
using boilerplate.api.data.Procedures;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace boilerplate.api.data.Stores
{
    public abstract class BaseStore : IBaseStore
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;

        protected abstract Type DataModelType { get; }
        protected abstract string EntriesProcedureName { get; }
        protected abstract string EntryProcedureName { get; }

        public BaseStore(ConnectionStrings connectionStrings, ApplicationDbContext context, IMapper mapper)
        {
            _connectionStrings = connectionStrings ?? ThrowHelper.NullArgument<ConnectionStrings>();
            _context = context ?? ThrowHelper.NullArgument<ApplicationDbContext>();
            _mapper = mapper ?? ThrowHelper.NullArgument<IMapper>();
        }

        public virtual async Task<EntriesQueryResponse<T>> GetEntries<T>(EntriesQueryRequest request)
        {
            using (var connection = new SqlConnection(_connectionStrings.DefaultConnection))
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add(ProcedureParams.AllowDirtyRead, true, DbType.Boolean);
                parameter.Add(ProcedureParams.Offset, request.Offset, DbType.Int32);
                parameter.Add(ProcedureParams.Limit, request.Limit, DbType.Int32);

                using (var multi = await connection.QueryMultipleAsync(EntriesProcedureName, param: parameter, commandType: CommandType.StoredProcedure).ConfigureAwait(false))
                {
                    return new EntriesQueryResponse<T>
                    {
                        Data = multi.Read<T>().ToList(),
                        TotalCount = multi.Read<int>().FirstOrDefault(),
                        Success = true,
                        Code = ResponseCode.Success
                    };
                }
            }
        }

        public virtual async Task<EntriesQueryResponse<T>> GetEntries<T>(string sqlRequest)
        {
            using (var connection = new SqlConnection(_connectionStrings.DefaultConnection))
            {
                var res = await connection.QueryAsync<T>(sqlRequest, commandType: CommandType.Text).ConfigureAwait(false);
                return new EntriesQueryResponse<T>
                {
                    Data = res.ToList(),
                    TotalCount = res.Count(),
                    Success = true,
                    Code = ResponseCode.Success
                };
            }
        }

        public virtual async Task<EntryQueryResponse<T>> GetEntry<T, K>(EntryQueryRequest<K> request)
        {
            using (var connection = new SqlConnection(_connectionStrings.DefaultConnection))
            {
                DynamicParameters parameter = new DynamicParameters();
                var idType = typeof(K) == typeof(int) ? DbType.Int32 : DbType.Guid;
                parameter.Add(ProcedureParams.Id, request.Id, idType);
                parameter.Add(ProcedureParams.AllowDirtyRead, false, DbType.Boolean);

                using (var multi = await connection.QueryMultipleAsync(EntryProcedureName, param: parameter, commandType: CommandType.StoredProcedure).ConfigureAwait(false))
                {
                    return new EntryQueryResponse<T>
                    {
                        Data = multi.Read<T>().FirstOrDefault(),
                        Success = true,
                        Code = ResponseCode.Success
                    };
                }
            }
        }

        public virtual async Task<EntryQueryResponse<T>> GetEntry<T>(string sqlRequest)
        {
            using (var connection = new SqlConnection(_connectionStrings.DefaultConnection))
            {
                var res = await connection.QueryAsync<T>(sqlRequest, commandType: CommandType.Text);
                return new EntryQueryResponse<T>
                {
                    Data = res.FirstOrDefault(),
                    Success = true,
                    Code = ResponseCode.Success
                };
            }
        }

        public virtual async Task<int> ExecuteAsync(string sqlRequest)
        {
            using (var connection = new SqlConnection(_connectionStrings.DefaultConnection))
            {
                return await connection.ExecuteAsync(sqlRequest, commandType: CommandType.Text);
            }
        }

        public virtual async Task<K> CreateEntry<T, K>(CreateCommandRequest<T> request)
        {
            var mappedEntry = MapCreateModel<T, K>(request);
            _context.Add(mappedEntry);
            await _context.SaveChangesAsync();
            
            return mappedEntry.Id;
        }

        public virtual async Task<bool> UpdateEntry<T, K>(UpdateCommandRequest<T> request)
        {
            var mappedEntry = MapUpdateModel<T, K>(request);
            var existingEntry = await _context.FindAsync(DataModelType, mappedEntry.Id).ConfigureAwait(false);
            if (existingEntry == null)
            {
                return false;
            }
            mappedEntry.DateCreated = ((IBaseEntryModel<K>)existingEntry).DateCreated;
            _context.Entry(existingEntry).CurrentValues.SetValues(mappedEntry);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public virtual async Task<bool> DeleteEntry<T>(DeleteCommandRequest<T> request)
        {
            var existingEntry = await _context.FindAsync(DataModelType, request.Id).ConfigureAwait(false);
            if (existingEntry == null)
            {
                return false;
            }
            _context.Remove(existingEntry);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        private IBaseEntryModel<K> MapCreateModel<T, K>(CreateCommandRequest<T> request)
        {
            return (IBaseEntryModel<K>)_mapper.Map(request.Entry, request.Entry.GetType(), DataModelType);
        }

        private IBaseEntryModel<K> MapUpdateModel<T, K>(UpdateCommandRequest<T> request)
        {
            return (IBaseEntryModel<K>)_mapper.Map(request.Entry, request.Entry.GetType(), DataModelType);
        }
    }
}
