// ----------------------------------------------------------
//            文件：LobbyViewModel.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月10日 14:27
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using MaoTouGu.JuXiaoYou.Domain.IM.Pages.Commands;
using MaoTouGu.JuXiaoYou.Domain.IM.Services;
using MaoTouGu.JuXiaoYou.Services.Networks;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MaoTouGu.JuXiaoYou.Domain.IM.Pages
{
    public sealed partial class LobbyViewModel : SystemPage, IHostedWindowNavigation
    {
        private readonly HubConnection _connection;

        private User _target;

        public LobbyViewModel()
        {
            WebApi = Ioc.Get<IWebApi>();
            Other  = new ViewList<User>();
            All    = new ViewList<User>();

            C2CMsgCollection = new ViewList<IMMessageVPO>();

            AddPlainTextToC2C = new AddPlainTextToC2CCommand(this);

            //
            //
            _c2cSrv = new PrivateMessageService();

            //
            // 初始化SignalR
            _connection = new HubConnectionBuilder()
                         .WithUrl($"{WebApi.SafetyUrl}/hub/chat?userId={WebApi.UserID}", options =>
                                                                                         {
                                                                                             // options.Headers.Add("X-Token", WebApi.User.Token);
                                                                                             // options.Headers.Add("X-Device", UserAgent.Device);
                                                                                             options.Transports      = HttpTransportType.WebSockets;
                                                                                             options.SkipNegotiation = true;
                                                                                         })
                         .AddNewtonsoftJsonProtocol(options =>
                                                    {
                                                        options.PayloadSerializerSettings = new JsonSerializerSettings
                                                        {
                                                            TypeNameHandling = TypeNameHandling.All,
                                                        };
                                                    })
                         .WithAutomaticReconnect()
                         .WithKeepAliveInterval(TimeSpan.FromMinutes(1))
                         .WithServerTimeout(TimeSpan.FromSeconds(30))
                         .Build();

            //
            // 注册
            _connection.On<MSG>("Received", Received);
        }

        private async Task GetUserListAsync()
        {
            var list = await WebApi.GetUserListAsync();

            if (list.IsFinished)
            {
                GUI.RunOnUIThread(() =>
                                  {
                                      All.AddMany(list.Value, true);
                                      Other.AddMany(All.Where(x => x.Id != WebApi.UserID), true);
                                  });
            }
        }
        protected override async void OnStart()
        {
            await GetUserListAsync();

            //
            // 启动连接。
            await _connection.StartAsync();
        }

        public User Target
        {
            get => _target;
            set
            {
                SetValue(ref _target, value);
                OpenChannelAsync();
            }
        }

        public IWebApi WebApi { get; }

        public User User => WebApi?.User;
    }
}