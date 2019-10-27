using System;

namespace MobileApp.Exceptions
{
    class PermissionDeniedException : Exception
    {
        public PermissionDeniedException()
        {
        }

        public PermissionDeniedException(string message) : base(message)
        {
        }
    }
}
