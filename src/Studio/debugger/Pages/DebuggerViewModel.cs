// ----------------------------------------------------------
//            文件：DebuggerViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月09日 23:15
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Collections;
using LiteDB;
using MaoTouGu.JuXiaoYou.Core;
using MaoTouGu.Studio.Database;
using MaoTouGu.Studio.Database.Core;

namespace MaoTouGu.JuXiaoYou.Pages
{
    public class DebuggerViewModel : InstancePage
    {
        private DebuggerDocument _document;

        private static readonly Lazy<IDataApiContract> _lazyApiValue = new Lazy<IDataApiContract>(Ioc.SafeGet<IDataApiContract>);

        public DebuggerViewModel(string databaseName, string collectionName)
        {
            InstanceID     = $"{databaseName} + {collectionName}";
            Title          = $"调试{collectionName}表";
            DatabaseName   = databaseName;
            CollectionName = collectionName;

            Properties = new ViewList<DebuggerProperty>();
            Documents  = new ViewList<DebuggerDocument>();
        }

        protected override async void OnStart()
        {
            var dbMgr = Ioc.Get<IDatabaseManager>();
            var db    = dbMgr.GetDatabase(DatabaseName);
            var col   = db.GetCollection(CollectionName);

            Database = db;
            DbSet    = col;

            if (Api.IsOnline)
            {
                var r = await Api.GetCollectionAsync(DatabaseName, CollectionName);

                if (r.IsFinished)
                {
                    DbSet.DeleteAll();
                    DbSet.Insert(r.Value);
                }
            }


            foreach (var document in DbSet.FindAll())
            {
                var id   = document.TryGetValue(DBHelper.Field_ID, out var idRawValue) ? idRawValue.AsString : string.Empty;
                var type = document.TryGetValue(DBHelper.Field_Type, out var typeRawValue) ? DefaultTypeNameBinder.Instance.GetType(typeRawValue.AsString) : null;
                var name = document.TryGetValue(DBHelper.Field_Name, out var nameRawValue) ? nameRawValue.AsString : string.Empty;

                var dd = new DebuggerDocument
                {
                    Id       = id,
                    Name     = name,
                    Type     = type,
                    TypeName = type?.Name,
                    Document = document,
                };

                Documents.Add(dd);
            }

            Document = Documents.FirstOrDefault();
        }

        void GetDocumentProperties(BsonDocument document)
        {
            foreach (var (k, v) in document)
            {
                DebuggerProperty dp;

                if (v.IsDocument)
                {
                    dp = GetDocumentProperties(v.AsDocument, new NestedDebuggerProperty
                    {
                        Raw        = v,
                        Key        = k,
                        Properties = new ViewList<DebuggerProperty>(),
                    });
                }
                else if (v.IsArray)
                {
                    dp = GetDocumentProperties(v.AsArray, new NestedDebuggerProperty
                    {
                        Raw        = v,
                        Key        = k,
                        Properties = new ViewList<DebuggerProperty>(),
                    });
                }
                else
                {
                    dp = new StringDebuggerProperty
                    {
                        Raw   = v,
                        Key   = k,
                        Value = v.RawValue.ToString(),
                    };
                }

                Properties.Add(dp);
            }
        }

        static NestedDebuggerProperty GetDocumentProperties(BsonDocument document, NestedDebuggerProperty property)
        {
            foreach (var (k, v) in document)
            {
                DebuggerProperty dp;

                if (v.IsDocument)
                {
                    dp = GetDocumentProperties(v.AsDocument, new NestedDebuggerProperty
                    {
                        Raw = v,
                        Key = k,
                        Properties = new ViewList<DebuggerProperty>(),
                    });
                }
                else if (v.IsArray)
                {
                    dp = GetDocumentProperties(v.AsArray, new NestedDebuggerProperty
                    {
                        Raw = v,
                        Key = k,
                        Properties = new ViewList<DebuggerProperty>(),
                    });
                }
                else
                {
                    dp = new StringDebuggerProperty
                    {
                        Raw = v,
                        Key = k,
                        Value = v.RawValue.ToString(),
                    };
                }

                property.Properties.Add(dp);
            }


            return property;
        }
        
        static NestedDebuggerProperty GetDocumentProperties(BsonArray document, NestedDebuggerProperty property)
        {
            var i = 0;
            foreach (var v in document)
            {
                DebuggerProperty dp;
                
                if (v.IsDocument)
                {
                    dp = GetDocumentProperties(v.AsDocument, new NestedDebuggerProperty
                    {
                        Raw        = v,
                        Key        = $"[{i++}]" ,
                        Properties = new ViewList<DebuggerProperty>(),
                    });
                }
                else
                {
                    dp = new StringDebuggerProperty
                    {
                        Raw   = v,
                        Key = $"[{i++}]",
                        Value = v.RawValue.ToString(),
                    };
                }

                property.Properties.Add(dp);
            }

            return property;
        }

        internal IDataApiContract Api => _lazyApiValue.Value;

        public DebuggerDocument Document
        {
            get => _document;
            set
            {
                SetValue(ref _document, value);

                Properties.Clear();

                if (_document is not null)
                {
                    GetDocumentProperties(_document.Document);
                }
            }
        }

        public LiteDatabase                  Database { get; private set; }
        public ILiteCollection<BsonDocument> DbSet    { get; private set; }

        public ViewList<DebuggerProperty> Properties { get; }
        public ViewList<DebuggerDocument> Documents  { get; }

        public string DatabaseName   { get; }
        public string CollectionName { get; }
    }

    public abstract class DebuggerProperty
    {
        public BsonValue Raw { get; init; }
    }

    public sealed class StringDebuggerProperty : DebuggerProperty
    {
        public string Key   { get; init; }
        public string Value { get; init; }
    }

    public sealed class NestedDebuggerProperty : DebuggerProperty
    {
        public string Key { get; init; }

        public ViewList<DebuggerProperty> Properties { get; init; }
    }

    public sealed class DebuggerDocument
    {
        public Type   Type { get; init; }
        public string Id   { get; init; }
        public string Name { get; init; }

        public string       TypeName { get; init; }
        public BsonDocument Document { get; init; }
    }
}