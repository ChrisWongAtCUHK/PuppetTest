using PuppeteerSharp;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PuppetTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Test().Wait();
        }

        static async Task Test()
        {
            try
            {
                await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
                using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions()
                {
                    Headless = false,
                    SlowMo = 1000 // if it is 100, program works
                }))
                {
                    using (var page = await browser.NewPageAsync())
                    {
                        const string siteName = "duckduckgo";
                        await page.GoToAsync($"https://{siteName}.com/");
                        await page.ScreenshotAsync($"{siteName}.png");
                        await page.CloseAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
