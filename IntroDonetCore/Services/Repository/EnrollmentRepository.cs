using IntroDonetCore.DAL;
using IntroDonetCore.Models;
using IntroDonetCore.Services.IRepository;

namespace IntroDonetCore.Services.Repository
{
    public class EnrollmentRepository:Repository<Enrollment>,IEnrollmentRepository
    {
        public EnrollmentRepository(IntroContext context) : base(context)
        {
        }
    }
}
