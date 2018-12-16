using Xunit;
using WindowsFormsApp1;

namespace FunctionTest
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("https://allegro.pl/uzytkownik/VAG24?string=8W0133835H&order=m&bmatch=cl-n-eng-global-uni-1-3-1130")]
        public void getSource_connection(string url)
        {
            string source = Connection.getSource(url);
            Assert.
        }
    }
}
