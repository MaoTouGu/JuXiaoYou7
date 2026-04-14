// ----------------------------------------------------------
//            文件：PushingBackgroundService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 09:54
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------

using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MaoTouGu.JuXiaoYou.Services
{
    public sealed class PushingBackgroundService : Lifetime, IPushingBackgroundService
    {
        private HubConnection _Hub;

        private readonly Lazy<IViewModelProvider> _shell;
        private readonly Lazy<IUserService>       _userService;

        public PushingBackgroundService()
        {
            _userService = new Lazy<IUserService>(Ioc.Get<IUserService>);
            _shell       = new Lazy<IViewModelProvider>(() => (IViewModelProvider)(Ioc.Get<IAppModel>()));
        }

        protected override async void StartBefore()
        {
            var api = Ioc.SafeGet<IWebApi>();
            var url = api?.SafetyUrl;

            if (api is null || !api.IsOnline)
            {
                return;
            }

            _Hub = new HubConnectionBuilder()
                  .WithUrl($"{url}/hub/events", options =>
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
            try
            {

                //
                // 启动这个Hub。
                _Hub.On<Spot>("WhenDataChanged", WhenDataChanged);

                await _Hub.StartAsync();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        protected override async void OnStop()
        {
            await _Hub.DisposeAsync();
        }

        async Task OnDataChanged(DataChangedSpot args)
        {
            var handler = _shell.Value
                                .GetContextList()
                                .OfType<IPushingEventHandler>()
                                .FirstOrDefault(x => x.CanHandle(args.EventID));

            if (handler is not null)
            {
                var usr = _userService.Value
                                      .Dictionary
                                      .SafetyGet(args.HandlerID);

                await handler.Handle(args.DocumentID, usr.DisplayName, args.EventID, args.Operation);
                return;
            }

            await DatabaseManager.Handle(args);
        }

        Task WhenDataChanged(Spot dataEvent)
        {
            return Task.Run(async () =>
                            {
                                if (dataEvent is UserSpot || dataEvent is UserChangeSpot)
                                {
                                    await _userService.Value.Handle(dataEvent);
                                }
                                else if (dataEvent is DataChangedSpot args)
                                {
                                    await OnDataChanged(args);
                                }

                            });
        }
    }
}