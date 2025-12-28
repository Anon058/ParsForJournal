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
            //Test1();
            SecondCourse();
        }

        
        public IWebDriver EnterToSchool()
        {
            EdgeDriverService service = EdgeDriverService.CreateDefaultService();
            var edgeOptions = new EdgeOptions();
            var downloadDirectory = textBox1.Text;
            if (downloadDirectory == null)
            {
                downloadDirectory = "C:\\Users";
            }
            else
            {
                edgeOptions.AddUserProfilePreference("download.default_directory", downloadDirectory);
            }
            edgeOptions.AddUserProfilePreference("intl.accept_languages", "nl");
            edgeOptions.AddUserProfilePreference("disable-popup-blocking", true);
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

            otchet = wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div/div[1]/div[4]/nav/ul/li[5]/ul/li[1]/a")));
            otchet.SendKeys(OpenQA.Selenium.Keys.Return);

            return driver;
        }

        public void SecondCourse()
        {
            Dictionary<string, int> groups = new Dictionary<string, int>()
            {
                { "371_0", 100 },
                { "372_0", 101 },
                { "373_0", 102 },
                { "374_0", 103 },
                { "375_0", 104 },
                { "376_0", 105 },
                { "378_0", 106 },
                { "379_0", 107 },
                { "377_0", 110 },
                { "383_0", 112 },
                { "380_0", 120 },
                { "381_0", 121 },
                { "382_0", 130 },
                { "369_0", 160 }
            };

            IWebDriver driver = EnterToSchool();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            try
            {

            foreach(var groupp in groups)
            {
            SelectElement group = new SelectElement(wait.Until(ExpectedConditions.ElementExists(By.Name("PCLID_IUP"))));
                
                Thread.Sleep(1500);
                group.SelectByValue(groupp.Key);

                    var lessons = StudyData.Lessons[groupp.Value.ToString()];
                    foreach(var lesson in lessons)
                    {
                        SelectElement less = new SelectElement(wait.Until(ExpectedConditions.ElementExists(By.Name("SGID"))));

                        less.SelectByValue(lesson.Key);

                        wait.Until(l => new SelectElement(l.FindElement(By.Name("SGID")))
                                            .SelectedOption.Text == lesson.Value.ToString());
                    }

                wait.Until(d => new SelectElement(d.FindElement(By.Name("PCLID_IUP")))
                                    .SelectedOption.Text == groupp.Value.ToString());
            }
            }
            finally
            {
                driver.Quit();
            }

        }
        public void Test1()
        {
            Dictionary<int, string> lessons = new Dictionary<int, string>()
            {
                { 1, "Биология" },
                {2, "География" },
                {3, "Иностранный язык" },
                {4, "Информатика/Гр.1" },
                {5, "Информатика/Гр.2" },
                {6, "История" },
                {7, "Литература" },
                {8, "Математика" },
                {9, "Обществознание" },
                {10, "Основы безопасности и защиты Родины"},
                {11, "Основы бережливого производства" },
                {12, "Основы исследовательской и проектной деятельности" },
                {13, "Русский язык" },
                {14, "Самостоятельная подготовка" },
                {15, "Физика" },
                {16, "Физическая культура" },
                {17, "Химия" }
            };



            IWebDriver driver = EnterToSchool();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            SecondCourse();
            try
            {
            SelectElement group = new SelectElement(wait.Until(ExpectedConditions.ElementExists(By.Name("PCLID_IUP"))));
                for (int i = 0; i < group.Options.Count; i++)
                {
                    group = new SelectElement(
                        wait.Until(ExpectedConditions.ElementExists(By.Name("PCLID_IUP"))));
                    group.SelectByIndex(i);

                    foreach(var lesson in lessons.Values)
                    {
                            SelectElement course = new SelectElement(driver.FindElement(By.Name("SGID")));

                        var option = course.Options.FirstOrDefault(o => o.Text.Trim().Equals(lesson, StringComparison.OrdinalIgnoreCase));

                        if (option == null)
                            continue;

                        option.Click();

                        wait.Until(d => new SelectElement(driver.FindElement(By.Name("SGID")))
                                            .SelectedOption.Text.Contains(lesson));

                        course.SelectByText(lesson);

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
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }



                        //IWebElement load = driver.FindElement(By.XPath("//button[text()='Загрузить']"));
                        Thread.Sleep(2000);
                        IWebElement load = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("load-journal-btn")));
                        load.Click();
                        load.SendKeys(OpenQA.Selenium.Keys.Return);


                        IWebElement setup = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[@title='Экспорт в Excel']")));//ошибьк
                        setup.SendKeys(OpenQA.Selenium.Keys.Return);

                        IWebElement confirm = wait.Until(ExpectedConditions.ElementExists(By.XPath("//button[text()='Да, больше не спрашивать']")));
                        confirm.SendKeys(OpenQA.Selenium.Keys.Return);





                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            finally
            {
                driver.Quit();
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