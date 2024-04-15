using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prime.SlackWorkspaceAutomationPoC.Data.Entities;
using Prime.SlackWorkspaceAutomationPoC.Data.Extensions;
using Prime.SlackWorkspaceAutomationPoC.Domain.Abstractions.Repositories;
using Prime.SlackWorkspaceAutomationPoC.Domain.Dtos;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Prime.SlackWorkspaceAutomationPoC.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository
        where TEntity: BaseEntity
    {
        protected IMapper Mapper { get; }
        protected SlackWorkspaceAutomationPoCDbContext DbContext { get; }
        protected DbSet<TEntity> Items { get; }

        public BaseRepository(SlackWorkspaceAutomationPoCDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Items = DbContext.Set<TEntity>();
            Mapper = mapper;
        }

        public async Task<TDto> GetByIdAsync<TDto>(Guid id) 
            => Mapper.Map<TDto>(await Items.FirstOrDefaultAsync(x => x.Id == id));

        public async Task<PaginatedResult<TDto>> GetAllAsync<TDto>(int pageNumber, int pageSize, Expression<Func<TDto, bool>> filter = null)
        {
            var query = Items.AsQueryable();

            if (filter != null)
                query = query.Where(Mapper.Map<Expression<Func<TEntity, bool>>>(filter));

            return await Mapper.ProjectTo<TDto>(query).PaginateAsync(pageNumber, pageSize);
        }

        public async Task<TDto> CreateAsync<TDto>(TDto dto)
        {
            var entity = Mapper.Map<TEntity>(dto);

            await Items.AddAsync(entity);
            await DbContext.SaveChangesAsync();

            return Mapper.Map<TDto>(entity);
        }

        public async Task UpdateAsync<TDto>(TDto dto)
        {
            var entity = Mapper.Map<TEntity>(dto);

            DbContext.Set<TEntity>().Update(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await Items
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                DbContext.Set<TEntity>().Remove(entity);
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> HasAnyAsync(Guid id)
            => await Items.AnyAsync(x => x.Id == id);
    }
}
