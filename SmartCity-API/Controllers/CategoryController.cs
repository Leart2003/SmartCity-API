using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SmartCity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Get all existing categories
        /// </summary>
        /// <returns>If any category, returns all categories</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return NotFound($"Category with id {id} was not found.");

            return Ok(_mapper.Map<CategoryDto>(category));
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryDto createDto)
        {
            var category = _mapper.Map<Category>(createDto);
            var created = await _categoryRepository.AddAsync(category);

            var dto = _mapper.Map<CategoryDto>(created);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, dto);
        }
    }
}
