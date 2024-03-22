using k8s.Models;
using KubeOps.Operator.Entities;
using System.ComponentModel;

namespace kubernetes_runbook_operator.Entities;

[KubernetesEntity(Group = "runbookpublishing.kubeops.dev", ApiVersion = "v1", Kind = "runbook", PluralName = "runbooks")]
public class V1Runbook : CustomKubernetesEntity<V1Runbook.V1RunbookEntitySpec, V1Runbook.V1RunbookEntityStatus>
{
    public class V1RunbookEntitySpec
    {
        [Description("Document of the runbook in markdown format")]
        public string Document { get; set; } = string.Empty;
        [Description("Identifier of the runbook.  Recommend using a GUID.  This will be the route that the markdown will be delivered on http[s]://[domain]/runbook/[this id]")]
        public string Identifier { get; set; } = Guid.NewGuid().ToString();
    }

    public class V1RunbookEntityStatus
    {
        public string RunbookStatus { get; set; } = string.Empty;
    }
}
