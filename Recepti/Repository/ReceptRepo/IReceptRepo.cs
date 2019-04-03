using Microsoft.EntityFrameworkCore;
using Recepti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Recepti.Repository.ReceptRepo
{
    public interface IReceptRepo : IRepo<Recept>
    {
    }
}
