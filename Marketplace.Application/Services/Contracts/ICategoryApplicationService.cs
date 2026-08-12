using Marketplace.Application.Dtos;

namespace Marketplace.Application.Services.Contracts
{
    public interface ICategoryApplicationService : IApplicationService
        <PostCategoryDto,PutCategoryDto,DeleteCategoryDto,GetByIdCategoryDto,GetAllCategoryDto>
    {
    }
}
