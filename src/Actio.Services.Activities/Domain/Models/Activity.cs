using Actio.Common.Exceptions;
using System;

namespace Actio.Services.Activities.Domain.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Activity(Guid id, string name, Category category, string description, Guid userId, DateTime createdAt)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ActioException("empty_activity_name", "Activity name can not be empty.");

            Id = id;
            Name = name;
            Category = category.Name;
            Description = description;
            UserId = userId;
            CreatedAt = createdAt;
        }
    }
}
