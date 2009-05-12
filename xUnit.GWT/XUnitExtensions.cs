using System.Collections;
using Xunit;

namespace xUnit.GWT
{
    public static class XUnitExtensions
    {
        public static void ShouldBe<T>(this T actual, T expected)
        {
            Assert.Equal(expected, actual);
        }
    }

    public static class collectionExtensions
    {
        public static bool IsEmpty(this IList actual)
        {
            return actual.Count <= 0;
        }
        
    }
}