using KubeOps.Operator.Webhooks;
using kubernetes_runbook_operator.Entities;

namespace kubernetes_runbook_operator.Webhooks;

public class RunbookValidator : IValidationWebhook<V1Runbook>
{
    private ILogger<RunbookValidator> _logger;

    public AdmissionOperations Operations => AdmissionOperations.Create;
    public RunbookValidator(ILogger<RunbookValidator> logger)
    {
        _logger = logger;
    }

    public ValidationResult Create(V1Runbook newEntity, bool dryRun)
    {
        _logger.LogInformation($"Validating entity {newEntity.Spec.Identifier}");
        if (string.IsNullOrWhiteSpace(newEntity.Spec.Identifier))
        {
            return ValidationResult.Fail(StatusCodes.Status400BadRequest, "Identifier must not be empty");
        }
        else if (Guid.TryParse(newEntity.Spec.Identifier, out _))
        {
            return ValidationResult.Fail(StatusCodes.Status400BadRequest, "Identifier must be a valid GUID");
        }
        else if (string.IsNullOrWhiteSpace(newEntity.Spec.Document))
        {
            return ValidationResult.Fail(StatusCodes.Status400BadRequest, "Document must not be empty");
        }

        return ValidationResult.Success();
    }
}
