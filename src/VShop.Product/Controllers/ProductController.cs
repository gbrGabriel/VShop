using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("api/v1/product")]
public class ProductController(IServiceProduct serviceProduct) : AbstractApiBaseController
{
    private readonly IServiceProduct _serviceProduct = serviceProduct;
}
