#if NET6_0_OR_GREATER

using Microsoft.AspNetCore.Mvc;

namespace BitNebula.Developkit.ApiResult.Extensions;

public static class ApiResultExtension
{
    public static OkObjectResult Ok(this Developkit.ApiResult.ApiResult result)
    {
        return new OkObjectResult(result);
    }

    public static BadRequestObjectResult Bad(this Developkit.ApiResult.ApiResult result)
    {
        return new BadRequestObjectResult(result);
    }
}

#endif