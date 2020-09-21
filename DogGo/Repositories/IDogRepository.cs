﻿using DogGo.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;


namespace DogGo.Repositories
{
    public interface IDogRepository
    {
        List<Dog> GetAllDogs();
        Dog GetDogById(int id);

        void AddDog(Dog dog);
        void DeleteDog(int id);

        void UpdateDog(Dog dog);
        List<Dog> GetDogsByOwnerId(int OwnerId);
        Dog GetDogByDogIdOwnerId(int dogId, int ownerId);
    }
}
