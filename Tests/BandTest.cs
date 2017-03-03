using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
    public class BandTest : IDisposable
    {
        public BandTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_DatabaseEmptyAtFirst()
        {
            //Arrange, Act
            int result = Band.GetBand().Count;

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueIfNameEqual()
        {
            //Arrange, Act
            Band firstBand = new Band("Angry Fish");
            Band secondBand = new Band("Angry Fish");

            //Assert
            Assert.Equal(firstBand, secondBand);
        }

        [Fact]
        public void Test_Save_SavesToDatabase()
        {
            //Arrange
            Band testBand = new Band("Angry Fish");

            //Act
            testBand.Save();
            List<Band> result = Band.GetBand();
            List<Band> testList = new List<Band>{testBand};

            //Assert
            Assert.Equal(testList, result);
        }

        public void Dispose()
        {
            Venue.DeleteAll();
            Band.DeleteAll();
        }

    }
}
