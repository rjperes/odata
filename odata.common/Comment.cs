using System;

namespace odata.common
{
    public class Comment
    {
        public int CommentId { get; set; }
        public virtual Post Post { get; set; }
        public DateTime Timestamp { get; set; }
        public string Text { get; set; }
    }
}
