// ----------------------------------------------------------
//            文件：DataService.Lifetime.cs
//            作者：Luoyisi<acorisbk@qq.com>
//            创建时间：2026年04月06日 02:44
//            版权所有：MaoTouGu Studio & Luoyisi
// 
// ----------------------------------------------------------
namespace MaoTouGu.Studio.Database.Core
{
    partial class DataService
    {
        //-------------------------------------------------------------
        //
        //                          Start
        //
        //-------------------------------------------------------------

        #region Start

        /// <summary>
        /// 启动一个视图模型，注意：视图模型只能启动一次。
        /// </summary>
        public async Task Start()
        {
            if (IsStarted)
            {
                return;
            }
            try
            {
                await StartBefore();
                await OnStart();
                await StartAfter();
                IsStarted = true;
            }
            catch(Exception e)
            {
                OnException(nameof(Start), e);
            }
        }

        /// <summary>
        /// 启动一个视图模型
        /// </summary>
        protected virtual Task StartBefore()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 启动一个视图模型
        /// </summary>
        protected virtual Task OnStart()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 启动一个视图模型
        /// </summary>
        protected virtual Task StartAfter()
        {
            return Task.CompletedTask;
        }

        #endregion
        
        //-------------------------------------------------------------`
        //
        //                          Stop
        //
        //-------------------------------------------------------------

        #region Stop

        /// <summary>
        /// 
        /// </summary>
        public async Task Stop()
        {
            try
            {

                await StopBefore();
                await OnStop();
                await StopAfter();
                IsStarted = false;
            }
            catch(Exception e)
            {
                OnException(nameof(Stop), e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual Task StopBefore() => Task.CompletedTask;

        /// <summary>
        /// 
        /// </summary>
        protected virtual Task OnStop() => Task.CompletedTask;

        /// <summary>
        /// 
        /// </summary>
        protected virtual Task StopAfter() => Task.CompletedTask;

        #endregion
        
        //-------------------------------------------------------------`
        //
        //                          Restart
        //
        //-------------------------------------------------------------
        public async Task Restart()
        {
            await Stop();
            await Start();
        }


        /// <summary>
        /// 判断此类型是否已经启动。
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public bool IsStarted { get; private set; }
    }
}