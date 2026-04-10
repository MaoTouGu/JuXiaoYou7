namespace MaoTouGu.Studio.Database
{
    public static class DBHelper
    {
        public const string Field_ID          = "_id";
        public const string Field_Type        = "_type";
        public const string Field_Name        = "name";
        public const string Field_VariantName = "v";
        public const string Field_SourceID    = "sid";
        public const string Field_TargetID    = "tid";
        public const string Field_OwnerID     = "oid";
        public const string Field_MemberID    = "m2";

        public static bool Null<T>(T target) where T : class => target is null;
        public static bool NotNull<T>(T target) where T : class => target is not null;


        public static string GetDatabaseID(string kikakuId, string databaseName) => $"{kikakuId}::{databaseName}";
        public static string GetTableID(string kikakuId, string databaseName, string colName) => $"{kikakuId}::{databaseName}_{colName}";

        public static void Replace(this BsonDocument document, string oldKey, string newKey)
        {
            if (document.Remove(oldKey, out var value))
            {
                document.TryAdd(newKey, value);
            }
        }

        public static bool HasID<T>(this ILiteCollection<T> db, string id)
        {
            return db.Exists(Query.EQ(Field_ID, id));
        }


        public static bool HasName<T>(this ILiteCollection<T> db, string name)
        {
            return db.Exists(Query.EQ(Field_Name, name));
        }
    }
}