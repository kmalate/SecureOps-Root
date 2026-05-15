using SecureOps.Application.DTO;
using SecureOps.Application.Interfaces;
using SecureOps.Domain.Entities;
using SecureOps.Domain.Enums;
using System.Text.Json;

namespace SecureOps.Application.Services
{
    public class FieldDefinitionService : IFieldDefinitionService
    {
        private readonly IFieldDefinitionRepository _repository;

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public FieldDefinitionService(IFieldDefinitionRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<FieldDefinitionDTO> AddAsync(FieldDefinitionDTO dto, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var entity = new FieldDefinition
            {
                Label = dto.Label,
                FieldTypeId = dto.FieldTypeId,
                Options = dto.Options != null ? JsonSerializer.Serialize(dto.Options) : string.Empty
            };

            var added = await _repository.AddAsync(entity, cancellationToken);

            return new FieldDefinitionDTO
            {
                Id = added.Id,
                Label = added.Label,
                FieldTypeId = added.FieldTypeId,
                Options = !string.IsNullOrEmpty(added.Options)
                    ? JsonSerializer.Deserialize<object>(added.Options) ?? new { }
                    : new { }
            };
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<FieldDefinitionDTO> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            return entity is null
                ? throw new KeyNotFoundException($"FieldDefinition with id {id} was not found.")
                : new FieldDefinitionDTO
            {
                Id = entity.Id,
                Label = entity.Label,
                FieldTypeId = entity.FieldTypeId,
                Options = !string.IsNullOrEmpty(entity.Options)
                    ? JsonSerializer.Deserialize<object>(entity.Options) ?? new { }
                    : new { }
            };
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<IEnumerable<FieldDefinitionListDTO>> GetAllAsync(FieldTarget fieldTarget, CancellationToken cancellationToken = default)
        {
            var entities = await _repository.GetAllAsync(fieldTarget, cancellationToken);
            return entities.Select(e => new FieldDefinitionListDTO
            {
                Id = e.Id,
                Label = e.Label,
                Options = !string.IsNullOrEmpty(e.Options)
                    ? JsonSerializer.Deserialize<object>(e.Options) ?? new { }
                    : new { },
                FieldType = e.FieldType != null ? new FieldTypeDTO
                {
                    Id = e.FieldType.Id,
                    Name = e.FieldType.Name
                } : null,
                DisplayOrder = e.DisplayOrder
            }).ToList();
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task UpdateAsync(FieldDefinitionDTO dto, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var entity = await _repository.GetByIdAsync(dto.Id, cancellationToken) ?? throw new KeyNotFoundException($"FieldDefinition with id {dto.Id} was not found.");
            entity.Label = dto.Label;
            entity.FieldTypeId = dto.FieldTypeId;
            entity.Options = dto.Options != null ? JsonSerializer.Serialize(dto.Options) : string.Empty;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc />
        /// </summary>
        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _repository.ExistsAsync(id, cancellationToken);
        }
    }
}
