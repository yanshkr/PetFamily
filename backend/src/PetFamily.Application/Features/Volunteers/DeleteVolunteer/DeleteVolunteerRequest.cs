﻿using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.Features.Volunteers.DeleteVolunteer;
public record DeleteVolunteerRequest(
    VolunteerId Id,
    bool IsSoftDelete
    );
