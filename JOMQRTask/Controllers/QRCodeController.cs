using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using QRCoder;
using System.Drawing;
using System.IO;


namespace JOMQRTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QRCodeController : ControllerBase
    {

        // get array of bits for an image
        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            //get Byte array from image path
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }


        ///api/QRCode/GenerateQRCode?QRCodeText=wellcom


        //api fun for generate text to qr**********************
        [HttpGet("GenerateQRCode")]
        public async Task<ActionResult> GenerateQRCode(String QRCodeText)
        {

            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData = _qrCode.CreateQrCode(QRCodeText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(_qrCodeData);
            Image qrCodeImage = qrCode.GetGraphic(20);

            var bytes = ImageToByteArray(qrCodeImage);

            return File(bytes, "image/bmp");

        }
    }
}
