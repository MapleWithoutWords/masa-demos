using Demo.MasaFactory.ApplicationService.Dtos;

namespace Demo.MasaFactory.ApplicationService.MessageConvertImpl
{
    public class SFBillMessageConvert : IBillMessageConvert
    {
        public async Task<string> ConvertRequestMessage(BillRequestDto dto)
        {
            return $"SF Request Message";
        }

        public async Task<BillResponseDto> ConvertResponseMessage(string dto)
        {
            return new BillResponseDto
            {
                SaleOrderId = 1,
                BillNo = $"SF{DateTime.Now.ToString("yyyyMMdd")}0001"
            };
        }
    }
}
