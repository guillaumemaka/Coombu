using System;

namespace Coombu.Models.ViewModels
{
    public class GroupViewModel
    {
        public Group Group { get; set; }
        public Boolean IsOwner { get; set; }
        public Boolean IsMember { get; set; }
        
        public GroupViewModel(Group group, UserProfile user) 
        {
            if (group.Owner != null) 
            {
                this.IsOwner = group.Owner.Equals(user);
            }
                        
            this.IsMember = user.Groups.Contains(group);
            this.Group = group;
        }

        public GroupViewModel()
        {
            // TODO: Complete member initialization
        }
    }
}
