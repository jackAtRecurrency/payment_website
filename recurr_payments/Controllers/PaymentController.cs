using System.Diagnostics;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace recurr_payments.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(ILogger<PaymentController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("GetPaymentLink")]
    public IActionResult GetPaymentLink([FromQuery] Dictionary<string, string> queryStringParams)
    {
        // build the query string
        var queryString = new System.Text.StringBuilder();
        var orderNo = "";
        var recurrency = "";
        foreach (var kvp in queryStringParams)
        {
            if(kvp.Key == "order_no")
            {
                orderNo = kvp.Value;
            }

            if(kvp.Key == "recurrency")
            {
                recurrency = kvp.Value;
            }

            if (queryString.Length > 0)
            {
                queryString.Append("&");
            }
            queryString.Append(WebUtility.UrlEncode(kvp.Key));
            queryString.Append("=");
            queryString.Append(WebUtility.UrlEncode(kvp.Value));
        }

        if (recurrency != "awesome") return BadRequest("Invalid Request");

        // construct the URL with query string
        var baseUrl = $"https://05c5-99-55-160-105.ngrok-free.app/cwc-play/payments/order/{orderNo}";
        var urlWithQueryString = $"{baseUrl}?{queryString}";
        int x = 50;
        int y = 20;
        int width = 750;
        int height = 850;

        string arguments = $"--app=\"{urlWithQueryString}\" --window-position={x},{y} --window-size={width},{height} --disable-extensions --disable-plugins-discovery --disable-popup-blocking --hide-scrollbars";
        return Ok(arguments);

        // string paymentLink = $"https://d484-2408-8207-25e8-8570-70d9-db52-dac3-4cca.jp.ngrok.io/cwc-play/payments/order/{orderId}?customerId={customerId}&companyId={companyId}&totalAmt={totalAmount}&taxAmt={taxAmount}";
        // return Ok(responseString.ToString());
    }
}
