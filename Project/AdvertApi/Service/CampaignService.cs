using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AdvertApi.DTOs.Requests;
using AdvertApi.DTOs.Responses;
using AdvertApi.Exceptions;
using AdvertApi.Handlers;
using AdvertApi.Models;
using Project_AdvertApi.DTOs.Responses;
using Project_AdvertApi.Exceptions;
using AdvertApi.DTOs.Responses.ModelsResponse;

namespace AdvertApi.Service
{
    public class CampaignService : ICampaignService
    {
        private readonly CampaignsDbContext _dbcontext;
        public IConfiguration _Configuration { get; set; }

        public CampaignService(CampaignsDbContext dbcontext, IConfiguration Configuration)
        {
            _dbcontext = dbcontext;
            _Configuration = Configuration;
        }

        //==========Zadanie 4
        public async Task<RegistratedClientResponse> RegistrateClientAsync(RegistrateNewClientRequest newClient)
        {
            var isClientExists = await _dbcontext.Clients.AnyAsync(c => c.Login.Equals(newClient.Login));

            if (isClientExists)
            {
                throw new ClientHasAlreadyExistsException($"Client with login={newClient.Login} has already exists");
            }

            var salt = PasswordHashingHandler.CreateSalt();
            var client = new Client
            {
                FirstName = newClient.FirstName,
                LastName = newClient.LastName,
                Email = newClient.Email,
                Phone = newClient.Phone,
                Login = newClient.Login,
                Password = PasswordHashingHandler.CreateHash(newClient.Password, salt), 
                Salt = salt             
            };

            await _dbcontext.AddAsync(client);
            await _dbcontext.SaveChangesAsync();

            var cl = await _dbcontext.Clients
                .Where(c => c.Login.Equals(newClient.Login))
                .SingleOrDefaultAsync();

            return new RegistratedClientResponse
            {
                FirstName = cl.FirstName,
                LastName = cl.LastName,
                Email = cl.Email,
                Phone = cl.Phone,
                Login = cl.Login,
            };
        }

        //==========Zadanie 6
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var isClientExists = await _dbcontext.Clients.AnyAsync(c => c.Login.Equals(request.Login));

            if (!isClientExists)
            {
                throw new ClientDoesNotExistsException($"Client with login={request.Login} does not exists");
            }

            var client = await _dbcontext.Clients.SingleOrDefaultAsync(c => c.Login.Equals(request.Login));

            if (!PasswordHashingHandler.Validate(request.Password, client.Password, client.Salt))
            {
                throw new IncorrectPasswordException("Incorrect Password");
            }

            string refreshToken = Guid.NewGuid().ToString();
            client.RefreshToken = refreshToken;

            _dbcontext.Update(client);
            await _dbcontext.SaveChangesAsync();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, client.IdClient.ToString()),
                new Claim(ClaimTypes.Name, client.Login),
                new Claim(ClaimTypes.Role, "client")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jasuaujsuhdyeyrjbsiweuuwhe7t363nuwdnu"));
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "s19504",
                audience: "Clients",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            return new LoginResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken
            };
        }

        //==========Zadanie 5
        public async Task<LoginResponse> CreateNewRefreshAndAccessTokenAsync(string refreshToken)
        {
            var isClientExists = await _dbcontext.Clients.AnyAsync(c => c.RefreshToken.Equals(refreshToken));

            if (!isClientExists)
            {
                throw new RefreshTokenDoesNotExistsException("Refresh-Token does not exists");
            }

            var client = await _dbcontext.Clients.SingleOrDefaultAsync(c => c.RefreshToken.Equals(refreshToken));

            string newRefreshToken = Guid.NewGuid().ToString();
            client.RefreshToken = newRefreshToken;

            _dbcontext.Update(client);
            await _dbcontext.SaveChangesAsync();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, client.IdClient.ToString()),
                new Claim(ClaimTypes.Name, client.Login),
                new Claim(ClaimTypes.Role, "client")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jasuaujsuhdyeyrjbsiweuuwhe7t363nuwdnu"));
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "s19504",
                audience: "Clients",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            return new LoginResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = newRefreshToken
            };
        }

        //==========Zadanie 7
        public async Task<IEnumerable<CampaignResponse>> GetCampaignsAsync()
        {
            var campaings = await _dbcontext.Campaings
                                            .OrderByDescending(c => c.StartDate)
                                            .ToListAsync();

            List<CampaignResponse> campaignsResponse = new List<CampaignResponse>();

            foreach (Campaing campaing in campaings)
            {
                var client = await _dbcontext.Clients
                                         .Where(c => c.IdClient == campaing.IdClient)
                                         .SingleOrDefaultAsync();


                var banners = await _dbcontext.Banners
                                              .Where(b => b.IdCampaing == campaing.IdCampaing)
                                              .ToListAsync();

                List<BannerModelResponse> bannersModelResponse = new List<BannerModelResponse>();
                foreach (Banner banner in banners)
                {
                    bannersModelResponse.Add(new BannerModelResponse
                    {
                        Name = banner.Name,
                        Price = banner.Price,
                        Area = banner.Area
                    });
                }

                campaignsResponse.Add(new CampaignResponse
                {
                    IdCampaing = campaing.IdCampaing,
                    StartDate = campaing.StartDate,
                    EndDate = campaing.EndDate,
                    PricePerSquareMeter = campaing.PricePerSquareMeter,
                    FromIdBuilding = campaing.FromIdBuilding,
                    ToIdBuilding = campaing.ToIdBuilding,

                    Banners = bannersModelResponse,

                    FirstName = campaing.Client.FirstName,
                    LastName = campaing.Client.LastName,
                    Email = campaing.Client.Email,
                    Phone = campaing.Client.Phone
                });
            }

            return campaignsResponse;
        }

        //==========Zadanie 8
        public async Task<NewlyCreatedCampaignResponse> CreateCampaignAsync(CreateNewCampaignRequest request)
        {
            if (await _dbcontext.Buildings.CountAsync() < 2)
            {
                throw new NotEnougBuildingsInTheDatabaseException("Not enough buildings in the database");
            }

            var isClientExists = await _dbcontext.Clients.AnyAsync(c => c.IdClient == request.IdClient);

            if (!isClientExists)
            {
                throw new ClientDoesNotExistsException($"Client with id={request.IdClient} does not exists");
            }

            var isBuildingFromBuildingExists = await _dbcontext.Buildings.AnyAsync(b => b.IdBuilding == request.FromIdBuilding);

            if (!isBuildingFromBuildingExists)
            {
                throw new BuildingDoesNotExistsException($"Building with an id={request.FromIdBuilding} does not exists");
            }

            var isBuildingToBuildingExists = await _dbcontext.Buildings.AnyAsync(b => b.IdBuilding == request.ToIdBuilding);

            if (!isBuildingFromBuildingExists)
            {
                throw new BuildingDoesNotExistsException($"Building with an id={request.ToIdBuilding} does not exists");
            }

            if (request.StartDate > request.EndDate)
            {
                throw new WrongDateException($"Start date={request.StartDate} can not be less than End date={request.EndDate}");
            }

            var fromBuilding = await _dbcontext.Buildings
                                               .Where(b => b.IdBuilding == request.FromIdBuilding)
                                               .SingleOrDefaultAsync();

            var toBuilding = await _dbcontext.Buildings
                                             .Where(b => b.IdBuilding == request.ToIdBuilding)
                                             .SingleOrDefaultAsync();

            if (!fromBuilding.City.Equals(toBuilding.City))
            {
                throw new BuildingsInDifferentCitiesException($"Building FromBuilding with an id={request.FromIdBuilding} and building ToBuilding with an id={request.ToIdBuilding} are located on different cities");
            }

            if (!fromBuilding.Street.Equals(toBuilding.Street))
            {
                throw new BuildingsOnDifferentStreetsException($"Building FromBuilding with an id={request.FromIdBuilding} and building ToBuilding with an id={request.ToIdBuilding} are located on different streets");
            }

            var campaign = new Campaing
            {
                IdClient = request.IdClient,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                PricePerSquareMeter = request.PricePerSquareMeter,
                FromIdBuilding = request.FromIdBuilding,
                ToIdBuilding = request.ToIdBuilding
            };

            await _dbcontext.AddAsync(campaign);
            await _dbcontext.SaveChangesAsync();

            decimal[] areas = CalculateArea(fromBuilding, toBuilding);

            var banner1 = new Banner
            {
                Name = "Banner1",
                Price = areas[0] * request.PricePerSquareMeter,
                IdCampaing = campaign.IdCampaing,
                Area = areas[0]
            };

            var banner2 = new Banner
            {
                Name = "Banner2",
                Price = areas[1] * request.PricePerSquareMeter,
                IdCampaing = campaign.IdCampaing,
                Area = areas[1]
            };

            await _dbcontext.AddRangeAsync(banner1, banner2);
            await _dbcontext.SaveChangesAsync();

            return new NewlyCreatedCampaignResponse
            {
                IdCampaing = campaign.IdCampaing,
                IdClient = campaign.IdClient,
                StartDate = campaign.StartDate,
                EndDate = campaign.EndDate,
                PricePerSquareMeter = campaign.PricePerSquareMeter,
                FromIdBuilding = campaign.FromIdBuilding,
                ToIdBuilding = campaign.ToIdBuilding,

                Banner1 = banner1,
                Banner2 = banner2,

                TotalPrice = (banner1.Area + banner2.Area) * request.PricePerSquareMeter
            };
        }

        private decimal[] CalculateArea(Building fromBuilding, Building toBuilding)
        {
            decimal banner1Area, banner2Area;
            decimal[] areas = new decimal[2];

            if (fromBuilding.Height > toBuilding.Height)
            {
                banner1Area = fromBuilding.Height;
                var buildingDifference = Math.Abs(fromBuilding.StreetNumber - toBuilding.StreetNumber);
                banner2Area = toBuilding.Height * buildingDifference;
            }
            else
            {
                banner1Area = toBuilding.Height;
                var buildingDifference = Math.Abs(fromBuilding.StreetNumber - toBuilding.StreetNumber);
                banner2Area = fromBuilding.Height * buildingDifference;
            }

            areas[0] = banner1Area;
            areas[1] = banner2Area;

            return areas;
        }
    }
}
