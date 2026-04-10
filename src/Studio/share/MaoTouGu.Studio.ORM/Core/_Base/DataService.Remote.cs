// ----------------------------------------------------------
//            文件：DataService.Remote.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 14:45
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Diagnostics;

namespace MaoTouGu.Studio.Database.Core
{
    partial class DataService
    {
        private static readonly Lazy<IDataApiContract> _lazyApiValue = new Lazy<IDataApiContract>(Ioc.SafeGet<IDataApiContract>);

        /// <summary>
        /// 查询指定的文档。
        /// </summary>
        /// <param name="documentId">文档ID。</param>
        /// <returns></returns>
        protected internal async Task<BsonDocument> GetAsync(string documentId)
        {
            if (Api is null)
            {
                return null;
            }

            try
            {
                var r = await Api.GetAsync(DatabaseName, CollectionName, documentId);

                if (!r.IsFinished)
                {
                    return null;
                }

                return JsonSerializer.Deserialize(r.Value).AsDocument;
            }
            catch(Exception e)
            {
                Debug.WriteLine($"DataService Handling DataChanged  ->{e.Message}");
                return null;
            }
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        protected internal async Task<IEnumerable<BsonDocument>> GetCollectionAsync()
        {
            var api = Ioc.SafeGet<IDataApiContract>();

            if (api.IsOnline)
            {
                var r = await api.GetCollectionAsync(DatabaseName, CollectionName);

                if (!r.IsFinished)
                {
                    return Array.Empty<BsonDocument>();
                }

                return r.Value;
            }

            return Array.Empty<BsonDocument>();
        }

        
        
        public async Task _AddAsync(BsonDocument document)
        {
            if (Ioc.SafeGet<IDataApiContract>() is not {} api)
            {
                return;
            }
            
            var json = JsonSerializer.Serialize(document);
            await api.AddAsync(json, DatabaseName, CollectionName);
        }
        
        public async Task<bool> _DeleteAsync(string id)
        {
            if (Api is null)
            {
                return false;
            }

            try
            {
                var r = await Api.RemoveAsync(DatabaseName, CollectionName, id);
                return r.IsFinished;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task _UpdateAsync(BsonDocument document)
        {
            if (Api is null)
            {
                return;
            }
            
            var json = JsonSerializer.Serialize(document);
            await Api.UpdateAsync(json, DatabaseName, CollectionName);
        }

        internal IDataApiContract Api => _lazyApiValue.Value;
    }
}