using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using Domain.Repository;
using Ninject;
using Service;

namespace WebUI.Ioc
{
    public class NinjectControllerFactory : DefaultControllerFactory 
    {
        private readonly IKernel _kernel;

        public NinjectControllerFactory()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)_kernel.Get(controllerType);
        }

        private void AddBindings()
        {
            
            _kernel.Bind<IImageRepository>().To<ImageRepository>();
            _kernel.Bind<IRoleRepository>().To<RoleRepository>();
            _kernel.Bind<ITagRepository>().To<TagRepository>();
            _kernel.Bind<IUserRepository>().To<UserRepository>();
            _kernel.Bind<ICategoryRepository>().To<CategoryRepository>();
            _kernel.Bind<ICommentRepository>().To<CommentRepository>();
            _kernel.Bind<IPostRepository>().To<PostRepository>();
            _kernel.Bind<ICalendarEventRepository>().To<CalendarEventRepository>();
            _kernel.Bind<IPostCatRepository>().To<PostCatRepository>();

            _kernel.Bind<IUnitOfWork>().To<BlogUnitofContext>();
            _kernel.Bind<DbContext>().To<BlogContext>();
            //_kernel.Bind<IUnitOfWorkContext>().To<BlogUnitofContext>();
           
            _kernel.Bind<ICategoryService>().To<CategoryService>();
            _kernel.Bind<ICommentService>().To<CommentService>();
            _kernel.Bind<ITagService>().To<TagService>();
            _kernel.Bind<IUserService>().To<UserService>();
            _kernel.Bind<IImageService>().To<ImageService>();
            _kernel.Bind<IPostService>().To<PostService>();
            _kernel.Bind<IRoleService>().To<RoleService>();
            _kernel.Bind<ICalendarEventService>().To<CalendarEventService>();
            _kernel.Bind<IPostCatService>().To<PostCatService>();
        }
    }
}