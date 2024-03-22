using HeyRed.MarkdownSharp;
using KubeOps.KubernetesClient;
using kubernetes_runbook_operator.Entities;
using Microsoft.AspNetCore.Mvc;

namespace kubernetes_runbook_operator.Controller
{
    [Route("runbook")]
    public class RunbookWebController : ControllerBase
    {
        private readonly IKubernetesClient _kubernetesClient;
        private readonly ILogger<RunbookWebController> _logger;
        public RunbookWebController(IKubernetesClient kubernetesClient, ILogger<RunbookWebController> logger)
        {
            _kubernetesClient = kubernetesClient;
            _logger = logger;
        }
        [HttpGet("{runbookIdentifier}")]
        public async Task<IActionResult> Index(Guid runbookIdentifier)
        {
            _logger.LogInformation($"Received request for runbook with identifier {runbookIdentifier}");

            var allRunbookEntities = await _kubernetesClient.List<V1Runbook>();
            _logger.LogInformation($"Found {allRunbookEntities.Count()} runbook entities");

            var runbookEntity = allRunbookEntities.FirstOrDefault(e => e.Spec.Identifier == runbookIdentifier.ToString());
            if (runbookEntity == null)
            {
                _logger.LogInformation($"No runbook entity found with identifier {runbookIdentifier}");
                return NotFound();
            }
            _logger.LogInformation($"Found runbook entity with identifier {runbookIdentifier}");
            Markdown markdown = new();

            return new ContentResult
            {
                Content = markdown.Transform(runbookEntity.Spec.Document),
                ContentType = "text/html"
            };
        }
    }
}
