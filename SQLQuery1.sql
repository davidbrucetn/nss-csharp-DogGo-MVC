SELECT wk.Id, wk.Date, wk.Duration, wk.DogId, wk.WalkerId, d.Name as DogName, d.Breed, d.Notes, d.ImageUrl, d.OwnerId,
                            o.Name as OwnerName, o.Email, o.Address, O.NeighborhoodId, O.Phone
                        FROM Walks wk
                            Join Dog d on d.Id = wk.DogId
                            Join Owner o on o.Id = d.OwnerId
                            where wk.WalkerId=1