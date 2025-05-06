using System.CommandLine;
using System.IO;
using System.Text;

var rootCommand = new RootCommand("create a respone file to teh command dib bundle");

rootCommand.SetHandler(() =>
{
    //הגדרת משתנים
    bool chek = false;
    String output = "", auther = "", note = "", sort = "", language = "", remove = "";
    
    //output פקודת
    while (!chek)
    {
        Console.WriteLine("Enter a name or path. To use the default- enter default");
        output = Console.ReadLine();
        if (output != "" && output.Equals("default"))
        {
            chek = true;
        }
        else if(output != "" && output.EndsWith(".txt"))
        {
            string directoryPath = Path.GetDirectoryName(output);
            if (Directory.Exists(directoryPath) || string.IsNullOrEmpty(directoryPath))
            {
                chek = true;
            }
            else
            {
                Console.WriteLine("worng");
            }
        }
        else
        {
            Console.WriteLine("worng"); 
        }
    }

    //auther פקודת
    chek = false;
    while (!chek)
    {
        Console.WriteLine("Do you want to add the creator's name in a remark? press n or name");
        auther = Console.ReadLine();
        if (auther != "")
            chek = true;
        else
            Console.WriteLine("worng");
    }
    //note פקודת
    chek = false;
    while (!chek)
    {
        Console.WriteLine("Do you want to add the path of the files in a remark? press y or n");
        note = Console.ReadLine();
        if (note != "" && (note.Equals("n") || note.Equals("y")))
        {
            chek = true;
        }
        else
        {
            Console.WriteLine("worng");
        }
    }

    //sort פקודת
    chek = false;
    while (!chek)
    {
        Console.WriteLine("How do you want to sort the files? press abc or type");
        sort = Console.ReadLine();
        if (sort != "" && (sort.Equals("abc") || sort.Equals("type")))
        {
            chek = true;
        }
        else
        {
            Console.WriteLine("worng");
        }
    }

    //language פקודת
    List<string> languages = new List<string> { "c#", "python", "java", "c", "js", "html", "jsx", "c++", "SQL", "json", "css" };
    chek = false;
    while (!chek)
    {
        Console.WriteLine("What languages ​​do you want to pack? press a name or all");
        language = Console.ReadLine();
        if (language != "")
            if (language == "all" || languages.Contains(language))
                chek = true;
            else
                Console.WriteLine("worng");
        else
            Console.WriteLine("worng");
    }

    //remove פקודת
    chek = false;
    while (!chek)
    {
        Console.WriteLine("Do you want to delete empty lines from the code?  press y or n");
        remove = Console.ReadLine();
        if (remove != "" && (remove.Equals("n") || remove.Equals("y")))
        {
            chek = true;
        }
        else
        {
            Console.WriteLine("worng");
        }
    }

    //יצירת קובץ התגובה
    String nameFile = "responFile.rsp";
    using (File.Create(nameFile)) { };

    //הכתיבה לקובץ
    using (StreamWriter writer = new StreamWriter(nameFile, true))
    {

        if (!output.Equals("default"))
        {
            writer.WriteLine("-o " + output + " ");
            writer.WriteLine(Environment.NewLine);
        }
        if (!auther.Equals("n"))
        {
            writer.WriteLine("-a " + auther + " ");
            writer.WriteLine(Environment.NewLine);
        }

        if (note != "" && note.Equals("y"))
        {
            writer.WriteLine("-n ");
            writer.WriteLine(Environment.NewLine);
        }

        if (sort != "" && sort.Equals("type"))
        {
            writer.WriteLine("-s type ");
            writer.WriteLine(Environment.NewLine);
        }

        if (remove != "" && remove.Equals("y"))
        {
            writer.WriteLine("-r ");
            writer.WriteLine(Environment.NewLine);
        }

        writer.WriteLine("-l " + language);
    }

});
await rootCommand.InvokeAsync(args);



