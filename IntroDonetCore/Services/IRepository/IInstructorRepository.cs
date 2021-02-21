using IntroDonetCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace IntroDonetCore.Services.IRepository
{
    public interface IInstructorRepository:IRepository<Instructor>
    {

        Task<IEnumerable<Instructor>> Instructors();
        Task<Instructor> Instructor(int id);


    }
}