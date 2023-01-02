using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TemplateApi.CQRS.Commands.Abstract;
using TemplateApi.CQRS.Commands.Models;

namespace TemplateApi.CQRS.Commands.Concrete
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly CommandOptions _options;

        public CommandDispatcher(IServiceScopeFactory serviceScopeFactory, IOptions<CommandOptions> options)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _options = options.Value;
        }

        public async Task DispatchAsync<T>(T command, CancellationToken cancellationToken = default) where T : ICommand
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var commandHandlers = serviceProvider.GetServices<ICommandHandler<T>>().ToList();

                CheckRulesAboutProcessingComands(commandHandlers);

                await Task.WhenAll(commandHandlers.Select(async x => await x.ExecuteAsync(command, cancellationToken)));
            }
        }

        private void CheckRulesAboutProcessingComands<T>(IEnumerable<ICommandHandler<T>> commandHandlers) where T : ICommand
        {
            if (!_options.AllowCommandExecuteByMoreThanOneCommandHandler && commandHandlers.Count() > 1)
                throw new InvalidOperationException($"Command Dispatcher has set option to process one type command to only one command handler! CommandType: {typeof(T)}");
        }
    }
}
