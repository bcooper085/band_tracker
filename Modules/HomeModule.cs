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
                Dictionary<string, object> model = new Dictionary<string object>();
                List<Venue> AllVenues = Venue.GetVenues();
                List<Band> AllBands = Band.GetBands();
                model.Add("venue", AllVenues);
                model.Add("bands", AllBands);
                return View["index.cshtml", model];
            };

            Post["/add-venue"] = _ => {
                Venue newVenue = new Venue(Request.Form["venue-name"]);
                newVenue.Save();
                Dictionary<string, object> model = new Dictionary<string object>();
                List<Venue> AllVenues = Venue.GetVenues();
                List<Band> AllBands = Band.GetBands();
                model.Add("venue", AllVenues);
                model.Add("bands", AllBands);
                return View["venues.cshtml", model];
            };

            Get["/venue/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                model.Add("venue", Venue.Find(parameters.id));
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
                model.Add("venue", SelectedVenue);
                model.Add("bands", BandList);
                return View["bands_venues.cshtml", model];
            };
            Get["/band/new"] = _ => {
                return View["bands_venues.cshtml"];
            };
            Get["/band/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Band SelectedBand = Band.Find(parameters.id);
                List<Venue> BandVenue = SelectedBand.GetVenues();
                model.Add("band", SelectedBand);
                return View["index.cshtml"];
            };

            Get["/venue/delete/{id}"] = parameters => {
                Venue SelectedVenue = Venue.Find(parameters.id);
                return View["bands_venues.cshtml", SelectedVenue];
            };

            Patch["/venue/edit/{id}"] = parameters => {
                Venue.UpdateName(parameters.id, Request.Form["venue-name"]);
                Dictionary<string, object> model = new Dictionary<string, object>();
                model.Add("venue", Venue.Find(parameters.id));
                return View["success.cshtml", model];
            };

            Delete["/venue/delete/{id}"] = parameters => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Venue SelectedVenue = Venue.Find(parameters.id);
                SelectedVenue.Delete();
                model.Add("venue", SelectedVenue);
                return View["success.cshtml", model];
            };
        }

    }
}
