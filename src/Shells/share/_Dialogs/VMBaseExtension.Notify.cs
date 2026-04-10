namespace MaoTouGu.Shells.Core
{
    public static class NotifyExt
    {
        private static void Notify(ViewModelBase target, Notification notification)
        {
            GUI.RunOnUIThread(() =>
                              {
                                  var host = Ioc.Get<IAppModel>().GetDialogHost(target);

                                  if (host is not INotificationManager inm)
                                  {
                                      return;
                                  }

                                  inm.Notify(notification);
                              });
        }

        public static Task<bool> Receive(this PageBase target, params object[] args)
        {
            return target.ReceiveInternal(args);
        }

        public static void Obsoleted(this ViewModelBase target, string title, string description, int duration = 10)
        {
            var notification = new Notification
            {
                Duration   = Math.Clamp(duration, 5, 100),
                Title      = title,
                Content    = description,
                Background = "#993d00",
                Color      = "#ca5100",
            };

            Notify(target, notification);
        }
        public static void SlateGray(this ViewModelBase target, string title, string description, int duration = 10)
        {
            var notification = new Notification
            {
                Duration   = Math.Clamp(duration, 5, 100),
                Title      = title,
                Content    = description,
                Background = "#4f5b66",
                Color      = "#708090",
            };

            Notify(target, notification);
        }

        public static void Info(this ViewModelBase target, string title, string description, int duration = 10)
        {
            var notification = new Notification
            {
                Duration   = Math.Clamp(duration, 5, 100),
                Title      = title,
                Content    = description,
                Color      = "#0092f2",
                Background = "#00558f",
            };

            Notify(target, notification);
        }

        public static void Success(this ViewModelBase target, string title, string description, int duration = 10)
        {
            var notification = new Notification
            {
                Duration   = Math.Clamp(duration, 5, 100),
                Title      = title,
                Content    = description,
                Background = "#768b27",
                Color      = "#99b433",
            };

            Notify(target, notification);
        }

        public static void Warning(this ViewModelBase target, string title, string description, int duration = 10)
        {
            var notification = new Notification
            {
                Duration   = Math.Clamp(duration, 5, 100),
                Title      = title,
                Content    = description,
                Background = "#a87200",
                Color      = "#db9400",
            };

            Notify(target, notification);
        }

        public static void Danger(this ViewModelBase target, string title, string description, int duration = 10)
        {
            var notification = new Notification
            {
                Duration   = Math.Clamp(duration, 5, 100),
                Title      = title,
                Content    = description,
                Background = "#a60609",
                Color      = "#bb2124",
            };

            Notify(target, notification);
        }
    }
}