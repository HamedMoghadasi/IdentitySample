using IdentitySample.Authorizations.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace DynamicAuthorization.Models
{
    public class ModelBase<TKey>: IModelBase where TKey: struct
    {
        [Key]
        public TKey Id { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class ModelBase : ModelBase<Guid> { }
}
