using System;

namespace TvMaze.Core.Extensions
{
    public static class ExceptionExtensions
    {
        public static string Message(this Exception ex)
        {
            return $"{ex.Message}{GetSeparator()}{ex.InnerException}{GetSeparator()}{ex.StackTrace}";
        }

        private static string GetSeparator() => $"{Environment.NewLine}========================================{Environment.NewLine}";
    }
}
