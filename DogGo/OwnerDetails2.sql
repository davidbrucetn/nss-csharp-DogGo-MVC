SELECT o.Id, o.[Name] as OwnerName, o.Email, o.Address, o.Phone, o.NeighborhoodId,
                                d.Id as DogId, d.Name, d.Breed, d.Notes, d.ImageUrl
                        FROM Owner o
                        JOIN Dog d on o.Id = d.OwnerId