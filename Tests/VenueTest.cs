using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
    public class VenueTest : IDisposable
    {
        public VenueTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_CategoriesEmptyAtFirst()
        {
            //Arrange, Act
            int result = Venue.GetVenues().Count;

            //Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueForSameName()
        {
            //Arrange, Act
            Venue firstVenue = new Venue("Red Rocks");
            Venue secondVenue = new Venue("Red Rocks");

            //Assert
            Assert.Equal(firstVenue, secondVenue);
        }

        [Fact]
        public void Test_Save_AssignsIdToVenue()
        {
            //Arrange
            Venue testVenue = new Venue("Red Rocks");
            testVenue.Save();

            //Act
            Venue savedVenue = Venue.GetVenues()[0];

            int result = savedVenue.GetId();
            int testId = testVenue.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Save_SavesVenueToDatabase()
        {
            //Arrange
            Venue testVenue = new Venue("Red Rocks");
            testVenue.Save();

            //Act
            List<Venue> result = Venue.GetVenues();
            List<Venue> testList = new List<Venue>{testVenue};

            //Assert
            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Find_FindsVenueInDatabase()
        {
            //Arrange
            Venue testVenue = new Venue("Red Rocks");
            testVenue.Save();

            //Act
            Venue foundVenue = Venue.Find(testVenue.GetId());

            //Assert
            Assert.Equal(testVenue, foundVenue);
        }

        [Fact]
        public void Test_Delete_DeletesVenueFromDatabase()
        {
            //Arrange
            Venue testVenue = new Venue("Red Rocks");
            testVenue.Save();

            //Act
            testVenue.Delete();
            List<Venue> result = Venue.GetVenues();
            List<Venue> testVenues = new List<Venue>{};

            //Assert
            Assert.Equal(testVenues, result);
        }

        public void Dispose()
        {
            Venue.DeleteAll();
        }
    }
}
