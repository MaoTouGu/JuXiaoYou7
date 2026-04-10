// ----------------------------------------------------------
//            文件：UserService.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年02月23日 09:52
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
using MaoTouGu.Studio.Database.Identity;
using MaoTouGu.Studio.Database.Spots;

namespace MaoTouGu.JuXiaoYou.Services
{
    public class UserService : Lifetime, IUserService
    {
        private readonly Dictionary<string, User> _Dict;
        private readonly ViewList<User>           _List;

        public UserService()
        {
            _Dict = new();
            _List = new();

            Collection = _List;
            Dictionary = new ReadOnlyDictionary<string, User>(_Dict);
        }
        
        
        private async Task OnUserChanged(UserSpot e)
        {
            var api = Ioc.SafeGet<IUserApiContract>();

            if (api is null)
            {
                return;
            }
            
            if (e.Operation == DataOperation.Added)
            {
                var r = await api.GetUserAsync(e.UserID);

                if (!r.IsFinished)
                {
                    return;
                }
                
                GUI.RunOnUIThread(() =>
                                  {
                                      if (_Dict.TryAdd(e.UserID, r.Value))
                                      {
                                          _List.Add(r.Value);
                                      }
                                  });
            }
            else if (e.Operation == DataOperation.Updated)
            {
                
                var r = await api.GetUserAsync(e.UserID);

                if (!r.IsFinished)
                {
                    return;
                }
                
                GUI.RunOnUIThread(() =>
                                  {
                                      if (_Dict.Remove(e.UserID) &&
                                          _Dict.TryAdd(e.UserID, r.Value))
                                      {
                                          _List.Remove(x => x.Id == e.UserID);
                                          _List.Add(r.Value);
                                      }
                                  });
            }
            else
            {
                GUI.RunOnUIThread(() =>
                                  {
                                      _Dict.Remove(e.UserID);
                                      _List.Remove(x => x.Id == e.UserID);
                                  });
            }
        }
        
        private void OnUserChanged(UserChangeSpot e)
        {
            var usr = _Dict.GetValueOrDefault(e.UserID);

            if (usr is null)
            {
                return;
            }
            
            if (e.NewGravatar != e.OldGravatar)
            {
                usr.Gravatar = e.NewGravatar;
            }
            else if (e.NewName != e.OldName)
            {
                usr.DisplayName = e.NewName;
            }
        }

        protected sealed override async void StartBefore()
        {
            var api = Ioc.SafeGet<IUserApiContract>();

            if (api is null)
            {
                return;
            }

            var r = await api.GetUserListAsync();


            if (!r.IsFinished)
            {
                return;
            }
            
            GUI.RunOnUIThread(() =>
                              {
                                  foreach (var user in r.Value.Where(user => _Dict.TryAdd(user.Id, user)))
                                  {
                                      _List.Add(user);
                                  }
                              });
        }

        public async Task Handle(Spot dataEvent)
        {
            if (dataEvent is null)
            {
                return;
            }
            
            if (dataEvent is UserSpot us)
            {
                await OnUserChanged(us);
                Debug.WriteLine("用户数量发生变化！");
            }

            if (dataEvent is UserChangeSpot ucs)
            {
                GUI.RunOnUIThread(() => OnUserChanged(ucs));
                Debug.WriteLine("用户属性发生变化！");
            }
        }

        public IReadOnlyList<User> Collection { get; }

        public IReadOnlyDictionary<string, User> Dictionary { get; }
    }
}