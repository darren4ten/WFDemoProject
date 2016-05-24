using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using Models;

namespace WFLibs
{

    public sealed class WaitForInputGeneric<T> : NativeActivity<T>
    {

        public InArgument<string> OperationNames
        {
            get;
            set;
        }

        public InOutArgument<T> workflowModel { get; set; }

        public string NodeName { get; set; }

        /// <summary>
        /// 创建书签时，必须设置CanInduceIdle为true
        /// </summary>
        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }

        protected override void Execute(NativeActivityContext context)
        {

            context.CreateBookmark("BookmarkTest", new BookmarkCallback(bookmarkCallback));
        }

        void bookmarkCallback(NativeActivityContext context, Bookmark bookmark, object value)
        {
            Dictionary<string, object> dic = value as Dictionary<string, object>;
            //dic.Add("model", value);
            T data = (T)(dic["model"]);
            context.SetValue(workflowModel, data);
        }
    }
}
