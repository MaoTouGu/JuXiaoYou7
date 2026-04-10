namespace MaoTouGu.Foundation
{
    public abstract class LifetimeExtended : Lifetime
    {
        
        
        //-------------------------------------------------------------
        //
        //          Suspend
        //
        //-------------------------------------------------------------


        #region Suspend

        public void Suspend()
        {
            if (!IsInitialized)
            {
                OnLogging("Suspend() was called, but this instance is not initialized");
                return;
            }

            try
            {
                SuspendBefore();
                OnSuspend();
                SuspendAfter();
            }
            catch (Exception e)
            {
               OnException(nameof(Suspend), e);
            }
        }

        protected virtual void SuspendBefore()
        {
        }

        protected virtual void OnSuspend()
        {
        }

        protected virtual void SuspendAfter()
        {
        }

        #endregion


        //-------------------------------------------------------------
        //
        //          Resume
        //
        //-------------------------------------------------------------


        #region Resume

        public void Resume()
        {
            if (!IsInitialized)
            {
                OnLogging("Resume() was called, but this instance is not initialized");
                return;
            }

            try
            {
                ResumeBefore();
                OnResume();
                ResumeAfter();
            }
            catch (Exception e)
            {
                OnException(nameof(Resume), e);
            }
        }

        protected virtual void ResumeBefore()
        {
        }

        protected virtual void OnResume()
        {
        }

        protected virtual void ResumeAfter()
        {
        }

        #endregion
    }
}