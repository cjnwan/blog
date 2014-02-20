
using System.Data.Entity;
using System.Threading;

namespace Domain.Repository
{
    
    public class BlogUnitofContext:UnitOfWorkContextBase
    {
       //private readonly ThreadLocal<BlogContext> _localCtx = new ThreadLocal<BlogContext>(() => new BlogContext());
        private readonly BlogContext context;

        public BlogUnitofContext()
        {
            context = new BlogContext() ;
        }
        //public BlogUnitofContext(DbContext context)
        //{
        //    context = context;
        //}
        //public BlogContext context { get; set; }

        protected override System.Data.Entity.DbContext Context
        {
            get { return context; }
        }


        //public BlogContext BlogContext {
        //    get  ;
        //    set { value = new BlogContext(); }}
    }
}