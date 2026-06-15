using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace All_Student_Subject_Avarage
{
    /// <summary>
    /// This project is 100% open source. Any developer is free to copy, 
    /// modify, or distribute this code without any restrictions.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            bool bIsContinue = false;
            do
            {
                // Display welcome message
                PrintMessage("Welcome to the Student Success Verification App.", '~');

                // Get the success threshold from the user
                PrintMessage("Please specify the success rate\nas a number out of a percentage (%).", '=');
                double nPercentag = SuccessPercentage();

                // Get number of students
                PrintMessage("How many students are there? Enter the number.");
                int nNumberOfStudents = ChekNumberOfStudents();

                // Collect student names
                string[] sStudentNameArr = StudentName(nNumberOfStudents);

                // Get number of subjects
                PrintMessage("How many subjects are there? Enter a number.");
                int nNumberOfSubjects = ChekNumberOfSubject();
                PrintMessage("", '-');

                // Collect grades for all students
                double[,] nStudentGrade = StudentGrades(nNumberOfStudents, nNumberOfSubjects, sStudentNameArr);

                // Process and display results for each student
                for (int i = 0; i < nNumberOfStudents; i++)
                {
                    double nTotal = 0;
                    double nCheck = CheckTheResult(nStudentGrade, i, nNumberOfSubjects, out nTotal);

                    PrintMessage($"Student Name: {sStudentNameArr[i]}", '*');

                    if (nCheck >= nPercentag)
                    {
                        PrintMessage($"Total grades: {nTotal} | Rate: {nCheck} % - Level: {GetGradeLevel(nCheck)} ✅");
                    }
                    else
                    {
                        PrintMessage($"Total grades: {nTotal} | Rate: {nCheck} % - Level: {GetGradeLevel(nCheck)} ❌");
                    }
                }

                // Check if user wants to run the app again
                PrintMessage("If you wish to continue, press the number 1\nor any key on the keyboard to exit.");
                bIsContinue = false;
                int nContinue;
                int.TryParse(Console.ReadLine(), out nContinue);
                if (nContinue == 1)
                {
                    bIsContinue = true;
                }
                Console.Clear();

            } while (bIsContinue);

            // Closing message
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════════╗");
            Console.WriteLine("║    Thank you for using our app! C#Fazza++ 😏     ║");
            Console.WriteLine("╚══════════════════════════════════════════════════╝");
            Console.ResetColor();
        }

        // Helper method to print formatted console messages
        static void PrintMessage(string message, char Pattern = '-')
        {
            Console.WriteLine(new string(Pattern, 48));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine(new string(Pattern, 48));
        }

        // Validates and returns the success percentage
        static double SuccessPercentage()
        {
            double nPercentage;
            while (!double.TryParse(Console.ReadLine(), out nPercentage) || !(nPercentage > 0 && nPercentage <= 100))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Error: The number should be between 1 and 100.");
                Console.ResetColor();
            }
            return nPercentage;
        }

        // Validates the number of students
        static int ChekNumberOfStudents()
        {
            int nNumberOfStudents = 0;
            while (!int.TryParse(Console.ReadLine(), out nNumberOfStudents) || !(nNumberOfStudents > 0 && nNumberOfStudents <= 15))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Error: The number should be between 1 and 15.\nTry again");
                Console.ResetColor();
            }
            return nNumberOfStudents;
        }

        // Validates the number of subjects
        static int ChekNumberOfSubject()
        {
            int nNumberOfSubjects = 0;
            while (!int.TryParse(Console.ReadLine(), out nNumberOfSubjects) || !(nNumberOfSubjects > 0 && nNumberOfSubjects <= 15))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Error: The number should be between 1 and 15.\nTry again");
                Console.ResetColor();
            }
            return nNumberOfSubjects;
        }

        // Collects and returns an array of student names
        static string[] StudentName(int nStudentNumber)
        {
            string[] sName = new string[nStudentNumber];
            Console.WriteLine($"Please enter the student's name.");
            for (int i = 0; i < sName.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($" student Number {i + 1} : ");
                Console.ResetColor();
                sName[i] = Console.ReadLine();
            }
            return sName;
        }

        // Collects grades for each subject for every student
        static double[,] StudentGrades(int nNumStud, int nNumSubj, string[] sName)
        {
            double[,] nGrade = new double[nNumStud, nNumSubj];
            for (int i = 0; i < nNumStud; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write($"Enter student grades:  ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($" 🧑‍🎓 {sName[i]} ");
                Console.ResetColor();

                for (int j = 0; j < nNumSubj; j++)
                {
                    Console.Write($"... Subject Number: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{j + 1} = ");
                    Console.ResetColor();
                    while (!double.TryParse(Console.ReadLine(), out nGrade[i, j]) || !(nGrade[i, j] >= 0 && nGrade[i, j] <= 100))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("❌ Error: The number should be between 0 and 100.\nTry again");
                        Console.ResetColor();
                    }
                }
            }
            return nGrade;
        }

        // Calculates the total and the average for a specific student
        static double CheckTheResult(double[,] nStudentGrade, int nStudentIndex, int nNumberSub, out double nTotal)
        {
            nTotal = 0;
            for (int j = 0; j < nNumberSub; j++)
            {
                nTotal += nStudentGrade[nStudentIndex, j];
            }

            double nResult = nTotal / nNumberSub;
            return nResult;
        }

        // Classifies the student level based on their average
        static string GetGradeLevel(double nScore)
        {
            if (nScore >= 90) return "Excellent ";
            if (nScore >= 75) return "Good ";
            if (nScore >= 50) return "Pass ";
            return "Fail ";
        }
    }
}