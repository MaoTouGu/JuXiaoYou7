using System.Windows.Threading;
using MaoTouGu.Shells.Core;

namespace MaoTouGu.Shells.Controls
{
    partial class DialogHost : INotificationManager
    {
        private readonly Queue<Notification> _Queue;
        private readonly DispatcherTimer     _Timer;

        //
        // Counting 使用250ms作为一个Tick
        private int          _Counting;
        private Notification _Current;
        
        private void OnNotificationProc(object sender, EventArgs e)
        {
            if (_Current is null)
            {
                //
                // 如果_Current为空，则判断
                if (_Queue.Count == 0)
                {
                    //
                    // 停止使用定时器
                    _Timer.Stop();
                    PART_MsgMask.Visibility = Visibility.Collapsed;
                    
                    //
                    // 抛出NotificationClosing事件。
                    RaiseEvent(new RoutedEventArgs
                    {
                        RoutedEvent = NotificationClosingEvent,
                    });
                }
                else
                {
                    if (PART_MsgMask.Visibility == Visibility.Collapsed)
                    {
                        PART_MsgMask.Visibility = Visibility.Visible;
                        RaiseEvent(new RoutedEventArgs
                        {
                            RoutedEvent = NotificationOpeningEvent,
                        });
                    }


                    //
                    // 弹出新的消息。
                    _Current         = _Queue.Dequeue();
                    _Counting        = 0;
                    PART_MSG.Content = new NotificationControl { DataContext = _Current };
                    
                    RaiseEvent(new RoutedEventArgs
                    {
                        RoutedEvent = NotificationChangedEvent,
                    });
                }
            }
            else
            {
                if (_Counting >= _Current.Duration)
                {
                    _Counting        = 0;
                    _Current         = null;
                    PART_MSG.Content = null;


                    PART_MsgMask.Visibility = Visibility.Collapsed;
                    RaiseEvent(new RoutedEventArgs
                    {
                        RoutedEvent = NotificationClosingEvent,
                    });
                }
                else
                {
                    _Counting++;
                }
            }
        }

        void ShowNotification(Notification notification)
        {
            _Counting        = 0;
            _Current         = notification;
            
            //
            // 直接显示。
            PART_MsgMask.Visibility = Visibility.Visible;
            PART_MSG.Content        = new NotificationControl { DataContext = notification };
            
            //
            // 启动计时器
            _Timer.Start();
            
            //
            // 触发事件。
            RaiseEvent(new RoutedEventArgs
            {
                RoutedEvent = NotificationOpeningEvent,
            });
        }

        public void Notify(Notification notification)
        {
            if (notification is null)
            {
                return;
            }

            GUI.RunOnUIThread(() =>
                              {
                                  if (_Current is null)
                                  {
                                      //
                                      // 如果队列没东西，则直接显示。
                                      ShowNotification(notification);
                                  }
                                  else
                                  {
                                      //
                                      // 添加到队列。
                                      _Queue.Enqueue(notification);
                                  }
                              });
        }


        public event RoutedEventHandler NotificationOpening
        {
            add => AddHandler(NotificationOpeningEvent, value);
            remove => RemoveHandler(NotificationOpeningEvent, value);
        }
        

        public event RoutedEventHandler NotificationChanged
        {
            add => AddHandler(NotificationChangedEvent, value);
            remove => RemoveHandler(NotificationChangedEvent, value);
        }
        
        public event RoutedEventHandler NotificationClosing
        {
            add => AddHandler(NotificationClosingEvent, value);
            remove => RemoveHandler(NotificationClosingEvent, value);
        }
    }
}