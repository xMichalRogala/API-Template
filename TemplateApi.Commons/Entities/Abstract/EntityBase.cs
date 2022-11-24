namespace TemplateApi.Commons.Entity.Abstract
{
    public abstract class EntityBase<TKey>
    {
        public  TKey Id { get; set; }
    }
}
