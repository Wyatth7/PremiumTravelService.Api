using System.Text.Json.Serialization;
using System.Xml.Serialization;
using PremiumTravelService.Api.Persistence.Entities.StateMachine;
using PremiumTravelService.Api.Persistence.Entities.Trip;

namespace PremiumTravelService.Api.Persistence.Entities;

public class StorageData
{
    [XmlElement("Trips")]
    public List<Trip.Trip> Trips { get; set; }
    public List<TripState> StateMachines { get; set; }
    public List<Person> Travellers { get; set; }
    public List<Person> Agents { get; set; }
    public List<Package> Packages { get; set; }
}