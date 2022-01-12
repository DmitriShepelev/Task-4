using System;

namespace Task_4.BLL.Handlers
{
    public class HandlerException : Exception
    {
        public HandlerException(Exception inner) : base("Handler exception", inner)
        {

        }
    }
}
