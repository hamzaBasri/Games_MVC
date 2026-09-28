using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Models.ViewModels
{
    public class GameListingVM
    {
        public GameListing GameListing { get; set; }
        public IEnumerable<SelectListItem> PlatformList { get; set; }
    }
}
