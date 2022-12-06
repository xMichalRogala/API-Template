﻿using System.ComponentModel.DataAnnotations.Schema;
using TemplateApi.Commons.Entity.Abstract;

namespace Auth.Domain.Entities
{
    public sealed class UserCredential : EntityBase<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Login { get; set; }
        public string Salt { get; set; }
        [Column("Password")]
        public byte[] PasswordBytes { get; set; }
        public int Iterations { get; set; }
    }
}