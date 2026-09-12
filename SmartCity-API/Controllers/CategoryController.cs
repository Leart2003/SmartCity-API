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
        /// <summary>
        /// Updates a category 
        /// </summary>
        /// <param name="id">The id of the category to be updated</param>
        /// <returns>If category is null, returns not found.If exists returns succesfully updated</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
                return NotFound($"Category with id {id} was not found.");

            return Ok(_mapper.Map<CategoryDto>(category));
        }
        /// <summary>
        /// Creates a category
        /// </summary>
        /// <param name="createDto">Creates a category by the given info of createDto</param>
        /// <returns>Returns the created category</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryDto>> Create([FromBody] CreateCategoryDto createDto)
        {
            var category = _mapper.Map<Category>(createDto);
            var created = await _categoryRepository.AddAsync(category);

            var dto = _mapper.Map<CategoryDto>(created);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, dto);
        }
        /// <summary>
        /// Updates a category
        /// </summary>
        /// <param name="id">Updates a category</param>
        /// <param name="updateDto">Updates category based on updateDto</param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateCategoryDto updateDto)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Category with id {id} was not found.");

            _mapper.Map(updateDto, existing);

            await _categoryRepository.UpdateAsync(existing);
            return NoContent();
        }
        /// <summary>
        /// Deletes a category 
        /// </summary>
        /// <param name="id">Deletes the category by the given Id</param>
        /// <returns>Returns no content after the category is updated</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _categoryRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Category with id {id} was not found.");

            await _categoryRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
