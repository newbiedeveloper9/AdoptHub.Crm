using k8s.Models;
namespace AdoptHub.Crm.Infrastructure.Kubernetes
{
    public class KubernetesDeploymentBuilder : IKubernetesDeploymentBuilder
    {
        private V1Deployment _deployment;
        private V1PodTemplateSpec _podTemplateSpec;
        private V1ObjectMeta _metadata;
        private V1DeploymentSpec _deploymentSpec;
        private V1Container _container;

        public KubernetesDeploymentBuilder()
        {
            Reset();
        }

        public KubernetesDeploymentBuilder WithName(string name)
        {
            _metadata.Name = name;
            return this;
        }

        public KubernetesDeploymentBuilder WithNamespace(string namespaceName)
        {
            _metadata.NamespaceProperty = namespaceName;
            return this;
        }

        public KubernetesDeploymentBuilder WithReplicas(int replicas)
        {
            _deploymentSpec.Replicas = replicas;
            return this;
        }

        public KubernetesDeploymentBuilder WithLabel(string key, string value)
        {
            _metadata.Labels ??= new Dictionary<string, string>();
            _metadata.Labels.Add(key, value);

            return this;
        }

        public KubernetesDeploymentBuilder WithSelectorMatchLabel(string key, string value)
        {
            _deploymentSpec.Selector ??= new V1LabelSelector();
            _deploymentSpec.Selector.MatchLabels ??= new Dictionary<string, string>();
            _deploymentSpec.Selector.MatchLabels.Add(key, value);

            return this;
        }

        public KubernetesDeploymentBuilder WithContainer(string name, string image, ushort k8sPort, ushort hostPort)
        {
            _container.Name = name;
            _container.Image = image;
            _container.Ports = new List<V1ContainerPort> { new V1ContainerPort { ContainerPort = k8sPort, HostPort = hostPort } };
            _container.Env = new List<V1EnvVar>() { new V1EnvVar { Name = "ALLOW_EMPTY_PASSWORD", Value = "YES" } };
            return this;
        }

        public V1Deployment Build()
        {
            _podTemplateSpec.Metadata = _metadata;
            _podTemplateSpec.Spec = new V1PodSpec
            {
                Containers = new List<V1Container> { _container }
            };

            _deployment.Metadata = _metadata;
            _deployment.Spec = _deploymentSpec;
            _deployment.Spec.Template = _podTemplateSpec;

            var result = _deployment;
            Reset();
            return result;
        }

        public void Reset()
        {
            _deployment = new V1Deployment
            {
                ApiVersion = "apps/v1",
                Kind = "Deployment"
            };
            _podTemplateSpec = new V1PodTemplateSpec();
            _metadata = new V1ObjectMeta();
            _deploymentSpec = new V1DeploymentSpec();
            _container = new V1Container();
        }
    }
}
