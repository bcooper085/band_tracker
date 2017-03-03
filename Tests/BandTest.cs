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
            int result = Band.GetBands().Count;

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
            List<Band> result = Band.GetBands();
            List<Band> testList = new List<Band>{testBand};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Save_AssignsIdToObject()
        {
            //Arrange
            Band testBand = new Band("Angry Fish");

            //Act
            testBand.Save();
            Band savedBand = Band.GetBands()[0];


            int result = savedBand.GetId();
            int testId = testBand.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Find_FindBandInDatabase()
        {
            //Arrange
            Band testBand = new Band("Angry Fish");
            testBand.Save();

            //Act
            Band foundBand = Band.Find(testBand.GetId());

            //Assert
            Assert.Equal(testBand, foundBand);
        }

        public void Dispose()
        {
            Venue.DeleteAll();
            Band.DeleteAll();
        }

    }
}
