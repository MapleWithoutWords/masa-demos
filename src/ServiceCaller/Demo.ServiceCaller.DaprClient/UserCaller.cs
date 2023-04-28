using Masa.BuildingBlocks.Service.Caller;
using Masa.Contrib.Service.Caller.DaprClient;

namespace Demo.ServiceCaller.DaprClient;

public class UserCaller : DaprCallerBase
{
    protected override string AppId { get; set; } = "{Replace-With-Your-Dapr-AppID}";

    public Task<string?> HelloAsync(string name)
        => Caller.GetAsync<string>($"/Hello", new { Name = name });

    protected override void UseDaprPost(MasaDaprClientBuilder masaHttpClientBuilder)
    {
        masaHttpClientBuilder.UseAuthentication();
    }

    protected override void ConfigMasaCallerClient(MasaCallerClient callerClient)
    {
    }
}
