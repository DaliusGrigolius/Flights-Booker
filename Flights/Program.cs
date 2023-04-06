using Microsoft.OpenApi.Models;
using Flights.Data;
using Flights.Domain.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add db context.
builder.Services.AddDbContext<Entities>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("Flights")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
{
    c.DescribeAllParametersInCamelCase();
    c.AddServer(new OpenApiServer
    {
        Description = "Development Server",
        Url = "https://localhost:7296"
    });

    c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"] + e.ActionDescriptor.RouteValues["controller"]}");
});
builder.Services.AddScoped<Entities>();

var app = builder.Build();

var entities = app.Services.CreateScope().ServiceProvider.GetService<Entities>();

entities.Database.EnsureCreated();

var random = new Random();

if (!entities.Flights.Any())
{
    Flight[] flightsToSeed = new Flight[]
        {
            new (   Guid.NewGuid(),
                    "American Airlines",
                    random.Next(90, 5000).ToString(),
                    new TimePlace("Los Angeles",DateTime.Now.AddHours(random.Next(1, 3))),
                    new TimePlace("Istanbul",DateTime.Now.AddHours(random.Next(4, 10))),
                        2),
            new (   Guid.NewGuid(),
                    "Deutsche BA",
                    random.Next(90, 5000).ToString(),
                    new TimePlace("Munchen",DateTime.Now.AddHours(random.Next(1, 10))),
                    new TimePlace("Schiphol",DateTime.Now.AddHours(random.Next(4, 15))),
                    random.Next(1, 853)),
            new (   Guid.NewGuid(),
                    "British Airways",
                    random.Next(90, 5000).ToString(),
                    new TimePlace("London, England",DateTime.Now.AddHours(random.Next(1, 15))),
                    new TimePlace("Vizzola-Ticino",DateTime.Now.AddHours(random.Next(4, 18))),
                        random.Next(1, 853)),
            new (   Guid.NewGuid(),
                    "Basiq Air",
                    random.Next(90, 5000).ToString(),
                    new TimePlace("Amsterdam",DateTime.Now.AddHours(random.Next(1, 21))),
                    new TimePlace("Glasgow, Scotland",DateTime.Now.AddHours(random.Next(4, 21))),
                        random.Next(1, 853)),
            new (   Guid.NewGuid(),
                    "BB Heliag",
                    random.Next(90, 5000).ToString(),
                    new TimePlace("Zurich",DateTime.Now.AddHours(random.Next(1, 23))),
                    new TimePlace("Baku",DateTime.Now.AddHours(random.Next(4, 25))),
                        random.Next(1, 853)),
            new (   Guid.NewGuid(),
                    "Adria Airways",
                    random.Next(90, 5000).ToString(),
                    new TimePlace("Ljubljana",DateTime.Now.AddHours(random.Next(1, 15))),
                    new TimePlace("Warsaw",DateTime.Now.AddHours(random.Next(4, 19))),
                        random.Next(1, 853)),
            new (   Guid.NewGuid(),
                    "ABA Air",
                    random.Next(90, 5000).ToString(),
                    new TimePlace("Praha Ruzyne",DateTime.Now.AddHours(random.Next(1, 55))),
                    new TimePlace("Paris",DateTime.Now.AddHours(random.Next(4, 58))),
                        random.Next(1, 853)),
            new (   Guid.NewGuid(),
                    "AB Corporate Aviation",
                    random.Next(90, 5000).ToString(),
                    new TimePlace("Le Bourget",DateTime.Now.AddHours(random.Next(1, 58))),
                    new TimePlace("Zagreb",DateTime.Now.AddHours(random.Next(4, 60))),
                        random.Next(1, 853))
    };
    entities.Flights.AddRange(flightsToSeed);
    entities.SaveChanges();
}

if (!entities.Airlines.Any())
{
    Airline[] airlinesToSeed = new Airline[]
    {
        new (
            "American Airlines",
            "American was founded more than 95 years ago and has a deep-rooted history leading the industry through innovation and firsts, including hiring the first Black U.S. commercial airline pilot, hiring the first female U.S. commercial airline pilot, launching the first loyalty program of any major carrier and becoming the first airline to introduce airport lounges.",
            "../../assets/images/american-airlines-logo.png"),
        new (
            "Deutsche BA",
            "DBA Luftfahrtgesellschaft mbH, founded as Delta Air and formerly branded  asDeutsche BA, was a low-cost airline headquartered on the grounds of Munich Airport in a building within the municipality of Hallbergmoos, Germany. It operated scheduled domestic and international services and also operated charter flights for tour operators in Europe and North Africa. It was acquired by Air Berlin in August 2006 when operating as dba, but continued to operate independently, marketed as Air Berlin (powered by dba)until being dissolved by its parent company Air Berlin on 30 November 2008.",
            "../../assets/images/DBA_logo.png"),
        new (
            "British Airways",
            "British Airways is a global airline, bringing people, places and diverse cultures closer together for more than 100 years. Serving our community and planet is at the heart of everything we do, and we look forward to sharing our exciting sustainability initiatives with you.",
            "../../assets/images/british-airways.png"),
        new (
            "Basiq Air",
            "Basiq Air is one Dutch former low-cost airline which was established in December 2000 and as a subsidiary of Transavia the predecessor of the transavia.com concept. Basiq Air offered cheap scheduled services to (according to the no-frills concept) to many popular destinations within Europe. Basiq Air mainly operated from Rotterdam, but also from Schiphol airport. All Basiq Air flights were bookable online. In six months after its foundation, 220 thousand tickets sold. Basiq Air introduced a new booking system in January 2003 allowing customers to arrange everything over the internet. This allowed Basiq Air to reduce some costs and compete better with the others low-cost airlineand then. But the establishment of Basiq Air also caused a lot of unrest among travel organizations. The low ticket prices that could be obtained via the internet ensured that potential customers of the travel agencies themselves booked the ticket and thus booked accommodation themselves, while the travel agencies were left with package tours. The then Transavia low cost daughter added new destinations to its timetable on March 30, 2003. Alicante, Palma de Mallorca, Seville, Milan, Naples, Pisa and Faro were added to the “old destinations” Barcelona, Madrid, Malaga, Bordeaux and Nice. The flight frequency to Madrid had also doubled from one to twice a day. After a difficult choice, on September 7, 2004, Transavia decided to reintegrate Basiq Air with the main brand Transavia and both Transavia and Basiq Air merged into the marketing concept \"transavia.com\", where flights were preferably sold through the website. As of January 1, 2005, all Basiq Air activities would be flown again under the name of Transavia. Transavia decided this because Transavia wanted to grow into a more developed travel company that could inspire a lot of confidence among the customers. Topman O. van den Brink: “We think it is time to merge the experience built up with Basiq Air into the strong and trusted brand Transavia. With this we are building a strong and unambiguous positioning of Transavia.”",
            "../../assets/images/basiq-air.png"),
        new (
            "BB Heliag",
            "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ac placerat vestibulum lectus mauris ultrices eros in cursus turpis. Arcu bibendum at varius vel pharetra vel turpis nunc eget. Tellus pellentesque eu tincidunt tortor aliquam nulla. Sagittis id consectetur purus ut faucibus pulvinar elementum integer. Ut tristique et egestas quis ipsum. Amet consectetur adipiscing elit ut aliquam purus sit amet luctus. Enim sit amet venenatis urna cursus. Nisl suscipit adipiscing bibendum est ultricies. Nulla facilisi nullam vehicula ipsum.",
            "../../assets/images/dummy-logo.png"),
        new (
            "Adria Airways",
            "Adria Airways was the largest airline in Slovenia, with more than 50 years of experience in providing charter and scheduled services. The Star Alliance member provided access to a global network of flights to 193 countries. Adria Airways was a Star Alliance since 2004, and a Lufthansa partner since 1996. The airline provided over 170 scheduled flights a week from Ljubljana, flying to European destinations including as Manchester, London, Brussels, Amsterdam, Frankfurt, Zurich, Munich, Malmo, Copenhagen, Stockholm, Lodz, Moscow, Istanbul, Tirane, Skopje, Pristina, Sarajevo, Maribor and Vienna. Adria Airways carrier had codeshare agreements with Aeroflot, Air India, Air Serbia, Austrian Airlines, LOT Polish Airlines, Lufthansa, Swiss International Air Lines and Turkish Airlines. Adria Airways fleet consisted of Airbus A319 and Bombardier CRJ900LR aircraft.",
            "../../assets/images/Adria_Airways.png"),
        new (
            "ABA Air",
            "ABA Air Group is committed to providing high quality aircraft parts to meet the needs of the aeronautical industry. ABA Air Group was founded in 2007. Today we are proud to say that we have a solid team and an impeccable track record, and have dedicated ourselves to serve our customers effectively and with a personalized attention. We are professionals in aeronautics, who through our passion for aviation and excellence, are firmly focused and determined to seek the innovation of our services. Our company is oriented towards excellent customer service, professional ethics, and human values while always complying with the highest standards of quality and innovation in the market. Every day our team works hard to continuously nurture our internal culture and to foster the external relationships that allow us to thrive as a business and deliver quality service to customers around the world.",
            "../../assets/images/aba-air-group.png"),
        new (
            "AB Corporate Aviation",
            "Travel the whole world aboard our private jets available for rental, reach your destinations final to your schedules, by direct flight, without expectations, in complete confidentiality and security.",
            "../../assets/images/dummy-logo.png"),
    };
    entities.Airlines.AddRange(airlinesToSeed);
    entities.SaveChanges();
}

app.UseCors(builder => builder
.WithOrigins("*")
.AllowAnyMethod()
.AllowAnyHeader()
);

app.UseSwagger().UseSwaggerUI();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
