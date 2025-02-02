using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Features.Files.UploadFile.Contracts;
public record UploadFileCommand(
    Stream Stream,
    string FileName);
