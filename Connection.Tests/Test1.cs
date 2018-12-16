using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1;
using Xunit;


namespace Connection.Tests
{
    public class Test1
    {
        [Theory]
        [InlineData("uzytkownik/VAG24?string=8W0133835H&order=m&bmatch=cl-n-eng-global-uni-1-3-1130")]
        [InlineData("https://allegro.pl/uzytkownik/VAG24?string=8W0133835H&order=m&bmatch=cl-n-eng-global-uni-1-3-1130")]
        public void getSource_pathiswrong(string url)
        {
            Action  act = () => WindowsFormsApp1.Form1.getSource(url);
            Assert.Throws<ArgumentException>(act);
        }

        [Theory]
        [InlineData("https://allllegro.pl/")]
        public void getSource_wrongaddress(string url)
        {
            Action act = () => WindowsFormsApp1.Form1.getSource(url);
            Assert.Throws<System.Net.WebException>(act);
        }
    }
}
