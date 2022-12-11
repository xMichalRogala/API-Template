﻿using TemplateApi.Commons.Entity.Abstract;

namespace Core.Domain.Schemas.Entities
{
    public class User : EntityBase<Guid>
    {
        public new Guid Id { get; set; } = Guid.NewGuid();
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
