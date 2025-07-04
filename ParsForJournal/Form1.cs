﻿using NUnit.Framework;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Test1();
        }

       

        private void Test1()
        {

            EdgeDriverService service = EdgeDriverService.CreateDefaultService();
            var edgeOptions = new EdgeOptions();
            var downloadDirectory = textBox1.Text;
            if(downloadDirectory == null)
            {
                downloadDirectory = "C:\\Users\\sasha\\Downloads";
            }
            else
            {
            edgeOptions.AddUserProfilePreference("download.default_directory", downloadDirectory);
            }
            edgeOptions.AddUserProfilePreference("intl.accept_languages", "nl");
            edgeOptions.AddUserProfilePreference("disable-popup-blocking", "true");
            var driver = new EdgeDriver(service, edgeOptions);

            driver.Navigate().GoToUrl("https://poo.susu.ru/");
            driver.Manage().Window.Maximize();

            Thread.Sleep(2500);
            IWebElement webElement = driver.FindElement(By.Id("schools"));

            SelectElement selectElement = new SelectElement(driver.FindElement(By.Id("schools")));
            selectElement.SelectByValue("2");

            driver.FindElement(By.Name("UN")).SendKeys(textBox2.Text);

            webElement = driver.FindElement(By.Name("PW"));
            webElement.SendKeys(textBox3.Text);

            webElement.SendKeys(OpenQA.Selenium.Keys.Enter);

            Thread.Sleep(5000);
            IWebElement otchet = driver.FindElement(By.XPath("/html/body/div/div[1]/div[4]/nav/ul/li[5]/a"));
            otchet.Click();

            otchet = driver.FindElement(By.XPath("/html/body/div/div[1]/div[4]/nav/ul/li[5]/ul/li[1]/a"));
            otchet.Click();

            for (int i = 0; i < int.MaxValue; i++)
            {
                try
                {
                    Thread.Sleep(3000);
                    SelectElement group = new SelectElement(driver.FindElement(By.Name("PCLID_IUP")));
                    group.SelectByIndex(i);

                    for (int j = 0; j <= int.MaxValue; j++)
                    {
                        try
                        {

                            SelectElement course = new SelectElement(driver.FindElement(By.Name("SGID")));
                            course.SelectByIndex(j);

                            Thread.Sleep(3000);
                            try
                            {
                                string selectSemestr = comboBox1.Text;
                                SelectElement period = new SelectElement(driver.FindElement(By.Name("TERMID")));
                                

                                    var priod = driver.FindElements(By.CssSelector("input[type='hidden'][name='TERMID']"));
                                    
                                if(priod.Count > 0)
                                {
                                    var selectedValue = priod[0].GetAttribute("value");
                                    if((selectedValue == "10" && selectSemestr == "1 полугодие") ||
                                        (selectedValue == "9" && selectSemestr == "2 полугодие"))
                                    {
                                        continue;
                                    }
                                }
                                
                                if(selectSemestr == "1 полугодие")
                                period.SelectByValue("9");
                                if(selectSemestr == "2 полугодие")
                                period.SelectByValue("10");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Ошибка при обработке полугодия: {ex.Message}");
                            }




                            Thread.Sleep(3000);
                            IWebElement load = driver.FindElement(By.Id("load-journal-btn"));
                            load.Click();
                            load.SendKeys(OpenQA.Selenium.Keys.Return);

                            try
                            {
                                Thread.Sleep(3000);
                                IWebElement setup = driver.FindElement(By.XPath("//button[@title='Экспорт в Excel']"));//ошибьк
                                setup.SendKeys(OpenQA.Selenium.Keys.Return);

                                Thread.Sleep(3000);
                                IWebElement confirm = driver.FindElement(By.XPath("//button[text()='Да, больше не спрашивать']"));
                                confirm.SendKeys(OpenQA.Selenium.Keys.Return);

                            }
                            catch
                            {
                                continue;
                            }
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
                catch
                {
                    break;
                }

            }

            driver.Quit();
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
    }
}