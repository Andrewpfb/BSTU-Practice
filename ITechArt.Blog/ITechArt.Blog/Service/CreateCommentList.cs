using System;
using System.Linq;
using System.Collections.Generic;

using ITechArt.Blog.Models;


namespace ITechArt.Blog.Service
{
    //TODO Исправить. Метод работает некорректно.
    public class CreateCommentList
    {
        static BlogContext db = new BlogContext();
        public static CommentViewModel CreateViewListComment(List<Comment> commentsList)
        {
            CommentViewModel cmp = new CommentViewModel();
            //Создание списка комментариев для поста.
            foreach (var tmpComm in commentsList)
            {
                cmp.CommentsViewList.Add(new CommentViewModel
                {
                    CommentId = tmpComm.Id,
                    ParentId = Convert.ToInt32(tmpComm.ParentId),
                    ParentAuthor = GetParentAuthor((int)tmpComm.ParentId),
                    ImagePath = tmpComm.UserImagePath,
                    NestedLvl = GetNestedLvl((int)tmpComm.ParentId, (int)tmpComm.Seed),
                    Username = tmpComm.User.Email.Split('@')[0],
                    NumberOfElapsedDays = (DateTime.Now - tmpComm.CommentOn).Days,
                    Text = tmpComm.Text
                });
            }
            //Создание подобия дерева комментариев.
            for (int i = 0; i < cmp.CommentsViewList.Count(); i++)
            {
                var tmpComm = cmp.CommentsViewList[i];
                if (cmp.CommentsViewList[i].ParentId != 0)
                {
                    int tmpParentIndex = cmp.CommentsViewList.FindIndex(p => p.CommentId == p.ParentId);
                    cmp.CommentsViewList.Remove(cmp.CommentsViewList[i]);
                    cmp.CommentsViewList.Insert(tmpParentIndex + 2, tmpComm);
                }
            }
            cmp.CommentsViewList.Reverse();
            return cmp;
        }

        private static string GetParentAuthor(int parentId)
        {
            string tmpParentAuthor;
            if (parentId != 0)
            {
                tmpParentAuthor = db.Comment.Find(parentId).User.Email.Split('@')[0];
            }
            else
            {
                tmpParentAuthor = "";
            }
            return tmpParentAuthor;
        }
        private static int GetNestedLvl(int parentId, int seed)
        {
            int nestedLvl;
            if (parentId == 0)
            {
                nestedLvl = -1;
            }
            else
            {
                nestedLvl = (int)(parentId - seed);
            }
            return nestedLvl;
        }
    }
}