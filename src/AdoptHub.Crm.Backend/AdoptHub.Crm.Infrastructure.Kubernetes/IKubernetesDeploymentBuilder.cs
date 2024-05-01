using k8s.Models;

namespace AdoptHub.Crm.Infrastructure.Kubernetes;

public interface IKubernetesDeploymentBuilder
{
    KubernetesDeploymentBuilder WithName(string name);
    KubernetesDeploymentBuilder WithNamespace(string namespaceName);
    KubernetesDeploymentBuilder WithReplicas(int replicas);
    KubernetesDeploymentBuilder WithLabel(string key, string value);
    KubernetesDeploymentBuilder WithSelectorMatchLabel(string key, string value);
    KubernetesDeploymentBuilder WithContainer(string name, string image, ushort k8sPort, ushort hostPort);
    V1Deployment Build();
    void Reset();
}