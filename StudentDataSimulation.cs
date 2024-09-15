using StudentsAPI.Model;

namespace StudentsAPI
{
    public class StudentDataSimulation
    {
        public static readonly List<Student> students = new List<Student>()
        {
            new Student() {Id = 1, Name ="Mostafa", age = 36 , Grade =80 },
            new Student() {Id = 2, Name ="Mohamed", age = 34 , Grade =90 },
            new Student() {Id = 3, Name ="Abdelrahman", age = 32 , Grade =50 },
            new Student() {Id = 4, Name ="Omar", age = 6 , Grade =95 }
        };
    }
}
