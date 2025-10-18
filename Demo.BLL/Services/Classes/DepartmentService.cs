using Demo.BLL.DTOs.Department;
using Demo.BLL.Factories;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Repositories.Interfaces;


namespace Demo.BLL.Services.Classes
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IUnitOfWork unitOfWork , IDepartmentRepository departmentRepository)
        {
            _unitOfWork = unitOfWork;
            _departmentRepository = departmentRepository;
        }
        //get all department
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {

            var depts = _departmentRepository.GetAll();

            var departmentsToReturn = depts.Select(d => d.ToDepartmentDto());

            return departmentsToReturn;

        }

        public DepartmentDetailsDto? GetById(int id)
        {

            var dept = _departmentRepository.GetById(id);

            return dept is null ? null : dept.ToDepartmentDetailsDto();

        }

        public int AddDepartment(CreatedDepartmentDto department)
        {

            _unitOfWork.DepartmentRepository.Add(department.ToEntity());
            return _unitOfWork.saveChanges();
        }

        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {

            _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.saveChanges();
        }

        public bool DeleteDepartment(int id)
        {

            var department = _unitOfWork.DepartmentRepository.GetById(id);

            if (department is null) return false;
            else
            {
                 _unitOfWork.DepartmentRepository.Remove(department) ;

                return _unitOfWork.saveChanges() > 0 ? true : false;


            }
        }
    }
}
