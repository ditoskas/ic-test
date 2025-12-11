namespace IcTest.Shared.ApiResponses
{
    public class ApiPaginatedResponse<T>(PaginatedResult<T> payload) :ApiResponse<PaginatedResult<T>>(payload)
    {
    }
}
