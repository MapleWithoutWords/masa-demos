using Demo.MasaFactory.ApplicationService.Dtos;

namespace Demo.MasaFactory.ApplicationService
{
    public interface IBillMessageConvert
    {
        public Task<string> ConvertRequestMessage(BillRequestDto dto);
        public Task<BillResponseDto> ConvertResponseMessage(string dto);
    }
}
