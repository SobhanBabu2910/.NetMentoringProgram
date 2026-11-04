using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulCatalog.Application.DTOs.Product
{
    public record CreateProductDto(
    string Name,
    decimal Price,
    int Amount,
    int CategoryId);
}
