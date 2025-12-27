using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V128.Debugger;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ParsForJournal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            Test1();
        }



        public void Test1()
        {

            EdgeDriverService service = EdgeDriverService.CreateDefaultService();
            var edgeOptions = new EdgeOptions();
            var downloadDirectory = textBox1.Text;
            if (downloadDirectory == null)
            {
                downloadDirectory = "C:\\";
            }
            else
            {
                edgeOptions.AddUserProfilePreference("download.default_directory", downloadDirectory);
            }
            edgeOptions.AddUserProfilePreference("intl.accept_languages", "nl");
            edgeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            var driver = new EdgeDriver(service, edgeOptions);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            driver.Navigate().GoToUrl("https://poo.susu.ru/");
            driver.Manage().Window.Maximize();

            IWebElement webElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("schools")));

            SelectElement selectElement = new SelectElement(driver.FindElement(By.Id("schools")));
            selectElement.SelectByValue("2");

            driver.FindElement(By.Name("UN")).SendKeys(textBox2.Text);

            webElement = driver.FindElement(By.Name("PW"));
            webElement.SendKeys(textBox3.Text);

            webElement.SendKeys(OpenQA.Selenium.Keys.Enter);

            IWebElement otchet = wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div/div[1]/div[4]/nav/ul/li[5]/a")));
            otchet.Click();

            otchet = driver.FindElement(By.XPath("/html/body/div/div[1]/div[4]/nav/ul/li[5]/ul/li[1]/a"));
            otchet.Click();

            SelectElement group = new SelectElement(wait.Until(ExpectedConditions.ElementExists(By.Name("PCLID_IUP"))));
            try
            {
                for (int i = 0; i < int.MaxValue; i++)
                {
                    group.SelectByIndex(i);

                    SelectElement course = new SelectElement(driver.FindElement(By.Name("SGID")));
                    for (int j = 0; j <= int.MaxValue; j++)
                    {

                        course.SelectByIndex(j);

                        string selectSemestr = comboBox1.Text;
                        SelectElement period = new SelectElement(wait.Until(ExpectedConditions.ElementExists(By.Name("TERMID"))));


                        var priod = driver.FindElements(By.CssSelector("input[type='hidden'][name='TERMID']"));

                        if (priod.Count > 0)
                        {
                            var selectedValue = priod[0].GetAttribute("value");
                            if ((selectedValue == "20" && selectSemestr == "1 полугодие") ||
                                (selectedValue == "19" && selectSemestr == "2 полугодие"))
                            {
                                continue;
                            }
                        }
                        try
                        {
                            if (selectSemestr == "1 полугодие")
                                period.SelectByValue("19");
                            if (selectSemestr == "2 полугодие")
                                period.SelectByValue("20");
                        }
                        catch
                        {

                        }





                        IWebElement load = wait.Until(ExpectedConditions.ElementExists(By.Id("load-journal-btn")));
                        load.Click();
                        load.SendKeys(OpenQA.Selenium.Keys.Return);


                        IWebElement setup = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[@title='Экспорт в Excel']")));//ошибьк
                        setup.SendKeys(OpenQA.Selenium.Keys.Return);

                        IWebElement confirm = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[text()='Да, больше не спрашивать']")));
                        confirm.SendKeys(OpenQA.Selenium.Keys.Return);





                    }

                    driver.Quit();
                }
            }
            catch
            {

            }
        }
        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            label6.Text = comboBox1.Text;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}