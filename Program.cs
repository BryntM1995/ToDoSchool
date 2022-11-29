using School.Entities;
using School.Services;
using System;
using System.Linq;

namespace School
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            var closeProgram = false;
            var lastStudentId = 0;
            var lastTaskId=0;
            StudentServices studentServices = new StudentServices();
            TaskServices taskServices = new TaskServices(); 

            do
            {
                Console.WriteLine("Bienvenido Profesor.");
                Console.WriteLine("Por favor elija una de las siguientes opciones:\n");
                Console.WriteLine("1) Crear registro de estudiante.");
                Console.WriteLine("2) Ver Registro de un estudiante.");
                Console.WriteLine("3) Actualizar un registro de un estudiante.");
                Console.WriteLine("4) Eliminar registro de estudiante.");
                Console.WriteLine("5) Ver Registro con todos los estudiantes.");
                Console.WriteLine("6) Opciones de tareas.\n");
                Console.WriteLine("0) Cerrar Porgrama.");
                var userChoice = ValidateInt(Console.ReadLine());
                Console.Clear();
                switch (userChoice)
                {
                    case (int)MainMenuOptions.CreateStudents:
                        Console.WriteLine("Por favor introduzca el nombre del estudiante");
                        var studentName = TryOnlyString(Console.ReadLine());
                        Console.WriteLine("Por favor introduzca el apellido del estudiante");
                        var studentLastName = TryOnlyString(Console.ReadLine());
                        Console.Clear();
                        GenerateStudentId(ref lastStudentId);
                        Student student = new Student(lastStudentId, studentName, studentLastName);
                        studentServices.AddToRecord(student);  
                        break;
                    case (int)MainMenuOptions.ReadStudent:
                        if (studentServices.GetAllRecords().Any())
                        {
                            Console.WriteLine("Registro de estudiantes.");
                            Console.WriteLine("________________________________________________________________");
                            foreach (var students in studentServices.GetAllRecords())
                            {
                                Console.WriteLine(students.ToStringStudent());
                                Console.WriteLine("________________________________________________________________");
                            }
                            Console.WriteLine("________________________________________________________________");
                         
                        }
                        else
                        {
                            Console.WriteLine("No hay ningun Registro guardado todavia."); 
                            Console.Clear();
                            goto default;
                        }
                        Console.WriteLine(" Por favor indique el Id del estudiante que desea ver en especifico.");
                        var readThisId = ValidateInt(Console.ReadLine());
                        Console.Clear();
                        Console.WriteLine(studentServices.GetRecord(readThisId).ToStringStudent());
                        break;
                    case (int)MainMenuOptions.UpdateStudent:
                        if (studentServices.GetAllRecords().Any<Student>())
                        {
                            Console.WriteLine("Registro de estudiantes.");
                            Console.WriteLine("________________________________________________________________");
                            foreach (var students in studentServices.GetAllRecords())
                            {
                                Console.WriteLine(students.ToStringStudent());
                                Console.WriteLine("________________________________________________________________");
                            }
                            Console.WriteLine("________________________________________________________________");
                        }
                        else
                        {
                            Console.WriteLine("No hay ningun Registro guardado todavia.");
                            goto default;
                        }
                        Console.WriteLine(" Por favor indique el Id del estudiante que desea actualizar.");
                        var updateThisId = ValidateInt(Console.ReadLine());
                        Console.WriteLine(" Por favor indique el nombre del estudiante que desea actualizar.");
                        var updateName = TryOnlyString(Console.ReadLine());
                        Console.WriteLine(" Por favor indique el Apellido del estudiante que desea actualizar.");
                        var updateLastName = TryOnlyString(Console.ReadLine());
                        Console.Clear();
                        Student updateStudent = new Student(studentServices.GetRecord(updateThisId).StudentId, updateName, updateLastName);
                        studentServices.UpdateFromRecord(updateStudent);
                        break;
                    case (int)MainMenuOptions.DeleteStudent:
                        if (studentServices.GetAllRecords().Any<Student>())
                        {
                            Console.WriteLine("Registro de estudiantes.");
                            Console.WriteLine("________________________________________________________________");
                            foreach (var students in studentServices.GetAllRecords())
                            {
                                Console.WriteLine(students.ToStringStudent());
                                Console.WriteLine("________________________________________________________________");
                            }
                            Console.WriteLine("________________________________________________________________");
                        }
                        else
                        {
                            Console.WriteLine("No hay ningun Registro guardado todavia.");
                            goto default;
                        }
                        Console.WriteLine(" Por favor indique el Id del estudiante que desea Eliminar.");
                        var deleteThisId = ValidateInt(Console.ReadLine());
                        Console.Clear();
                        var deleted= studentServices.RemoveFromRecord(deleteThisId); 
                        if (deleted)
                        {
                            taskServices.RemoveAllTaskFromOneStudent(deleteThisId);
                        } 
                        break;
                    case (int)MainMenuOptions.ReadAllStudents:
                       
                        if (studentServices.GetAllRecords().Any<Student>())
                        {
                            Console.WriteLine("Registro de estudiantes.");
                            Console.WriteLine("________________________________________________________________");
                            foreach (var students in studentServices.GetAllRecords())
                            {
                                Console.WriteLine(students.ToStringStudent());
                                Console.WriteLine("________________________________________________________________");
                            }
                            Console.WriteLine("________________________________________________________________");
                        }
                        else
                        {
                            Console.WriteLine("No hay ningun Registro guardado todavia.");
                            goto default;
                        }
                        break;
                    case (int)MainMenuOptions.TaskOptions:
                        Console.WriteLine("Bievenido Profesor a la opcion de tareas.");
                        Console.WriteLine("Por favor elija una de las siguientes opciones:\n");
                        Console.WriteLine("1) Crear una tarea.");
                        Console.WriteLine("2) Ver Registro de una tarea.");
                        Console.WriteLine("3) Actualizar un registro de una tarea.");
                        Console.WriteLine("4) Eliminar registro de una tarea.");
                        Console.WriteLine("5) Ver Registro con todos los estudiantes y tareas asignada.\n");
                        Console.WriteLine("0) Cerrar Porgrama.");
                        var userTaskChoice = ValidateInt(Console.ReadLine());
                        switch (userTaskChoice)
                        {
                            case (int)SubMenuOptions.CreateTask:
                                Console.WriteLine("Por favor introduzca el nombre de la tarea");
                                var taskName = TryOnlyString(Console.ReadLine());
                                Console.WriteLine("Por favor introduzca el nombre del profesor.");
                                var teacherName = TryOnlyString(Console.ReadLine());
                                Console.WriteLine("Por favor elija un id de la lista para asignar tarea.\n");
                                if (studentServices.GetAllRecords().Any<Student>())
                                {
                                    Console.WriteLine("Registro de estudiantes.");
                                    Console.WriteLine("________________________________________________________________");
                                    foreach (var students in studentServices.GetAllRecords())
                                    {
                                        Console.WriteLine(students.ToStringStudent());
                                        Console.WriteLine("________________________________________________________________");
                                    }
                                    Console.WriteLine("________________________________________________________________");
                                }
                                else
                                {
                                    Console.WriteLine("No hay ningun Registro guardado todavia.");
                                    goto default;
                                }
                                var studentTaskId = ValidateInt(Console.ReadLine());
                                Console.Clear();
                                Task task = new Task(GenerateTaskId(ref lastTaskId), taskName, teacherName, studentServices.GetRecord(studentTaskId));
                                taskServices.AddToRecord(task);
                                break;
                            case (int)SubMenuOptions.ReadTask:
                                if (taskServices.GetAllRecords().Any<Task>())
                                {
                                    Console.WriteLine("Registro de tareas.");
                                    Console.WriteLine("________________________________________________________________");
                                    foreach (var tasks in taskServices.GetAllRecords())
                                    {
                                        Console.WriteLine(tasks.ToStringTask());
                                        Console.WriteLine("________________________________________________________________");
                                    }
                                    Console.WriteLine("________________________________________________________________");
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("No hay ningun Registro guardado todavia.");
                                    goto default;
                                }
                                Console.WriteLine(" Por favor indique el Id la tarea que desea ver en especifico.");
                                var readThisTaskId = ValidateInt(Console.ReadLine());
                                Console.Clear();
                                Console.WriteLine(taskServices.GetRecord(readThisTaskId).ToStringTask());
                                Console.WriteLine("");
                                break;
                            case (int)SubMenuOptions.UpdateTask:
                                if (taskServices.GetAllRecords().Any<Task>())
                                {
                                    Console.WriteLine("Registro de tareas.");
                                    Console.WriteLine("________________________________________________________________");
                                    foreach (var tasks in taskServices.GetAllRecords())
                                    {
                                        Console.WriteLine(tasks.ToStringTask());
                                        Console.WriteLine("________________________________________________________________");
                                    }
                                    Console.WriteLine("________________________________________________________________");
                                }
                                else
                                {
                                    Console.WriteLine("No hay ningun Registro guardado todavia.");
                                    goto default;
                                }
                                Console.WriteLine(" Por favor indique el Id de la tarea que desea actualizar.");
                                var updateThisTaskId = ValidateInt(Console.ReadLine());
                                Console.WriteLine(" Por favor indique el nombre de la tarea que desea actualizar.");
                                var updateTaskName = TryOnlyString(Console.ReadLine());
                                Console.WriteLine(" Por favor indique el nuevo nombre de el profesor que desea añadir.");
                                var updateTeacherName = TryOnlyString(Console.ReadLine());
                                Console.Clear();
                                Task updateTask = new Task(updateThisTaskId, updateTaskName, updateTeacherName, taskServices.GetRecord(updateThisTaskId).StudentTaskId);
                                taskServices.UpdateFromRecord(updateTask);
                                break;
                            case (int)SubMenuOptions.DeleteTask:
                                if (taskServices.GetAllRecords().Any<Task>())
                                {
                                    Console.WriteLine("Registro de tareas.");
                                    Console.WriteLine("________________________________________________________________");
                                    foreach (var tasks in taskServices.GetAllRecords())
                                    {
                                        Console.WriteLine(tasks.ToStringTask());
                                        Console.WriteLine("________________________________________________________________");
                                    }
                                    Console.WriteLine("________________________________________________________________");
                                }
                                else
                                {
                                    Console.WriteLine("No hay ningun Registro guardado todavia.");
                                    goto default;
                                }
                                Console.WriteLine(" Por favor indique el Id del estudiante que desea Eliminar.");
                                var deleteThisTask = ValidateInt(Console.ReadLine());
                                Console.Clear();
                                taskServices.RemoveFromRecord(deleteThisTask);
                                break;
                            case (int)SubMenuOptions.ReadAllTasks:
                                if (taskServices.GetAllRecords().Any<Task>())
                                {
                                    Console.WriteLine("Registro de tareas.");
                                    Console.WriteLine("________________________________________________________________");
                                    foreach (var tasks in taskServices.GetAllRecords())
                                    {
                                        Console.WriteLine(tasks.ToStringTask());
                                        Console.WriteLine("________________________________________________________________");
                                    }
                                    Console.WriteLine("________________________________________________________________");
                                }
                                else
                                {
                                    Console.WriteLine("No hay ningun Registro guardado todavia.");
                                    goto default;
                                }
                                break;
                            case (int)SubMenuOptions.Close:
                                break;
                            default:
                                Console.WriteLine("intente otra opcion.");
                                break;
                        }
                        break;
                    case (int)MainMenuOptions.Close:
                        Console.Clear();
                        Console.WriteLine("Cerrando...");
                        closeProgram = true;
                        break;
                    default:
                        Console.WriteLine("intente otra opcion.");
                        break;
                }
            } while (!closeProgram);
            
        }
        public static int ValidateInt(string choice)
        {
            var isItNumber = int.TryParse(choice, out int result);
            if (isItNumber)
            {
                return result;
            }
            Console.WriteLine("Introduzca un numero valido");
            return 99;
        }
        public enum MainMenuOptions
        {
            Close = 0,
            CreateStudents = 1,
            ReadStudent = 2,
            UpdateStudent= 3,
            DeleteStudent= 4,
            ReadAllStudents=5,
            TaskOptions=6,
        }
        public enum SubMenuOptions
        {
            Close = 0,
            CreateTask = 1,
            ReadTask = 2,
            UpdateTask = 3,
            DeleteTask = 4,
            ReadAllTasks = 5,
        }
        public static bool StringVerifier(string userResponse)
        {
            if (userResponse.Contains('1')|
                userResponse.Contains('2')|
                userResponse.Contains('3')|
                userResponse.Contains('4')|
                userResponse.Contains('5')|
                userResponse.Contains('6')|
                userResponse.Contains('7')|
                userResponse.Contains('8')|
                userResponse.Contains('9')|
                userResponse.Contains('0')|
                userResponse.Contains(' ')|
                userResponse.Contains('-')|
                userResponse.Contains('+')|
                userResponse.Contains('/')|
                userResponse.Contains('x')|
                userResponse.Contains('*')|
                userResponse.Contains('$')|
                userResponse.Contains('&')|
                userResponse.Contains('#'))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string TryOnlyString(string userResponse)
        {
                if (!StringVerifier(userResponse))
                {
                    Console.WriteLine("No numeros o espacios vacios en este campo por favor.");
                    var requestedString = Console.ReadLine();
                   TryOnlyString(requestedString);
                }
            return userResponse;
        }
        public static  void GenerateStudentId(ref int lastId)
        {
            lastId++;
        }
        public static ref int GenerateTaskId(ref int lastTaskId)
        {
            lastTaskId++;
            return ref lastTaskId;
        }
    }

}
