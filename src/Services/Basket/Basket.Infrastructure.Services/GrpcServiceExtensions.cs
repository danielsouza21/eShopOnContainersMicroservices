using Discount.Grpc.Protos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Basket.Infrastructure.Services
{
    public static class GrpcServiceExtensions
    {
        public static void ConfigureDiscountGrpcService(this IServiceCollection services, string gprcDiscountUrl)
        {
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o => o.Address = new Uri(gprcDiscountUrl));
        }
    }
}
