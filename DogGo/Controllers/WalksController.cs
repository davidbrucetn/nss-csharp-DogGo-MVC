using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogGo.Controllers
{
    public class WalksController : Controller
    {

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        private readonly IWalkerRepository _walkerRepo;
        private readonly IWalkRepository _walkRepo;
        private readonly IOwnerRepository _ownerRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;
        private readonly IDogRepository _dogRepo;

        // ASP.NET will give us an instance of our Walker Repository. 
        // This is called "Dependency Injection"
        public WalksController(IDogRepository dogRepository, IWalkerRepository walkerRepository, IWalkRepository walkRepository, IOwnerRepository ownerRepository, INeighborhoodRepository neighborhoodRepository)
        {
            _walkerRepo = walkerRepository;
            _walkRepo = walkRepository;
            _ownerRepo = ownerRepository;
            _neighborhoodRepo = neighborhoodRepository;
            _dogRepo = dogRepository;
        }

        // GET: Walkers
        public IActionResult Index()

        {
            int ownerId = GetCurrentUserId();
            Owner owner = _ownerRepo.GetOwnerById(ownerId);
            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(ownerId);
            List<Walk> allWalks = new List<Walk>();
            foreach(Dog dog in dogs)
            {
                List<Walk> dogWalks = _walkRepo.GetAllWalksDog(dog.Id);
                foreach(Walk walk in dogWalks)
                {
                    allWalks.Add(walk);
                }
            }

            
            return View(allWalks);
        }

        // GET: WalksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Owners/Create
        public ActionResult Create()
        {
            int ownerId = GetCurrentUserId();
            Owner owner = _ownerRepo.GetOwnerById(ownerId);
            List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(owner.NeighborhoodId);
            List<Dog> dogs = _dogRepo.GetDogsByOwnerId(ownerId);

            WalkFormViewModel vm = new WalkFormViewModel()
            {
                Walk = new Walk(),
                Walkers = walkers,
                Dogs = dogs
            };

            return View(vm);
        }

        // POST: Owners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Walk walk)   //Method overloading, two identical methods with different parameters
        {
            try
            {
                _walkRepo.AddWalk(walk);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(walk);
            }
        }

        // GET: WalksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Create(Walker walker)
        {
            throw new NotImplementedException();
        }

      
    }
}
