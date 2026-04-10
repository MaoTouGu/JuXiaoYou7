using MaoTouGu.Shells.Languages;

namespace MaoTouGu.Shells.Core
{
    public static class QuestionExt
    {

        //-------------------------------------------------------------
        //
        //          Question
        //
        //-------------------------------------------------------------

        public static Task<bool> Question(this PageBase target, NotifyType type, string title, string desc, string ok = null, string cancel = null)
        {
            if (string.IsNullOrEmpty(ok))
            {
                ok = I18N.GetEnum(ButtonText.Ok);
            }

            if (string.IsNullOrEmpty(cancel))
            {
                cancel = I18N.GetEnum(ButtonText.Cancel);
            }

            return Dialog.AddDialog(target, new QuestionRoot(type, title, desc, ok, cancel)).Awaitable;
        }


        public static Task<bool> Question(this PageBase target, BooleanRoot root)
        {
            return Dialog.AddDialog(target, root).Awaitable;
        }

        public static Task<TripleOption> Question(this PageBase target, TripleOptionRoot root)
        {
            return Dialog.AddDialog(target, root).Awaitable;
        }

        public static Task<QuadOption> Question(this PageBase target, QuadOptionRoot root)
        {
            return Dialog.AddDialog(target, root).Awaitable;
        }
        //-------------------------------------------------------------
        //
        //          QueryWith
        //
        //-------------------------------------------------------------
        public static Task<bool> QueryWithDanger(this PageBase target, string title, string desc, string ok = null, string cancel = null)
        {
            return Question(target, NotifyType.Danger, title, desc, ok, cancel);
        }

        public static Task<bool> QueryWithWarning(this PageBase target, string title, string desc, string ok = null, string cancel = null)
        {
            return Question(target, NotifyType.Warning, title, desc, ok, cancel);
        }

        public static Task<bool> QueryWithInfo(this PageBase target, string title, string desc, string ok = null, string cancel = null)
        {
            return Question(target, NotifyType.Info, title, desc, ok, cancel);
        }


        public static Task<bool> QueryWithSuccess(this PageBase target, string title, string desc, string ok = null, string cancel = null)
        {
            return Question(target, NotifyType.Success, title, desc, ok, cancel);
        }


        public static Task<bool> QueryWithObsoleted(this PageBase target, string title, string desc, string ok = null, string cancel = null)
        {
            return Question(target, NotifyType.Obsoleted, title, desc, ok, cancel);
        }

        public static Task<TripleOption> Query(this PageBase target, string title, string desc, string op1, string op2)
        {
            return Question(target, new TripleOptionRoot(title, desc, op1, op2));
        }


        public static Task<QuadOption> Query(this PageBase target, string title, string desc, string op1, string op2, string op3)
        {
            return Question(target, new QuadOptionRoot(title, desc, op1, op2, op3));
        }

        //-------------------------------------------------------------
        //
        //          Question
        //
        //-------------------------------------------------------------

        public static Task<bool> Question(this DialogBase target, NotifyType type, string title, string desc, string ok = null, string cancel = null)
        {
            if (string.IsNullOrEmpty(ok))
            {
                ok = I18N.GetEnum(ButtonText.Ok);
            }

            if (string.IsNullOrEmpty(cancel))
            {
                cancel = I18N.GetEnum(ButtonText.Cancel);
            }

            return Dialog.AddDialog(target, new QuestionRoot(type, title, desc, ok, cancel)).Awaitable;
        }


        public static Task<TripleOption> Question(this DialogBase target, TripleOptionRoot root)
        {
            return Dialog.AddDialog(target, root).Awaitable;
        }

        public static Task<QuadOption> Question(this DialogBase target, QuadOptionRoot root)
        {
            return Dialog.AddDialog(target, root).Awaitable;
        }

        public static Task<bool> Question(this DialogBase target, BooleanRoot root)
        {
            return Dialog.AddDialog(target, root).Awaitable;
        }

        //-------------------------------------------------------------
        //
        //          QueryWith
        //
        //-------------------------------------------------------------
        public static Task<bool> QueryWithDanger(this DialogBase target, string title, string desc, string ok = null, string cancel = null)
        {
            return Question(target, NotifyType.Danger, title, desc, ok, cancel);
        }

        public static Task<bool> QueryWithWarning(this DialogBase target, string title, string desc, string ok = null, string cancel = null)
        {
            return Question(target, NotifyType.Warning, title, desc, ok, cancel);
        }

        public static Task<bool> QueryWithInfo(this DialogBase target, string title, string desc, string ok = null, string cancel = null)
        {
            return Question(target, NotifyType.Info, title, desc, ok, cancel);
        }


        public static Task<bool> QueryWithSuccess(this DialogBase target, string title, string desc, string ok = null, string cancel = null)
        {
            return Question(target, NotifyType.Success, title, desc, ok, cancel);
        }


        public static Task<bool> QueryWithObsoleted(this DialogBase target, string title, string desc, string ok = null, string cancel = null)
        {
            return Question(target, NotifyType.Obsoleted, title, desc, ok, cancel);
        }
        public static Task<TripleOption> Query(this DialogBase target, string title, string desc, string op1, string op2)
        {
            return Question(target, new TripleOptionRoot(title, desc, op1, op2));
        }


        public static Task<QuadOption> Query(this DialogBase target, string title, string desc, string op1, string op2, string op3)
        {
            return Question(target, new QuadOptionRoot(title, desc, op1, op2, op3));
        }
    }
}