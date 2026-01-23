namespace Application.Interfaces {
    public interface IGenericService<TEntity, TReadDto, TCreateDto, TUpdateDto> where TEntity : class  {
        Task<IEnumerable<TReadDto>> GetAllAsync();
        Task<TReadDto?> GetByIdAsync(Guid id);
        Task<TReadDto> CreateAsync(TCreateDto datum);
        Task<TReadDto> UpdateAsync(Guid id, TUpdateDto datum);
        Task<bool> DeleteAsync(Guid id);
        Task<TReadDto> RestoreAsync(Guid id);
    }
}
