using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Model;
using Infrastructure;
using Service;

namespace WebUI.Controllers
{
    public class PostController : Controller
    {
        private IPostService _postService;
        private ICommentService _commentService;

        public PostController(IPostService post,ICommentService comment)
        {
            _postService = post;
            _commentService = comment;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(string title)
        {
            return View(_postService.Entities.Single(p=>p.PostTitle==title));
        }

        [HttpPost]
        public ActionResult AddComment()
        {
            string name = Request.Form["name"];
            string mail = Request.Form["mail"];
            string content = Request.Form["content"];
            string postId = Request.Form["postId"];
            string replay = Request.Form["replay"];
            string replycommentId = Request.Form["replycommentId"];
            int tempId = Convert.ToInt32(postId);
             

            Post post = _postService.Entities.Single(p => p.PostId ==tempId );

            Comment commentSend = new Comment()
                {
                    CommentContent = content,
                    CommentEmail = mail,
                    CommentUserName = name,
                    CommnetDate = DateTime.Now
                };

            post.Comments.Add(commentSend);

            if (replay == "true")
            {
                int id = Convert.ToInt32(replycommentId);
                Comment commentReceive=_commentService.Entities.Single(c => c.CommentId == id);
                MailSend.SendEmailReply(commentSend, commentReceive);
            }
            else
            {
                MailSend.SendEmail(commentSend);
            }
           
            _postService.Update(post);
            return View("Detail",_postService.Entities.Single(p => p.PostId==tempId));
        }

    }
}
