using Demo.MasaFactory.ApplicationService.Dtos;

namespace Demo.MasaFactory.ApplicationService.MessageConvertImpl
{
    public class JDBillMessageConvert : IBillMessageConvert
    {
        public async Task<string> ConvertRequestMessage(BillRequestDto dto)
        {
            return $"JD Request Message";
        }

        public async Task<BillResponseDto> ConvertResponseMessage(string dto)
        {
            return new BillResponseDto
            {
                SaleOrderId = 1,
                BillNo = $"JD{DateTime.Now.ToString("yyyyMMdd")}0001"
            };
        }
    }
}
