using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {
        private readonly IWalkerRepository _walkerRepo;
        private readonly IWalkRepository _walkRepo;

        // ASP.NET will give us an instance of our Walker Repository. 
        // This is called "Dependency Injection"
        public WalkersController(IWalkerRepository walkerRepository, IWalkRepository walkRepository)
        {
            _walkerRepo = walkerRepository;
            _walkRepo = walkRepository;
        }
        // GET: Walkers
        public IActionResult Index()
        {
            List<Walker> walkers = _walkerRepo.GetAllWalkers();

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
    }
}
