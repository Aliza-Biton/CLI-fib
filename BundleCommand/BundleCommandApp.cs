using System;
using System.CommandLine;
using System.Linq.Expressions;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

var bundleCommand = new Command("bundle", "Bundle files to a single file");


//הגדרת האופציות השונות
var bundleOption = new Option<FileInfo>("--output", "my file");
var bundlelanguage = new Option<String>("--language", "choose the language")
{
    IsRequired = true
}.FromAmong("python", "java", "html", "jsx", "c++", "c#", "c", "css", "js", "sql", "json", "all"); ;
var bundleAuthor = new Option<String>("--author", "add a name of the author");
var bundleNote = new Option<bool>("--note", "add a note");
var bundleSort = new Option<String>("--sort", "sort the files").FromAmong("abc", "type");
var bundleEmptyLine = new Option<bool>("remove-empty-lines", "remove empty lines");

//הוספת קיצורים לפקודות
bundleOption.AddAlias("-o");
bundlelanguage.AddAlias("-l");
bundleAuthor.AddAlias("-a");
bundleNote.AddAlias("-n");
bundleSort.AddAlias("-s");
bundleEmptyLine.AddAlias("-r");

//הוספת האופציות לפקודה
bundleCommand.AddOption(bundleOption);
bundleCommand.AddOption(bundlelanguage);
bundleCommand.AddOption(bundleNote);
bundleCommand.AddOption(bundleAuthor);
bundleCommand.AddOption(bundleSort);
bundleCommand.AddOption(bundleEmptyLine);

//הקוד העיקרי- הפקודה בנדל
bundleCommand.SetHandler(async (output, language, note, author, sort, remove) =>
{
    //הכנסת הניתוב הנוכחי למשתנה
    var currentPath = Environment.CurrentDirectory;
    try
    {
        //יצירת קובץ בשם נבחר או שם ברירת מחדל
        string fileName;
        if (output == null)
            fileName = Path.Combine(currentPath, "bundle.txt");
        else
            fileName = output.FullName;

        // בדיקת סיומת הקובץ
        if (!fileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("סיומת הקובץ חייבת להיות .txt");
            return;
        }
        try
        {
            // יצירת הקובץ
            using (File.Create(fileName)) { }
        }
        catch
        {
            Console.WriteLine("שגיאה ביצירת הקובץ");
            return;
        }


        //טיפול בשפות
        //מערך השפות האפשריות לבחירה
        var arrlanguages = new[] { "c#", "python", "java", "c", "js", "html", "jsx", "c++", "SQL", "json", "css" };
        //מערך עזר מקביל
        var arrlanguagesEz = new[] { "*.cs", "*.py", "*.java", "*.c", "*.js", "*.html", "*.jsx", "*.cpp", "*.sql", "*.json", "*.css" };
        //אם נבחרו כל השפות
        List<string> arrFiles = new List<string>();
        if (language.Equals("all"))
        {
            //מעבר על מערך השפות ואסיפתם לרשימה
            for (int i = 0; i < arrlanguages.Length; i++)
            {
                var nowArrFiles = Directory.EnumerateFiles(currentPath, arrlanguagesEz[i], SearchOption.AllDirectories)
                .Where(file => !file.Contains("debug") && !file.Contains("bin")).ToList();
                arrFiles.AddRange(nowArrFiles);
            }
            arrFiles.ForEach(file => Console.WriteLine(file));
        }
        //אם נבחרה שפה אחת
        else
        {
            var myCurrent = "";
            int i = 0;
            for (; i < arrlanguages.Length && !language.Equals(arrlanguages[i]); i++) ;
            try
            {
                myCurrent = arrlanguagesEz[i];
                //אסיפת קבצי הקוד לרשימה אחת
                arrFiles = Directory.EnumerateFiles(currentPath, myCurrent, SearchOption.AllDirectories)
                .Where(file => !file.Contains("debug") && !file.Contains("bin")).ToList();
            }
            catch
            {
                Console.WriteLine("the name of the language is worng");
                return;
            }
        }
        if (arrFiles.Count == 0)
        {
            Console.WriteLine("לא נמצאו קבצים מתאימים.");
            return;
        }
        if (sort == null || sort.Equals("abc"))
        {

            // מיין לפי שם הקובץ
            var sortedFilePaths = arrFiles.OrderBy(path => Path.GetFileName(path)).ToList();
            arrFiles = sortedFilePaths;
        }

        //מעבר על הקבצים והעתקם לקובץ שנוצר
        using (StreamWriter writer = new StreamWriter(fileName, true, Encoding.UTF8))
        {
            //אופציית הוספת ניתוב מקןר הקבצים
            if (note)
            {
                await writer.WriteAsync("//The source of the file: " + currentPath);
                await writer.WriteAsync(Environment.NewLine);
            }
            if (author != null)
            {
                await writer.WriteAsync("//The author: " + author);
                await writer.WriteAsync(Environment.NewLine);
            }
            // מעבר על הקבצים והעתקם לקובץ שנוצר
            foreach (var file in arrFiles)
            {
                try
                {
                    var text = await File.ReadAllLinesAsync(file);
                    await writer.WriteAsync(Environment.NewLine);
                    await writer.WriteAsync("//--------------");
                    await writer.WriteAsync(Environment.NewLine);

                    foreach (var line in text)
                    {
                        if (!string.IsNullOrWhiteSpace(line) || remove == false)
                        {
                            await writer.WriteLineAsync(line);
                        }
                    }
                }
                catch
                {
                    Console.WriteLine($"אירעה שגיאה בקריאת הקובץ {file}");
                }

            }
        }
        Console.WriteLine("הקובץ נוצר בהצלחה");
    }
    catch
    {
        Console.WriteLine("you are have a worng");
    }
}, bundleOption, bundlelanguage, bundleNote, bundleAuthor, bundleSort, bundleEmptyLine);

//הגדרת פקודת השורש
var rootCommand = new RootCommand("Root command for File Bundler CLI");

rootCommand.AddCommand(bundleCommand);

await rootCommand.InvokeAsync(args);


