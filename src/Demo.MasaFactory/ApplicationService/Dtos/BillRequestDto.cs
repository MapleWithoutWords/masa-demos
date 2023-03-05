using System.ComponentModel.DataAnnotations;

namespace Demo.MasaFactory.ApplicationService.Dtos
{
    /// <summary>
    /// 物流面单请求dto
    /// </summary>
    public class BillRequestDto
    {
        public int SaleOrderId { get; set; }

        public int ShopId { get; set; }
        public string ShopName { get; set; }

        public int GoodId { get; set; }
        public string GoodName { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public string UserAddress { get; set; }


        //其它数据……
    }
}
