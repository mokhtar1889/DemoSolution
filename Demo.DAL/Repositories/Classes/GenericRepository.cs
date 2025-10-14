using Demo.DAL.Contexts;
using Demo.DAL.Models.Shared;
using Demo.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories.Classes
{
    public class GenericRepository<Entity>(ApplicationDbContext context):IGenericRepository<Entity> where Entity : BaseEntity
    {
        // get Entity by id
        public Entity? GetById(int id)
        {
            var Entity = context.Set<Entity>().Find(id);
            return Entity;
        }

        //get all Entitys
        public IEnumerable<Entity> GetAll(bool withTracking = false)
        {
   
            if (withTracking == true) return context.Set<Entity>().ToList();

            else return context.Set<Entity>().AsNoTracking().Where(entity => entity.IsDeleted == false).ToList();
        }

        public IEnumerable<TResult> GetAll<TResult>(System.Linq.Expressions.Expression<Func<Entity, TResult>> selector)
        {
            return context.Set<Entity>().Where(entity => entity.IsDeleted == false).Select(selector);
        }

        // add Entity
        public int Add(Entity Entity)
        {

            context.Set<Entity>().Add(Entity);
            return context.SaveChanges();

        }

        public int Update(Entity Entity)
        {

            context.Set<Entity>().Update(Entity);
            return context.SaveChanges();

        }

        public int Remove(Entity Entity)
        {

            context.Set<Entity>().Remove(Entity);
            return context.SaveChanges();

        }

        //public IEnumerable<Entity> GetEnumerable()
        //{
        //    return context.Set<Entity>();
        //}

        //public IQueryable<Entity> GetQueryable()
        //{
        //    return context.Set<Entity>();
        //}


    }
}
