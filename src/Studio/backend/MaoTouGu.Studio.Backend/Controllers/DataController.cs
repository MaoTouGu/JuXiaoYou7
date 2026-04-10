using System.Security.Claims;
using System.Text.RegularExpressions;
using LiteDB;
using MaoTouGu.Foundation;
using MaoTouGu.Studio.Database;
using MaoTouGu.Studio.Database.Core;
using MaoTouGu.Studio.Database.Spots;
using MaoTouGu.Studio.Database.Utils;
using JsonSerializer = LiteDB.JsonSerializer;

namespace MaoTouGu.Studio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class DataController(IDatabaseService _Manager, IUserService _usrSrv, IHubContext<PushingHub> _Hub) : Controller
    {
        private const string NotSelectRegex = "DROP|BEGIN|CHECKPOINT|COMMIT|CREATE|UPDATE|DELETE|INSERT|REBUILD|ROLLBACK|RENAME|PRAGMA|PARSELISTS";

        async Task HandleAddingJob(DatabaseTask task, ILiteCollection<BsonDocument> collection)
        {
            var document = JsonSerializer.Deserialize(task.Payload).AsDocument;

            //
            //
            collection.Insert(document);

            await _Hub.Clients
                      .All
                      .SendAsync(nameof(ISpotRecipient.WhenDataChanged), new DataChangedSpot
                       {
                           DocumentID = document["_id"].AsString,
                           EventID    = $"{task.DatabaseName}.{task.CollectionName}",
                           HandlerID  = task.UserID,
                           Operation  = task.Operation,
                       });
        }

        async Task HandleUpdatingJob(DatabaseTask task, ILiteCollection<BsonDocument> collection)
        {
            var document = JsonSerializer.Deserialize(task.Payload).AsDocument;

            //
            //
            collection.Update(document);

            await _Hub.Clients
                      .All
                      .SendAsync(nameof(ISpotRecipient.WhenDataChanged), new DataChangedSpot
                       {
                           DocumentID = document["_id"].AsString,
                           EventID    = $"{task.DatabaseName}.{task.CollectionName}",
                           HandlerID  = task.UserID,
                           Operation  = task.Operation,
                       });
        }

        async Task HandleRemovingJob(DatabaseTask task, ILiteCollection<BsonDocument> collection)
        {
            //
            //
            collection.Delete(task.Payload);

            await _Hub.Clients
                      .All
                      .SendAsync(nameof(ISpotRecipient.WhenDataChanged), new DataChangedSpot
                       {
                           DocumentID = task.Payload,
                           EventID    = $"{task.DatabaseName}.{task.CollectionName}",
                           HandlerID  = task.UserID,
                           Operation  = task.Operation,
                       });
        }

        async Task Handle(DatabaseTask task)
        {
            try
            {
                var databaseManager = _Manager.GetDatabase(task.DatabaseName).Database;
                var collection      = databaseManager?.GetCollection(task.CollectionName);

                if (collection is null)
                {
                    return;
                }


                switch (task.Operation)
                {
                    case DataOperation.Added:
                        await HandleAddingJob(task, collection);
                        break;
                    case DataOperation.Removed:
                        await HandleRemovingJob(task, collection);
                        break;
                    case DataOperation.Updated:
                        await HandleUpdatingJob(task, collection);
                        break;
                }
            }
            catch(Exception ex)
            {
                //TODO: 日志、重试、死信队列
                Console.WriteLine($"DB Error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Content("Hi");
        }
        
        [return: ReturnType<Result>]
        [HttpGet("gen")]
        [Authorize]
        public IActionResult Generate([FromServices] IDatabaseService manager, string dbName, string colName)
        {
            var claim_userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim_userId is null)
            {
                return BadRequest("UserID不存在");
            }

            var usr = _usrSrv.GetUser(claim_userId.Value);

            if (usr is null)
            {
                return BadRequest("用户不存在或者无效的权限");
            }
            
            
            var databaseManager = _Manager.GetDatabase(dbName).Database;
            var collection      = databaseManager.GetCollection(colName);

            for (var i = 0; i < 100; i++)
            {
                collection.Insert(new BsonDocument
                {
                    { "_id", ID.Get() },
                    { "name", i.ToString() },
                    { "value", i.ToString() },
                });
            }
            
            return Ok();
        }


        [HttpPost("add")]
        [Authorize]
        public async Task<IActionResult> Add(string dbName, string colName)
        {

            var claim_userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim_userId is null)
            {
                return BadRequest("UserID不存在");
            }

            using var reader  = new StreamReader(Request.Body);
            var       payload = await reader.ReadToEndAsync();

            await Handle(new DatabaseTask
            {
                DatabaseName   = dbName,
                CollectionName = colName,
                Payload        = payload,
                UserID         = claim_userId.Value,
                Operation      = DataOperation.Added,
            });

            return Ok();
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> Update(string dbName, string colName)
        {
            var claim_userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim_userId is null)
            {
                return BadRequest("UserID不存在");
            }

            using var reader  = new StreamReader(Request.Body);
            var       payload = await reader.ReadToEndAsync();


            await Handle(new DatabaseTask
            {
                DatabaseName   = dbName,
                CollectionName = colName,
                Payload        = payload,
                UserID         = claim_userId.Value,
                Operation      = DataOperation.Updated,
            });

            return Ok();
        }


        [HttpGet("remove")]
        [Authorize]
        public async Task<IActionResult> Remove(string dbName, string colName, string id)
        {
            var claim_userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);


            if (claim_userId is null)
            {
                return BadRequest("UserID不存在");
            }

            await Handle(new DatabaseTask
            {
                DatabaseName   = dbName,
                CollectionName = colName,
                Payload        = id,
                UserID         = claim_userId.Value,
                Operation      = DataOperation.Removed,
            });

            return Ok();
        }

        [HttpGet("get")]
        [Authorize]
        public IActionResult Get([FromServices] IDatabaseService manager, string dbName, string colName, string id)
        {

            var databaseManager = manager?.GetDatabase(dbName).Database;
            var collection      = databaseManager?.GetCollection(colName);

            if (collection is null)
            {
                return NotFound();
            }

            if (collection.HasID(id))
            {
                return Json(Result<string>.Success(JsonSerializer.Serialize(collection.FindById(id))));
            }

            return Json(Result<string>.Failed(""));
        }


        [return: GzipReturn]
        [HttpGet("query")]
        [Authorize]
        public IActionResult Query([FromServices] IDatabaseService manager, string dbName, string query)
        {
            //
            // 在Query中不允许出现Drop、Delete
            if (string.IsNullOrEmpty(dbName))
            {
                return BadRequest("数据库名字为空。");
            }

            if (string.IsNullOrEmpty(query))
            {
                return BadRequest("SQL语句为空。");
            }

            if (Regex.IsMatch(query, NotSelectRegex))
            {
                return BadRequest("SQL语句出现危险字段。");
            }

            var database = manager.GetDatabase(dbName)
                                  .Database;

            using (var reader = database.Execute(query))
            {
                var gzip = reader.SerializeAsGZipStream();
                return File(gzip, "application/zip", "files.zip");
            }
        }
        
        [HttpGet("table")]
        [return: GzipReturn]
        [Authorize]
        public IActionResult Table([FromServices] IDatabaseService manager, string dbName,  string colName)
        {
            //
            // 在Query中不允许出现Drop、Delete
            if (string.IsNullOrEmpty(dbName))
            {
                return BadRequest("数据库名字为空。");
            }

            if (string.IsNullOrEmpty(colName))
            {
                return BadRequest("集合名为空。");
            }
            
            var database = manager.GetDatabase(dbName)
                                  .Database;
            var collection      = database?.GetCollection(colName);

            if (collection is null)
            {
                return NotFound();
            }
            
            
            var gzip = collection.SerializeAsGZipStream();
            
            return File(gzip, "application/zip", "files.zip");
        }
    }


    public readonly record struct DatabaseTask(string UserID,
                                               string DatabaseName,
                                               string CollectionName,
                                               DataOperation Operation,
                                               string Payload);
}