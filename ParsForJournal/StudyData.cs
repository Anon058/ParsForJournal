using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsForJournal
{
    public static class StudyData
    {
        public static readonly Dictionary<string, Dictionary<string, string>> Lessons = new Dictionary<string, Dictionary<string, string>>()
        {
            ["100"] = new Dictionary<string, string>()
            {
                ["7374"] = "Биология",
                ["7367"] = "География",
                ["7179"] = "Иностранный язык",
                ["7172"] = "Информатика/Гр.1",
                ["7845"] = "Информатика/Гр.2",
                ["7174"] = "История",
                ["7177"] = "Литература",
                ["7173"] = "Математика",
                ["7175"] = "Обществознание",
                ["7181"] = "Основы безопасности и защиты Родины",
                ["7368"] = "Основы бережливого производства",
                ["7376"] = "Основы исследовательской и проектной деятельности",
                ["7178"] = "Русский язык",
                ["8058"] = "Самостоятельная подготовка",
                ["7375"] = "Физика",
                ["7176"] = "Физическая культура",
                ["7171"] = "Химия"
            }

        };
    }
}
