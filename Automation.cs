using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MyVacation
{
    class Automation
    {
        IWebDriver driver;
        List<Database> databases = new List<Database>();
        public async Task<bool> Run()
        {
            try
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--disable-blink-features=AutomationControlled");
                chromeOptions.AddArgument("user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.169 Safari/537.36");
                if(MainWindow.isHeadless)
                {
                    chromeOptions.AddArgument("headless");
                }

                using (driver = new ChromeDriver(chromeOptions))
                {
                    driver.Url = "https://www.skyscanner.net/";
                    if(driver.Url.Contains("sttc/px/captcha-v2/"))
                    {
                        MessageBox.Show("It's Captcha");
                        await Task.Delay(7000);
                        return false;
                    }
                    else
                    {
                        IWebElement origin = await Task.Run(() => driver.FindElement(By.XPath("//input[@id='fsc-origin-search']")));
                        IWebElement from = await Task.Run(() => driver.FindElement(By.XPath("//label[@for='fsc-origin-search']")));
                        origin.Click();
                        from.Click();
                        origin.Click();
                        origin.Clear();
                        origin.Click();
                        origin.SendKeys(MainWindow.From);
                        from.Click();
                        IWebElement destination = await Task.Run(() => driver.FindElement(By.XPath("//input[@id='fsc-destination-search']")));
                        destination.Click();
                        destination.Clear();
                        destination.SendKeys(MainWindow.To);
                        var ddate = await Task.Run(() => driver.FindElement(By.XPath("//span[@class='DateInput_DateInput--text__-b97u']")));
                        var rdate = await Task.Run(() => driver.FindElement(By.XPath("//button[@id='return-fsc-datepicker-button']")));
                        var jdriver = (IJavaScriptExecutor)driver;
                        jdriver.ExecuteScript($"var ele=arguments[0]; ele.innerHTML = '{MainWindow.dDay}/{MainWindow.dMonth}/{MainWindow.dYear}';", ddate);
                        jdriver.ExecuteScript($"var ele=arguments[0]; ele.innerHTML = '{MainWindow.rDay}/{MainWindow.rMonth}/{MainWindow.rYear}';", rdate);
                        IWebElement submit = await Task.Run(() => driver.FindElement(By.XPath("//button[@type='submit']")));
                        submit.Click();
                        Thread.Sleep(5000);

                        try
                        {
                            if (await Task.Run(() => driver.FindElement(By.XPath("//span[@class='price flightLink']")).Text) == "Get prices")
                            {
                                await Task.Run(() => driver.FindElement(By.XPath("//span[@class='price flightLink']")).Click());
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            
                        }
                        try
                        {
                            if(await Task.Run(() => driver.FindElement(By.XPath("//span[@id='browse-error-apologies-icon']")).Text) == "")
                            {
                                MessageBox.Show("Sorry, we found no flights on this date.");
                                return false;
                            }
                        }
                        catch (NoSuchElementException)
                        {

                        }
                        if (driver.Url.Contains("sttc/px/captcha-v2/"))
                        {
                            MessageBox.Show("It's Captcha");
                            Thread.Sleep(7000);
                            return false;
                        }

                        Thread.Sleep(5000);
                        int x = await Task.Run(() => driver.FindElements(By.XPath("//div[@class='EcoTicketWrapper_itineraryContainer__ZWE4O']")).Count);
                        int y = 1, z = 0, a = 0;
                        for (int i = 0; i < x; i++)
                        {
                            databases.Add(new Database
                                    (
                                    await Task.Run(() => driver.FindElements(By.XPath("//a[@class='FlightsTicket_link__ODZjM']"))[i].GetAttribute("href")),
                                    await Task.Run(() => driver.FindElements(By.XPath("//img[@class='BpkImage_bpk-image__img__ZWY1M']"))[i].GetAttribute("alt")),
                                    await Task.Run(() => driver.FindElements(By.XPath("//span[@class='BpkText_bpk-text__YWQwM BpkText_bpk-text--xl__MmE4Y']"))[a + z].Text),
                                    await Task.Run(() => driver.FindElements(By.XPath("//span[@class='BpkText_bpk-text__YWQwM BpkText_bpk-text--base__NDljN LegInfo_routePartialCityTooltip__NTE4Z']"))[a + z].Text),
                                    await Task.Run(() => driver.FindElements(By.XPath("//span[@class='BpkText_bpk-text__YWQwM BpkText_bpk-text--xl__MmE4Y']"))[a + y].Text),
                                    await Task.Run(() => driver.FindElements(By.XPath("//span[@class='BpkText_bpk-text__YWQwM BpkText_bpk-text--base__NDljN LegInfo_routePartialCityTooltip__NTE4Z']"))[a + y].Text),
                                    await Task.Run(() => driver.FindElements(By.XPath("//span[@class='BpkText_bpk-text__YWQwM BpkText_bpk-text--xl__MmE4Y']"))[a + z + 2].Text),
                                    await Task.Run(() => driver.FindElements(By.XPath("//span[@class='BpkText_bpk-text__YWQwM BpkText_bpk-text--base__NDljN LegInfo_routePartialCityTooltip__NTE4Z']"))[a + z + 2].Text),
                                    await Task.Run(() => driver.FindElements(By.XPath("//span[@class='BpkText_bpk-text__YWQwM BpkText_bpk-text--xl__MmE4Y']"))[a + y + 2].Text),
                                    await Task.Run(() => driver.FindElements(By.XPath("//span[@class='BpkText_bpk-text__YWQwM BpkText_bpk-text--base__NDljN LegInfo_routePartialCityTooltip__NTE4Z']"))[a + y + 2].Text)
                                    ));
                            a += 3;
                            z++;
                            y++;
                        }
                    }
                }
                driver.Quit();
                SaveData(databases);
                DataView view = new DataView();
                view.Show();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public void SwitchToWindow(Expression<Func<IWebDriver, bool>> predicateExp)
        {
            var predicate = predicateExp.Compile();
            foreach (var handle in driver.WindowHandles)
            {
                driver.SwitchTo().Window(handle);
                if (predicate(driver))
                {
                    return;
                }
            }

            throw new ArgumentException(string.Format("Unable to find window with condition: '{0}'", predicateExp.Body));
        }

        public void SaveData(List<Database> data)
        {
            if (File.Exists("Airline.json"))
            {
                DateTime dt = File.GetLastWriteTime("Airline.json");
                if((DateTime.Now - dt).TotalHours > 24)
                {

                }
            }
            else
            {
                
            }
            using (StreamWriter sw = new StreamWriter(new FileStream("Airline.json", FileMode.Append)))
            {
                sw.Write(JsonConvert.SerializeObject(data, Formatting.Indented));
            }
        }
    }
}
