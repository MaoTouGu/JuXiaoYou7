using System.Diagnostics;
using System.Runtime;
using MaoTouGu.Studio.Database.IM;

namespace MaoTouGu.Studio.Hubs
{
    public class IMHub : Hub, IPrivateHub
    {
        public const string GroupID = nameof(GroupID);
        
        public override async Task OnConnectedAsync()
        {
            // 获取 HttpContext
            var httpContext = Context.GetHttpContext();
            var query       = httpContext?.Request.Query;

            // 从查询字符串中读取 groupid
            if (query is not null && query.TryGetValue(GroupID, out var groupId))
            {
                //
                //
                var id = groupId.ToString();

                // 将当前连接添加到指定组
                await Groups.AddToGroupAsync(Context.ConnectionId, id);

                // 可选：存储到 Items 或 Context 中供后续使用
                Context.Items[GroupID] = id;
            }
            
            await base.OnConnectedAsync();
        }

        public async Task SendPrivateAsync(MSG msg)
        {
            
        }
        
        public async Task SendPublicAsync(MSG msg)
        {
            
        }

        //
        // 私聊信息。
        public async Task SendC2CAsync(MSG msg)
        {
            await Channels.Private.Writer.WriteAsync(msg);
        }

        public async Task PostC2CMessageAsync(MSG msg)
        {
            
        }
    }
}