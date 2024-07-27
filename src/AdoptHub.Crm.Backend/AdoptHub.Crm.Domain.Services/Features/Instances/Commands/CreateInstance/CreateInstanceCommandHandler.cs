using AdoptHub.Crm.Infrastructure.Kubernetes;
using k8s;
using MediatR;

namespace AdoptHub.Crm.Domain.Services.Features.Instances.Commands.CreateInstance
{
    public record CreateInstanceCommand(string Name) : IRequest;

    public class CreateInstanceCommandHandler : IRequestHandler<CreateInstanceCommand>
    {
        private readonly IKubernetesDeploymentBuilder _kubernetesDeploymentBuilder;
        private const string k8sNamespace = "default"/*"adopthub-servers"*/;

        public CreateInstanceCommandHandler(IKubernetesDeploymentBuilder kubernetesDeploymentBuilder)
        {
            _kubernetesDeploymentBuilder = kubernetesDeploymentBuilder;
        }

        public async Task Handle(CreateInstanceCommand request, CancellationToken cancellationToken)
        {
            //Generate code that will create a new deployment in k8s cluster
            var guid = Guid.NewGuid();

            var name = request.Name.Replace(" ", "-").ToLower();

            // Load from the default kubeconfig on the machine.
            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();

            // Use the config object to create a client.
            var client = new Kubernetes(config);

            var a = _kubernetesDeploymentBuilder
                .WithName(name)
                .WithNamespace(k8sNamespace)
                .WithReplicas(1)
                .WithLabel("app", name)
                .WithSelectorMatchLabel("app", name)
                .WithContainer("hello-world-name", "bitnami/redis:latest", 6379, 6379)
                .Build();

            a.Validate();

            var test = await client.CreateNamespacedDeploymentAsync(a, k8sNamespace, pretty: true, cancellationToken: cancellationToken);
            test.Validate();

        }
    }
}
