using System;
using static System.Console;
using System.Globalization;
using System.Collections.Generic;

//Exception for when contestant number is not between 0 and 30
class NotBetween0and30Exception : Exception
{
  private static string msg = "Number must be between 0 and 30";
  public NotBetween0and30Exception() : base(msg)
  {}
}

//Exception for when contestant's talent code is not S D O or M
class InvalidCodeException : Exception
{
  private static string msg = "{0} is not a valid talent code. Assigned as Invalid.";
  public InvalidCodeException(char value) : base(string.Format(msg, value))
  {}
}

//Exception for when entered talent code in GetLists is not S D O or M
class InvalidTypeException : Exception
{
  private static string msg = "{0} is not a valid code.";
  public InvalidTypeException(string value) : base(string.Format(msg, value))
  {}
}

class Contestant
{
  private int numberOfContestants;
  public int NumberOfContestants
  {
    get
    {
      return numberOfContestants;
    }
    set
    {
      //throw exception if the number of contestants entered is not between 0 and 30
      if(value > 30 || value < 0)
      {
        NotBetween0and30Exception nbe = new NotBetween0and30Exception();
        throw(nbe);
      }
      numberOfContestants = value;
    }
  }
  public static char[] talentCodes = new char[] {'S', 'D', 'M', 'O'}; 
  public static string[] talentStrings = new string[] {"Singing", "Dancing", "Musical instrument", "Other"};
  public static int[] counts = {0, 0, 0, 0};
  private string name;
  public string Name {get; set;}
  private string talent {get; set;}
  public string Talent
  {
    get
    {
      return talent;
    }
    set
    {
      talent = value;
    }
  }
  private char talentCode;
  public char TalentCode
  {
    get
    {
      return talentCode;
    }
    set
    {

      //Check to see if entered talent code is S D O or M. If it isn't, assign I.
      bool isValid = false;

      while (!isValid)
      {
        for (int z = 0; z < Contestant.talentCodes.Length; ++z)
        {
          if (value == Contestant.talentCodes[z])
          {
            isValid = true;
            ++counts[z];
            this.talentCode = value;
            this.Talent = Contestant.talentStrings[z];
          }
        }
        if (!isValid)
        {
          this.talentCode = 'I';
          break;
        }
      }
    }  
  }
  private int age;
  public int Age
  {
    get
    {
      return age;
    }
    set
    {
      age = value;
    }
  }
  private double fee;
  public double Fee
  {
    get
    {
      return fee;
    }
    set
    {
      fee = value;
    }  
  }
  public Contestant(){}
}

class ChildContestant : Contestant
{
  public ChildContestant(){}  
  public ChildContestant(string name, char talentCode, int age)
  {
    this.Name = name;
    this.TalentCode = talentCode;
    this.Age = age;
    this.Fee = 15;
  }

  public string getType()
  {
   return "Child Contestant";
  }
  
  public override string ToString()
  {
    return(getType() + " " + Name + " " + TalentCode + " Fee " + Fee.ToString("C", CultureInfo.GetCultureInfo("en-US")));
  }
}

class TeenContestant : Contestant
{
  public TeenContestant(){}  
  public TeenContestant(string name, char talentCode, int age)
  {
    this.Name = name;
    this.TalentCode = talentCode;
    this.Age = age;
    this.Fee = 20;
  }
  
  public string getType()
  {
   return "Teen Contestant";
  }
  
  public override string ToString()
  {
    return(getType() + " " + Name + " " + TalentCode + " Fee " + Fee.ToString("C", CultureInfo.GetCultureInfo("en-US")));
  }
}

class AdultContestant : Contestant
{
  public AdultContestant(){}  
  public AdultContestant(string name, char talentCode, int age)
  {
    this.Name = name;
    this.TalentCode = talentCode;
    this.Age = age;
    this.Fee = 30;
  }
  
  public string getType()
  {
   return "Adult Contestant";
  }
  
  public override string ToString()
  {
    return(getType() + " " + Name + " " + TalentCode + " Fee " + Fee.ToString("C", CultureInfo.GetCultureInfo("en-US")));
  }
}

class GreenvilleRevenue
{
  static List<Contestant> ContestantList = new List<Contestant>();
  
  static void Main()
  {
    int contestantsThisYear;

    //Call getContestantNumber to prompt user to enter contestant number, and then assign the result to a variable
    contestantsThisYear = getContestantNumber("this", 0, 30);

    //Call getContestantData to prompt user to enter the name, talent code and age for each contestant this year
    getContestantData(contestantsThisYear, Contestant.talentCodes, Contestant.talentStrings, Contestant.counts);

    //Call getLists to continuously prompt use to enter a talent code and to show the contestant data for that talent code until they enter Z
    getLists();
  }

  public static int getContestantNumber(string when, int min, int max)
  {
    //Prompt user to enter number of contestants for this year
    WriteLine("Enter number of contestants {0} year >>", when);
    
    //Check that input is between 0 and 30. If it isn't, throw an exception and get user input again.
    Contestant num = new Contestant();
    int enteredNum;
    while(num.NumberOfContestants == 0)
    {
      try
      {
        int.TryParse(ReadLine(), out enteredNum);
        num.NumberOfContestants = enteredNum;
      }
      catch(NotBetween0and30Exception e)
      {
        WriteLine(e.Message);
        num.NumberOfContestants = 0;
      }
    } 
    ;
    return num.NumberOfContestants;
  }

  public static void getContestantData(int numThisYear, char[] talentCodes, string[] talentStrings, int[] counts)
  {
    //Initiate a counter so that the user is asked for contestant data until they've entered it for all contestants
    int x = 0;
    while (x < numThisYear)
    {
      //Prompt user to enter name of contestant
      Console.Write("Enter contestant name >> ");
      string enteredName;
      enteredName = Console.ReadLine();
      
      //Prompt user to enter talent code of contestant
      WriteLine("Talent codes are:");
      for (int y = 0; y < talentCodes.Length; ++y)
      WriteLine(" {0} {1}", talentCodes[y], talentStrings[y]);
      Write(" Enter talent code >> ");
      char enteredTalent;

      //Verify that talent code entry is a character 
      while(!(char.TryParse(ReadLine(), out enteredTalent)))
      {
        Console.WriteLine("Invalid entry! Please try again.");
      }

      //Prompt user to enter age of contestant
      Console.WriteLine("Please enter the contestant's age >> ");
      int enteredAge;
      
      //Verify that age entry is a number
      while(!(Int32.TryParse(ReadLine(), out enteredAge)))
      {
        Console.WriteLine("Invalid entry! Please try again.");
      }

      //Create a new contestant based on the age
      if (enteredAge <= 12) 
      ContestantList.Add(new ChildContestant(enteredName, enteredTalent, enteredAge));
      else if (enteredAge > 12 && enteredAge < 18)
      ContestantList.Add(new TeenContestant(enteredName, enteredTalent, enteredAge));
      else if (enteredAge >= 18)
      ContestantList.Add(new AdultContestant(enteredName, enteredTalent, enteredAge));

      //Check if the assigned talent was I. If it is, throw an excpetion.
      try
      {
        if (ContestantList[x].TalentCode == 'I')
        {
          InvalidCodeException nbe = new InvalidCodeException(enteredTalent);
          throw(nbe);
        }
      }
      catch (InvalidCodeException e)
      {
        WriteLine(e.Message);
      }
      //Increase contestant counter by 1 
      ++x;
    }
  } 

  public static void getLists()
  {
    string currentCode = null;

  // Display names of contestants based on the inputted code by looping through ContestantList and printing each contestants' ToString method
    do 
    {
      WriteLine("Enter a talent code: ");
      currentCode = ReadLine();
      if(currentCode == "M")
      {
        WriteLine("Contestants with talent Musical instrument are:");
        foreach (Contestant oConst in ContestantList)
        {
          if (oConst.TalentCode == 'M')
            WriteLine(oConst.ToString());
        }
      }
      else if(currentCode == "S") 
      {
        WriteLine("Contestants with talent Singing are:");
        foreach (Contestant oConst in ContestantList)
        {
          if (oConst.TalentCode == 'S')
            WriteLine(oConst.ToString());
        }
      }
      else if(currentCode == "D") 
      {
        WriteLine("Contestants with talent Dancing are:");
        foreach (Contestant oConst in ContestantList)
        {
          if (oConst.TalentCode == 'D')
            WriteLine(oConst.ToString());
        }
      }
      else if(currentCode == "O") 
      {
        WriteLine("Contestants with talent Other are:");
        foreach (Contestant oConst in ContestantList)
        {
          if (oConst.TalentCode == 'O')
            WriteLine(oConst.ToString());
        }
      }
      else 
      {
        //If the entry wasn't any S D O or M, and it isn't Z, throw an exception.
        try
        {
          if (currentCode != "Z")
          {
            InvalidTypeException nbe = new InvalidTypeException(currentCode);
            throw(nbe);
          }
        }
        catch (InvalidTypeException e)
        {
          WriteLine(e.Message);
        }
      }
      //If Z is entered, get out of loop and end the program
    } 
  while(!(currentCode == "Z"));
  }
}
