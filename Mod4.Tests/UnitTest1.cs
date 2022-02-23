using System;
using Xunit;
using Mod4;

namespace Mod4.Tests
{
    public class FormatGenreTest
    {
        [Fact]
        public void Test1()
        {
            var genreEntry = "action adventure fantasy";
            string result = genreEntry.Replace(" ", "|");

            //return formattedGenre;
            Assert.Equal("action|adventure|fantasy", result);
        }
    }
}