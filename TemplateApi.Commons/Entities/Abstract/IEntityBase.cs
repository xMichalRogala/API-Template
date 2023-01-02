namespace TemplateApi.Commons.Entity.Abstract
{
    public interface IEntityBase<TKey>
    {
        public  TKey Id { get; set; }
    }
}
