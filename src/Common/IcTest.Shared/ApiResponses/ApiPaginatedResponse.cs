using AmegaCore.Pagination;

namespace AmegaCore.ApiResponses
{
    public class ApiPaginatedResponse<T>(PaginatedResult<T> payload) :ApiResponse<PaginatedResult<T>>(payload)
    {
    }
}
