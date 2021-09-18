using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VectorViewer.Exceptions
{
    static class TaskExtensions
    {
        public static async void RunSafe(this Task task, Action<Exception> exceotionHandler)
        {
            try
            {
                await task;
            }
            catch(Exception e)
            {
                exceotionHandler?.Invoke(e);
            }
        }
    }
}
