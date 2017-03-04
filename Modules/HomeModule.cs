using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace BandTracker
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                List<Venue> AllVenues = Venue.GetVenues();
                List<Band> AllBands = Band.GetBands();
                model.Add("venues", AllVenues);
                model.Add("bands", AllBands);
                return View["index.cshtml", model];
            };

            Post["/add-venue"] = _ => {
                Venue newVenue = new Venue(Request.Form["venue-name"]);
                newVenue.Save();
                Dictionary<string, object> model = new Dictionary<string, object>();
                List<Venue> AllVenues = Venue.GetVenues();
                List<Band> AllBands = Band.GetBands();
                model.Add("venues", AllVenues);
                model.Add("bands", AllBands);
                return View["success.cshtml", model];
            };

            Get["/venue/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Venue SelectedVenue = Venue.Find(parameters.id);
                List<Band> BandList = SelectedVenue.GetBands();
                model.Add("venues", Venue.Find(parameters.id));
                model.Add("bands", BandList);
                return View["venues.cshtml", model];
            };

            Post["/venue/{id}/band/new"] = parameters => {
                Band newBand = new Band(Request.Form["band-name"]);
                newBand.Save();
                Venue SelectedVenue = Venue.Find(parameters.id);
                SelectedVenue.AddBand(newBand);
                Dictionary<string, object> model = new Dictionary<string, object>();
                List<Band> BandList = SelectedVenue.GetBands();
                model.Add("venues", SelectedVenue);
                model.Add("bands", BandList);
                return View["success.cshtml", model];
            };

            
        }

    }
}
