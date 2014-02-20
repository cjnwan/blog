using System.Collections.Generic;
using Domain.Model;

namespace WebUI.ViewModel
{
    public class OnePost
    {
        public Post Post { get; set; }
        public List<Category> Categories { get; set; }
        public List<Tag> Tags { get; set; }
        public Image Image { get; set; }
    }
}