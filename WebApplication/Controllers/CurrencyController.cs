using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class CurrencyController : ApiController
    {
        int marginLeft = 35;
        int marginRight = 15;
        int marginDown = 20;
        int marginUp = 10;
        int YHorizontal;
        int lenghtHorizontal;
        int xMax;
        int lenghtVertical;

        //чтение данных
        Currency[] currencys = new Currency[]
        {
            new Currency { Id = 1, Date = "13.02", Category = "usd", Value = 56 },
            new Currency { Id = 2, Date = "14.02", Category = "usd", Value = 56.5 },
            new Currency { Id = 3, Date = "15.02", Category = "usd", Value = 55.5 }
        };

        public IEnumerable<Currency> GetAllCurrencys()
        {
            return currencys;
        }

        public IHttpActionResult GetCurrency(int id)
        {
            var currency = currencys.FirstOrDefault((p) => p.Id == id);
            if (currency == null)
            {
                return NotFound();
            }
            return Ok(currency);
        }

        public void GetGraphic()
        {
            Image imageFile = Image.FromFile("pic.bmp");
            Graphics graphics = Graphics.FromImage(imageFile);
            Bitmap bitmap = new Bitmap(1000, 300, graphics);
            YHorizontal = imageFile.Height - marginDown;
            lenghtHorizontal = imageFile.Width - marginLeft - marginRight;
            xMax = imageFile.Width - marginRight;
            lenghtVertical = YHorizontal - marginUp;
            double hStep = (double)(lenghtHorizontal / currencys.Length);
            int vStep = (int)(lenghtVertical / 10);
 
            //координатная плоскость
            Pen pen = new Pen(Color.Black, 2);
            graphics.DrawLine(pen, marginLeft, YHorizontal, marginLeft, marginUp);
            graphics.DrawLine(pen, marginLeft, YHorizontal, xMax, YHorizontal);
            for (int i = 1; i <= 10; i++)
            {
                int Y = YHorizontal - i * vStep;
                graphics.DrawLine(pen, marginLeft-5, Y, marginLeft, Y);
                graphics.DrawString((73 + i).ToString(), new Font("Arial", 8), Brushes.Black, 2, Y - 5);
            }

            //Y starts with 74 at (marginLeft, marginDown)
            //Y stops at 84 at (marginLeft, marginUp)
            float prevX = marginLeft;
            float prevY = (float) (marginDown + (currencys[0].Value - 74) / 10 * vStep);
            for (int i = 1; i < currencys.Length; i++)
            {
                float X = (float) (marginLeft + (hStep * i));
                float Y = (float)(marginDown + (currencys[i].Value - 74) / 10 * vStep);
                graphics.DrawLine(pen, prevX, prevY, X,Y);
                prevX = X;
                prevY = Y;
            }
            imageFile.Save("pic.bmp");
        }
    }
}
