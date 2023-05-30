using API.Contracts;
using API.Models;
using API.Repositories;
using API.ViewModels.Educations;
using API.ViewModels.Universities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EducationController : BaseController<Education, EducationVM>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper<Education, EducationVM> _mapper;
    public EducationController(IEducationRepository education, IMapper<Education, EducationVM> mapper) : base(education, mapper)
    {
        _educationRepository = education;
        _mapper = mapper;
    }

}