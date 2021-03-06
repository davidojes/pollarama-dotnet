using ArtistAwards.Data;
using DotNetAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArtistAwards.Services
{
  public class ArtistService
  {
    public ArtistService(IWebHostEnvironment webHostEnvironment, AppDbContext context)
    {
      WebHostEnvironment = webHostEnvironment;
      ArtistContext = context;
    }

    public IWebHostEnvironment WebHostEnvironment { get; }
    private AppDbContext ArtistContext;
    private IEnumerable<Artist> Artists;

    private string JsonFileName
    {
      get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "artists.json"); }
    }

    public IEnumerable<Artist> GetArtists()
    {
      Artists = ArtistContext.Artists;
      return Artists;
    }

    public async Task<Artist> GetArtist(int id)
    {
      Artist artist = await ArtistContext.Artists.FindAsync(id) ;
      return artist;
    }

    //public async Task<List<Artist>> GetArtists()
    //{
    //  Artists = await ArtistContext.Artists.ToListAsync();
    //  return Artists;
    //}

    public async Task<Artist> CreateArtist(Artist artist)
    {
      ArtistContext.Add(artist);
      await ArtistContext.SaveChangesAsync();

      return artist;
    }

    public async Task VoteAsync(int id)
    {

      Artist artistToVote = await ArtistContext.Artists.FindAsync(id);
      artistToVote.Votes += 1;

      await ArtistContext.SaveChangesAsync();
    }

  }
}
