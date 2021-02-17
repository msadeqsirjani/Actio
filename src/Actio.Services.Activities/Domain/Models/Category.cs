using Actio.Common.Exceptions;
using System;

namespace Actio.Services.Activities.Domain.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Category(string name)
        {
            if (name.IsNullOrWhiteSpace())
                throw new ActioException("empty_category_name", "Category name can not be empty.");

            Id = Guid.NewGuid();
            Name = name.ToLowerInvariant();
        }
    }
}
