using Domain.DataAccessContracts;
using Domain.ModelClasses;
using Microsoft.EntityFrameworkCore;

namespace EfcData;

public class PostSqliteDAO : IPostDao
{
    private readonly PostContext context;
    
    public PostSqliteDAO(PostContext context)
    {
        this.context = context;
    }
       

    public async Task<Post> GetPostByIdAsync(int id)
    {
        return context.Posts.First(t => t.Id == id);
    }
   

    public async Task DeletePostAsync(int id)
    {
        var firstOrDefault = context.Posts.FirstOrDefault(p => p.Id == id);
        if (firstOrDefault != null)
        {
            context.Posts.Remove(firstOrDefault);
        }
        await context.SaveChangesAsync();
    }
   

    public async Task<ICollection<Post>> GetAllPostsAsync()
    {
        ICollection<Post> posts = await context.Posts.ToArrayAsync(); // get RedditForum, get the lists of posts from ForumReddit
        return posts;
    }
   

    public async Task<Post> AddPostAsync(Post post)
    {
        int largestId = 0;
        if (context.Posts.Any())
        {
            largestId = context.Posts.Max(t => t.Id);
        }
        int nextId = largestId + 1;
        post.Id = nextId;
        context.Posts.Add(post);
        await context.SaveChangesAsync();
        return post;
    }
   

    public async Task UpdatePostAsync(Post post)
    {
        Post updatePost = context.Posts.FirstOrDefault(t => t.Id == post.Id)!;
        if (updatePost == null) throw new Exception("The post does not exist.");
        else
        {
            context.Posts.Remove(updatePost);
            context.Posts.Add(post);
        }
        await context.SaveChangesAsync();
    }
}