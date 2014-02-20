using System.Collections.Generic;
using System.Linq;
using Domain.Model;
using WebUI.Dto;

namespace WebUI.ViewModel
{
    public class PerPost
    {
        public DtoPost Post;

        public List<Tag> Tags;

        public List<Category> Categories;

    }

}