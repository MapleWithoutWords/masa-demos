using Masa.BuildingBlocks.Data;
using Microsoft.Extensions.Options;

namespace Demo.MasaFactory.ApplicationService.MessageConvertFactory
{
    public class BillMessageConvertFactory : MasaFactoryBase<IBillMessageConvert, MasaRelationOptions<IBillMessageConvert>>
    {
        protected override string DefaultServiceNotFoundMessage => "Default BillMessageConvert not found, you need to add it";

        protected override string SpecifyServiceNotFoundMessage => "Please make sure you have used [{0}] BillMessageConvert, it was not found";

        protected override MasaFactoryOptions<MasaRelationOptions<IBillMessageConvert>> FactoryOptions => _optionsMonitor.CurrentValue;


        private readonly IOptionsMonitor<MasaFactoryOptions<MasaRelationOptions<IBillMessageConvert>>> _optionsMonitor;

        public BillMessageConvertFactory(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _optionsMonitor = serviceProvider.GetRequiredService<IOptionsMonitor<MasaFactoryOptions<MasaRelationOptions<IBillMessageConvert>>>>();
        }
    }
}
