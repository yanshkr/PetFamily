using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Features.Volunteers.UpdatePetPosition;
public record UpdatePetPositionCommand(Guid VolunteerId, Guid PetId, int Position);