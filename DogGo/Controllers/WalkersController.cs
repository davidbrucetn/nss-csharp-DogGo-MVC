using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DogGo.Controllers
{
    
    public class WalkersController : Controller
    {
        private readonly IWalkerRepository _walkerRepo;
        private readonly IWalkRepository _walkRepo;
        private readonly IOwnerRepository _ownerRepo;

        // ASP.NET will give us an instance of our Walker Repository. 
        // This is called "Dependency Injection"
        public WalkersController(IWalkerRepository walkerRepository, IWalkRepository walkRepository, IOwnerRepository ownerRepository)
        {
            _walkerRepo = walkerRepository;
            _walkRepo = walkRepository;
            _ownerRepo = ownerRepository;
        }
        // GET: Walkers
        public IActionResult Index()

        {

            int ownerId = GetCurrentUserId();
            Owner owner = _ownerRepo.GetOwnerById(ownerId);
            List<Walker> AllWalkers = _walkerRepo.GetAllWalkers();
            if (owner == null)
            {
                return View(AllWalkers);
            }
            List<Walker> walkers = AllWalkers.FindAll(walker => walker.NeighborhoodId == owner.NeighborhoodId);

            return View(walkers);
        }

        // GET: Walkers/Details/5
        public ActionResult Details(int id)
        {

            Walker walker = _walkerRepo.GetWalkerById(id);

            List<Walk> walks = _walkRepo.GetAllWalks(id);
            int TotalTime = 0;
            foreach(Walk walk in walks) { TotalTime += walk.Duration; };
            TimeSpan ts = TimeSpan.FromSeconds(TotalTime);




            WalkerProfileViewModel vm = new WalkerProfileViewModel()
            {
                Walker = walker,

                Walks = walks,
                TotalTime = TotalTime,
                TS = ts
            
            };

            return View(vm);
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id != null)
            {
                return int.Parse(id);
            }
            return 0;
        }
    }
}
