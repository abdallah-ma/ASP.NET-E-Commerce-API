using AutoMapper;
using ProductService.Models;
using ProductService.Dtos;

namespace ProductService.Helpers
{
    public class ProductUrlResolver : IValueResolver<Models.Product , ProductToReturnDto,string>
    {

        public readonly IConfiguration Configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string Resolve(Models.Product Source,ProductToReturnDto Dest,string destmember,ResolutionContext context)
        {

            if (!string.IsNullOrEmpty(Source.PictureUrl))
            {
                string url = $"{Configuration["ApiBaseUrl"]}{Source.PictureUrl}";
                return $"{Configuration["ApiBaseUrl"]}{Source.PictureUrl}";
            }
            return String.Empty;
        }

    }
}
