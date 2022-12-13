namespace TemplateApi.CQRS.Commands.Models
{
    public class CommandOptions
    {
        public int? MaxDegreeOfParaleism { get; set; }
        public bool AllowCommandExecuteByMoreThanOneCommandHandler { get; set; }
    }
}
