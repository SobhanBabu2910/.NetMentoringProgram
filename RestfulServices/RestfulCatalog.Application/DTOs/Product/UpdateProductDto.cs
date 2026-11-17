using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulCatalog.Application.DTOs.Product
{
    public record UpdateProductDto(
    string Name,
    decimal Price,
    int Amount
    );
}
