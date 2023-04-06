using System.ComponentModel;

namespace Flights.Dtos
{
    public record FlightSearchParameters(

        [DefaultValue("12/25/2023 10:30:00 AM")]
        DateTime? FromDate,

        [DefaultValue("12/26/2023 10:30:00 AM")]
        DateTime? ToDate,

        [DefaultValue("Berlin")]
        string? From,

        [DefaultValue("Vilnius")]
        string? Destination,

        [DefaultValue("Lufthansa")]
        string? Airline,

        [DefaultValue(1)]
        int? NumberOfPassengers

        );
}
