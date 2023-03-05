using Demo.MasaFactory.ApplicationService.MessageConvertImpl;
using Masa.BuildingBlocks.Data;

namespace Demo.MasaFactory.ApplicationService.MessageConvertFactory
{
    public static class IServiceCollectionExtensions
    {
        public static void AddBillMessageConvertServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(JDBillMessageConvert));
            services.AddTransient(typeof(SFBillMessageConvert));
            services.AddTransient(typeof(ZTBillMessageConvert));
            services.Configure<MasaFactoryOptions<MasaRelationOptions<IBillMessageConvert>>>(opt =>
            {
                opt.AddService("JD", typeof(JDBillMessageConvert))
                .AddService("SF", typeof(SFBillMessageConvert))
                .AddService("ZT", typeof(ZTBillMessageConvert));
            });
            services.AddSingleton<IMasaFactory<IBillMessageConvert>, BillMessageConvertFactory>();
        }

        public static MasaFactoryOptions<MasaRelationOptions<IBillMessageConvert>> AddService(this MasaFactoryOptions<MasaRelationOptions<IBillMessageConvert>> factoryOptions, string name, Type implType)
        {
            if (factoryOptions.Options.Any(e => e.Name == name))
            {
                return factoryOptions;
            }


            var relationOptions = new MasaRelationOptions<IBillMessageConvert>(name, serviceProvider =>
            {
                return (IBillMessageConvert)serviceProvider.GetRequiredService(implType);
            });

            factoryOptions.Options.Add(relationOptions);

            return factoryOptions;
        }
    }
}
