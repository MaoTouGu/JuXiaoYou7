// ----------------------------------------------------------
//            文件：AppExt.DangerOp.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2025年12月28日 21:47
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.JuXiaoYou
{
    partial class AppExt
    {
        public static void YouCantRemoveIt(this PageBase target, string title = null, string description = null)
        {

            if (string.IsNullOrEmpty(title))
            {
                title = "警告";
            }

            if (string.IsNullOrEmpty(description))
            {
                description = "你无法删除这个数据，因为这不是你本人创建的！";
            }

            target.Warning(title, description);
        }
        public static void SaveSuccess(this PageBase target, string title = null, string description = null)
        {

            if (string.IsNullOrEmpty(title))
            {
                title = "提示";
            }

            if (string.IsNullOrEmpty(description))
            {
                description = "保存成功！";
            }
            target.Success(title, description);
        }

        public static void RemoveSuccess(this PageBase target, string title = null, string description = null)
        {

            if (string.IsNullOrEmpty(title))
            {
                title = "提示";
            }

            if (string.IsNullOrEmpty(description))
            {
                description = "删除成功！";
            }
            target.Warning(title, description);
        }

        public static async Task<bool> RemoveReference(this PageBase target, string title = null, string description = null)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "警告";
            }

            if (string.IsNullOrEmpty(description))
            {
                description = "你确定要删除这项数据吗？此操作不会删除数据本身，只会删除引用！";
            }


            var r = await target.QueryWithDanger(title, description);

            if (!r)
            {
                return false;
            }

            return true;
        }

        public static async Task<bool> RemoveThis(this PageBase target, string title = null, string description = null)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "警告";
            }

            if (string.IsNullOrEmpty(description))
            {
                description = "你确定要删除这项数据吗？";
            }


            var r = await target.QueryWithDanger(title, description);

            if (!r)
            {
                return false;
            }

            return true;
        }

        public static async Task<bool> ConfirmRemoveThis(this PageBase target, int time, string title = null, string description = null)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "警告";
            }

            if (string.IsNullOrEmpty(description))
            {
                description = $"第{time}次确认，你确定要删除这项数据吗？";
            }


            var r = await target.QueryWithDanger(title, description);

            if (!r)
            {
                return false;
            }

            return true;
        }
    }
}