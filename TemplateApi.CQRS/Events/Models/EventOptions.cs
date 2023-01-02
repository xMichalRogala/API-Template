namespace TemplateApi.CQRS.Events.Models
{
    public class EventOptions
    {
        public int ParallelDegree { get; set; } = 3;

        public int Delay { get; set; } = 5000;
    }
}
