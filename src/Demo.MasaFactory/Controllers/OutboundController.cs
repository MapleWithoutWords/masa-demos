using Demo.MasaFactory.ApplicationService;
using Demo.MasaFactory.ApplicationService.Dtos;
using Demo.MasaFactory.ApplicationService.MessageConvertFactory;
using Masa.BuildingBlocks.Data;
using Microsoft.AspNetCore.Mvc;

namespace Demo.MasaFactory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutboundController : ControllerBase
    {
        private readonly IMasaFactory<IBillMessageConvert> _billMessageConvertFactory;

        public OutboundController(IMasaFactory<IBillMessageConvert> billMessageConvertFactory)
        {
            _billMessageConvertFactory = billMessageConvertFactory;
        }

        /// <summary>
        /// 获取物流面单
        /// </summary>
        /// <param name="saleOrderId">销售订单id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetBillAsync(int saleOrderId = 0)
        {
            //TODO:根据订单id获取订单信息
            var saleOrderInfo = new { GoodId = 1, GoodName = "星巴克咖啡豆", ShopId = 1, ShopName = "星巴克专卖店", UserId = 1, UserName = "张三", UserPhone = "18888888888", UserAddress = "新疆省克拉玛依市" };
            string logisticsProviderCode = "SF";
            if (saleOrderId == 1)
            {
                logisticsProviderCode = "ZT";
            }
            else if (saleOrderId == 2)
            {
                logisticsProviderCode = "JD";
            }

            var billMessageConvert = _billMessageConvertFactory.Create(logisticsProviderCode);
            var requestMessage = await billMessageConvert.ConvertRequestMessage(new BillRequestDto
            {
                GoodId = saleOrderInfo.GoodId,
                GoodName = saleOrderInfo.GoodName,
                SaleOrderId = saleOrderId,
                ShopId = saleOrderInfo.ShopId,
                ShopName = saleOrderInfo.ShopName,
                UserAddress = saleOrderInfo.UserAddress,
                UserId = saleOrderInfo.UserId,
                UserName = saleOrderInfo.UserName,
                UserPhone = saleOrderInfo.UserPhone,
            });
            //TODO:发送http请求
            string responseStr = "";
            var responseMessage = await billMessageConvert.ConvertResponseMessage(responseStr);

            return $"Request Message:【{requestMessage}】。Response Message：【{responseMessage.BillNo}】";
        }
    }
}
