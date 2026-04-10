// ----------------------------------------------------------
//            文件：LockController.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月07日 17:34
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using System.Security.Claims;
using MaoTouGu.Foundation;

namespace MaoTouGu.Studio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LockController(IResourceLockingService _LockService, IUserService _UserDB) : Controller
    {
        [HttpGet("get")]
        [Authorize]
        public IActionResult Get(string id)
        {
            var claim_userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim_userId is null)
            {
                return Json(Result.Failed("UserID不存在"));
            }

            if (string.IsNullOrEmpty(id))
            {
                return Json(Result.Failed("文档id不存在"));
            }

            
            var dl = _LockService.Has(id);

            return Json(Result.Success(dl?.OwnerID));
        }
        
        [HttpGet("open")]
        [Authorize]
        public IActionResult Open(string id)
        {
            
            var claim_userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim_userId is null)
            {
                return Json(Result.Failed("UserID不存在"));
            }

            var userID = claim_userId.Value;
            
            if (string.IsNullOrEmpty(id))
            {
                return Json(Result.Failed("文档id不存在"));
            }

            var dl = _LockService.Has(id);

            if (dl is null)
            {
                _LockService.Add(id, userID);
                return Json(Result.Success("开始编辑当前文档"));
            }

            if (dl.OwnerID != userID)
            {
                return Json(Result.Failed("他人正在编辑此资源，无法取得文档锁。"));
            }
            
            return Json(Result.Failed("已经取得了文档锁。"));
        }
        
        [HttpGet("refresh")]
        [Authorize]
        public IActionResult Refresh(string id)
        {
            var claim_userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim_userId is null)
            {
                return Json(Result.Failed("UserID不存在"));
            }

            var userID = claim_userId.Value;
            
            if (string.IsNullOrEmpty(id))
            {
                return Json(Result.Failed("文档id不存在"));
            }
            
            var dl = _LockService.Has(id);

            if (dl is null)
            {
                _LockService.Add(id, userID);
                return Json(Result.Failed("当前文档不存在。"));
            }

            if (dl.OwnerID != userID)
            {
                return Json(Result.Failed("你没有刷新该锁的权限。"));
            }
            
            _LockService.Refresh(id);
            return Json(Result.Success("刷新成功！"));
        }
        
        [HttpGet("release")]
        [Authorize]
        public IActionResult Release(string id)
        {
            var claim_userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim_userId is null)
            {
                return Json(Result.Failed("UserID不存在"));
            }

            var userID = claim_userId.Value;
            
            if (string.IsNullOrEmpty(id))
            {
                return Json(Result.Failed("文档id不存在"));
            }
            
            var dl = _LockService.Has(id);

            if (dl is null)
            {
                _LockService.Add(id, userID);
                return Json(Result.Failed("当前文档不存在。"));
            }

            if (dl.OwnerID != userID)
            {
                return Json(Result.Failed("你没有删除该锁的权限。"));
            }
            
            _LockService.Release(id);
            return Json(Result.Success("刷新成功！"));
        }
        
        [HttpGet("remove")]
        [Authorize]
        public IActionResult Remove()
        {
            var claim_userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim_userId is null)
            {
                return Json(Result.Failed("UserID不存在"));
            }

            var userID = claim_userId.Value;

            
            _LockService.ReleaseAll(userID);
            return Json(Result.Success("清空您所拥有的文档锁成功！"));
        }
        
        [HttpGet("removeAll")]
        [Authorize]
        public IActionResult RemoveAll()
        {
            var claim_userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim_userId is null)
            {
                return Json(Result.Failed("UserID不存在"));
            }

            var userID = claim_userId.Value;

            if (!_UserDB.IsSuperAdmin(userID))
            {
                return Json(Result.Failed("您不是超级管理员，无权限使用此功能。"));
            }
            
            
            
            _LockService.ReleaseAll();
            return Json(Result.Success("清空所有文档锁成功！"));
        }
    }
}