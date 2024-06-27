namespace API.Entities
{
    public class UserLike
    {
        // person who is liked
        public AppUser SourceUser { get; set; }
        public int SourceUserId { get; set; }

        //person who is being liked
        public AppUser TargetUser { get; set; }
        public int TargetUserId { get; set; }
    }
}