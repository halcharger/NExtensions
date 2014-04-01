using System;

namespace NExtensions
{
    public static class ExceptionExtensions
    {
        public static Exception GetBaseException(this Exception ex)
        {
            var innerEx = ex;
            while (ex.InnerException != null)
                innerEx = ex.InnerException;
            return innerEx;
        }
    }
}