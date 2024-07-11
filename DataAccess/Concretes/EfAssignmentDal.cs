using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Context;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes
{
    public class EfAssignmentDal : EfRepositoryBase<Assignment, Guid, ProjectTrackingSystemContext>, IAssignmentDal
    {
        public EfAssignmentDal(ProjectTrackingSystemContext context) : base(context)
        {
        }
    }
}
