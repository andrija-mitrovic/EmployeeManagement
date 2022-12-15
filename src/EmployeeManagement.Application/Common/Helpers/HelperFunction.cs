using System.Runtime.CompilerServices;

namespace EmployeeManagement.Application.Common.Helpers
{
    public static class HelperFunction
    {
        public static string GetMethodName([CallerMemberName] string memberName = "")
        {
            return memberName;
        }
    }
}
