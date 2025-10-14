[web] GET /workflow/v1/task-templates/  (Dataverse.Api.External.Controllers.v1.Workflow.TaskTemplatesController.MinimalQuery)  [L67–L75]
  └─ calls TaskTemplateRepository.ReadQuery [L71]
  └─ queries TaskTemplate [L71]
    └─ reads_from TaskTemplates
  └─ uses_service IControlledRepository<TaskTemplate>
    └─ method ReadQuery [L71]

