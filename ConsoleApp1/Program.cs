

namespace ConsoleApp1
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string Name { get; set; }
        public string Specialization { get; set; }

        public Instructor(int instructorId, string name, string specialization)
        {
            InstructorId = instructorId;
            Name = name;
            Specialization = specialization;
        }

        public string PrintDetails()
        {
            return $"Instructor ID: {InstructorId}, Name: {Name}, Specialization: {Specialization}";
        }
    }

    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public Instructor Instructor { get; set; }

        public Course(int courseId, string title, Instructor instructor)
        {
            CourseId = courseId;
            Title = title;
            Instructor = instructor;
        }

        public string PrintDetails()
        {
            string instructorName = Instructor != null ? Instructor.Name : "No Instructor Assigned";
            return $"Course ID: {CourseId}, Title: {Title}, Instructor: {instructorName}";
        }
    }

    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Course> Courses { get; set; }

        public Student(int studentId, string name, int age)
        {
            StudentId = studentId;
            Name = name;
            Age = age;
            Courses = new List<Course>();
        }

        public bool Enroll(Course course)
        {
            if (course != null && !Courses.Any(c => c.CourseId == course.CourseId))
            {
                Courses.Add(course);
                return true;
            }
            return false;
        }

        public string PrintDetails()
        {
            string courseTitles = Courses.Count > 0 ? string.Join(", ", Courses.Select(c => c.Title)) : "None";
            return $"Student ID: {StudentId}, Name: {Name}, Age: {Age}, Enrolled Courses: [{courseTitles}]";
        }
    }

    public class StudentManager
    {
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public List<Instructor> Instructors { get; set; }

        public StudentManager()
        {
            Students = new List<Student>();
            Courses = new List<Course>();
            Instructors = new List<Instructor>();
        }

        public bool AddStudent(Student student)
        {
            if (!Students.Any(s => s.StudentId == student.StudentId))
            {
                Students.Add(student);
                return true;
            }
            return false;
        }

        public bool AddCourse(Course course)
        {
            if (!Courses.Any(c => c.CourseId == course.CourseId))
            {
                Courses.Add(course);
                return true;
            }
            return false;
        }

        public bool AddInstructor(Instructor instructor)
        {
            if (!Instructors.Any(i => i.InstructorId == instructor.InstructorId))
            {
                Instructors.Add(instructor);
                return true;
            }
            return false;
        }

        public Student FindStudent(int studentId)
        {
            return Students.FirstOrDefault(s => s.StudentId == studentId);
        }

        public Course FindCourse(int courseId)
        {
            return Courses.FirstOrDefault(c => c.CourseId == courseId);
        }

        public Instructor FindInstructor(int instructorId)
        {
            return Instructors.FirstOrDefault(i => i.InstructorId == instructorId);
        }

        public bool EnrollStudentInCourse(int studentId, int courseId)
        {
            Student student = FindStudent(studentId);
            Course course = FindCourse(courseId);

            if (student != null && course != null)
            {
                return student.Enroll(course);
            }
            return false;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            StudentManager manager = new StudentManager();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("==========================================");
                Console.WriteLine("     STUDENT MANAGEMENT SYSTEM");
                Console.WriteLine("==========================================");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Instructor");
                Console.WriteLine("3. Add Course");
                Console.WriteLine("4. Enroll Student in Course");
                Console.WriteLine("5. Show All Students");
                Console.WriteLine("6. Show All Courses");
                Console.WriteLine("7. Show All Instructors");
                Console.WriteLine("8. Find Student by ID or Name");
                Console.WriteLine("9. Find Course by ID or Name");
                Console.WriteLine("10. Exit");
                Console.WriteLine("==========================================");
                Console.Write("Choose an option (1-10): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter Student ID: ");
                        int sId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Student Name: ");
                        string sName = Console.ReadLine();
                        Console.Write("Enter Student Age: ");
                        int sAge = int.Parse(Console.ReadLine());

                        Student newStudent = new Student(sId, sName, sAge);
                        if (manager.AddStudent(newStudent))
                            Console.WriteLine("\n[+] Student added successfully!");
                        else
                            Console.WriteLine("\n[!] Error: Student ID already exists!");
                        break;

                    case "2":
                        Console.Write("Enter Instructor ID: ");
                        int iId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Instructor Name: ");
                        string iName = Console.ReadLine();
                        Console.Write("Enter Specialization: ");
                        string spec = Console.ReadLine();

                        Instructor newInstructor = new Instructor(iId, iName, spec);
                        if (manager.AddInstructor(newInstructor))
                            Console.WriteLine("\n[+] Instructor added successfully!");
                        else
                            Console.WriteLine("\n[!] Error: Instructor ID already exists!");
                        break;

                    case "3":
                        Console.Write("Enter Course ID: ");
                        int cId = int.Parse(Console.ReadLine());
                        Console.Write("Enter Course Title: ");
                        string cTitle = Console.ReadLine();

                        Console.Write("Enter Instructor ID for this course: ");
                        int insId = int.Parse(Console.ReadLine());
                        Instructor assignedInstructor = manager.FindInstructor(insId);

                        if (assignedInstructor == null)
                        {
                            Console.WriteLine("\n[!] Instructor not found! Course cannot be added without a valid instructor.");
                        }
                        else
                        {
                            Course newCourse = new Course(cId, cTitle, assignedInstructor);
                            if (manager.AddCourse(newCourse))
                                Console.WriteLine("\n[+] Course added successfully!");
                            else
                                Console.WriteLine("\n[!] Error: Course ID already exists!");
                        }
                        break;

                    case "4":
                        Console.Write("Enter Student ID: ");
                        int studentIdToEnroll = int.Parse(Console.ReadLine());
                        Console.Write("Enter Course ID: ");
                        int courseIdToEnroll = int.Parse(Console.ReadLine());

                        if (manager.EnrollStudentInCourse(studentIdToEnroll, courseIdToEnroll))
                            Console.WriteLine("\n[+] Student enrolled successfully!");
                        else
                            Console.WriteLine("\n[!] Enrollment failed. Check IDs or duplication.");
                        break;

                    case "5":
                        Console.WriteLine("--- All Students ---");
                        if (manager.Students.Count == 0)
                        {
                            Console.WriteLine("No students found.");
                        }
                        else
                        {
                            foreach (var s in manager.Students)
                                Console.WriteLine(s.PrintDetails());
                        }
                        break;

                    case "6":
                        Console.WriteLine("--- All Courses ---");
                        if (manager.Courses.Count == 0)
                        {
                            Console.WriteLine("No courses found.");
                        }
                        else
                        {
                            foreach (var c in manager.Courses)
                                Console.WriteLine(c.PrintDetails());
                        }
                        break;

                    case "7":
                        Console.WriteLine("--- All Instructors ---");
                        if (manager.Instructors.Count == 0)
                        {
                            Console.WriteLine("No instructors found.");
                        }
                        else
                        {
                            foreach (var i in manager.Instructors)
                                Console.WriteLine(i.PrintDetails());
                        }
                        break;

                    case "8":
                        Console.Write("Enter Student ID or Name to search: ");
                        string sSearch = Console.ReadLine();
                        var foundStudents = manager.Students.Where(s => s.StudentId.ToString() == sSearch || s.Name.Equals(sSearch, StringComparison.OrdinalIgnoreCase)).ToList();

                        if (foundStudents.Count > 0)
                        {
                            foreach (var s in foundStudents)
                                Console.WriteLine(s.PrintDetails());
                        }
                        else
                        {
                            Console.WriteLine("No student found with that ID or Name.");
                        }
                        break;

                    case "9":
                        Console.Write("Enter Course ID or Title to search: ");
                        string cSearch = Console.ReadLine();
                        var foundCourses = manager.Courses.Where(c => c.CourseId.ToString() == cSearch || c.Title.Equals(cSearch, StringComparison.OrdinalIgnoreCase)).ToList();

                        if (foundCourses.Count > 0)
                        {
                            foreach (var c in foundCourses)
                                Console.WriteLine(c.PrintDetails());
                        }
                        else
                        {
                            Console.WriteLine("No course found with that ID or Title.");
                        }
                        break;

                    case "10":
                        exit = true;
                        Console.WriteLine("Exiting application. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("\n[!] Invalid choice. Please try again.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }
            }
        }
    }
}
