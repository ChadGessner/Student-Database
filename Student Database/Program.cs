using System;
using System.Collections.Generic;
string[] students = 
{ 
    "Chad",
    "Anh",
    "Craig",
    "John",
    "Grant",
    "Katie",
    "Rocket Mortgage"
};
string[] homeTown =
{
    "Wyoming",
    "Grand Rapids",
    "Detroit",
    "Detroit",
    "Grand Rapids",
    "Rocket Mortgage",
    "Rocket Mortgage",
};
string[] food =
{
    "Pizza",
    "Pizza",
    "Pizza",
    "Rocket Mortgage",
    "Pizza",
    "Cheese",
    "Grand Circus Alumni"
};
Orchestrator();

void Orchestrator()
{
    bool isKey;
    bool isNewUser;
    string key = GetKey();
    while (true)
    {
        Console.WriteLine(GetCategory(key));
        Console.WriteLine($"Would you like to know more about {key}?");
        isKey = Console.ReadLine().Trim().ToLower() == "y";
        if (isKey)
        {
            continue;
        }
        Console.WriteLine("Would you like to know about another student?");
        isNewUser = Console.ReadLine().TrimEnd().ToLower() == "y";
        if (isNewUser)
        {
            key = GetKey();
            continue;
        }
        Console.WriteLine("Have a great day!");
        break;

    }
}

Dictionary<string, string[]> GetStudentsDictionary()
{
    Dictionary<string, string[]> studentsDictionary = new Dictionary<string, string[]>();
    for (int i = 0; i < students.Length; i++)
    {
        studentsDictionary[students[i]] = new string[] { homeTown[i], food[i] };
    }
    return studentsDictionary;
}

bool IsInRange(int selection, int length)
{
    return selection <= length && selection > 0;
}

string TitleCase(string input)
{
    string output = string.Empty;
    string[] inputArray = input.Split();
    for(int i = 0; i < inputArray.Length; i++)
    {
        output += inputArray[i][0].ToString().ToUpper() + inputArray[i].Substring(1) + " ";
    }
    return output.Trim();
}

string GetKey()
{
    Dictionary<string, string[]> studentsDictionary = GetStudentsDictionary();
    int selection;
    string key;
    while (true)
    {
        Console.WriteLine("Choose from the following list of students...");
        for (int i = 0; i < students.Length; i++)
        {
            Console.WriteLine($"{i + 1} {students[i]} \n");
        }
        string read = Console.ReadLine().Trim();
        bool isInt = int.TryParse(read, out selection);
        bool fancyParse = isInt ? IsInRange(selection, students.Length) : studentsDictionary.ContainsKey(TitleCase(read));
        if (!fancyParse)
        {
            Console.WriteLine
                (
                "That was not a valid integer from the list or a valid student name... " +
                "\nEnter a valid integer from the list or a valid student name... \n"
                );
            continue;
        }
        key = isInt ? students[selection - 1] : TitleCase(read);
        break;
    }
    return key;
}

bool ParseCategoryString(string category, string key, out int selectedCategory)
{
    string[] categories = GetStudentsDictionary()[key];
    string homeTown = "Home Town";
    string favoriteFood = "Favorite Food";
    bool isValidCategory = homeTown.Contains(TitleCase(category)) || favoriteFood.Contains(TitleCase(category)) || homeTown == TitleCase(category) || favoriteFood == TitleCase(category);
    selectedCategory = isValidCategory && homeTown.Contains(TitleCase(category)) || homeTown == TitleCase(category) ? 1 : 2;
    return isValidCategory;
}

string GetCategory(string key)
{
    string[] categories = GetStudentsDictionary()[key];
    string homeTown = categories[0];
    string favoriteFood = categories[1];
    int selection;
    string category;
    while (true)
    {
        Console.WriteLine($"What would you like to know about {key}?");
        Console.WriteLine($"1) {key}'s Home Town...");
        Console.WriteLine($"2) {key}'s Favorite Food...");
        string read = Console.ReadLine().Trim();
        bool isInt = int.TryParse(read, out selection);
        bool isValidCategory = isInt ? isInt : ParseCategoryString(read, key, out selection);
        bool parseSelection = isInt ? IsInRange(selection, categories.Length) : isValidCategory;
        if (!parseSelection)
        {
            Console.WriteLine
                (
                $"That was not a valid selection, " +
                $"\nplease enter a valid integer or category name..."
                );
            continue;
        }
        category = selection == 1 ? "Home Town" : "Favorite Food";
        return $"{key}'s {category} is {categories[selection - 1]}... \n";
    }

}
   