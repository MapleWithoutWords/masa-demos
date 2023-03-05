using Demo.MasaFactory.ApplicationService.Dtos;

namespace Demo.MasaFactory.ApplicationService.MessageConvertImpl
{
    public class ZTBillMessageConvert : IBillMessageConvert
    {
        public async Task<string> ConvertRequestMessage(BillRequestDto dto)
        {
            return $"ZT Request Message";
        }

        public async Task<BillResponseDto> ConvertResponseMessage(string dto)
        {
            return new BillResponseDto
            {
                SaleOrderId = 1,
                BillNo = $"ZT{DateTime.Now.ToString("yyyyMMdd")}0001"
            };
        }
    }
}
